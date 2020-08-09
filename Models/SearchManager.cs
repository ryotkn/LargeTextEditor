using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LargeTextEditor
{
    class SearchManager
    {
        private FileDetail FileDetail;

        private bool RegexChecked;
        private bool UpperLowerMatched;
        private bool WordMatched;

        public long byteIndex = 0;
        public volatile bool Stopped = false;


        public long findByteIndex { get; set; }

        public long findByteCount { get; set; }


        public enum Direction
        {
            Forward, Backward
        }


        public SearchManager(
            FileDetail FileDetail,
            bool RegexChecked, bool UpperLowerMatched, bool WordMatched)
        {
            this.FileDetail = FileDetail;

            this.RegexChecked = RegexChecked;
            this.UpperLowerMatched = UpperLowerMatched;
            this.WordMatched = WordMatched;

        }


        public async Task<bool> FindNext(
            Regex regex,
            long startByteIndex)
        {
            findByteIndex = -1;
            findByteCount = 0;

            long byteIndex = startByteIndex + 1;

            Interlocked.Exchange(ref this.byteIndex, byteIndex);

            var builder = new StringBuilder();

            var lines = new List<LineReader.ReadLineResult>();

            using (var fileStream = File.OpenRead(FileDetail.Path))
            using (var reader =
                new StreamReader(fileStream))
            {
                FileDetail.ByteLength = fileStream.Length;

                if (byteIndex >= FileDetail.ByteLength)
                {
                    byteIndex = 0;
                }

                fileStream.Position =
                    FileDetail.BomLength + byteIndex;

                var LineReader = new LineReader(
                    reader, FileDetail.MaxColumns);

                while (true)
                {
                    if (Stopped)
                    {
                        return false;
                    }

                    Interlocked.Exchange(ref this.byteIndex, byteIndex);

                    var ReadLineResult = await LineReader.ReadLineAsync();

                    lines.Add(ReadLineResult);

                    var line = lines.Select(x => x.Line).Join("");

                    var match = regex.Match(line);

                    if (match.Success)
                    {
                        var pre_text = line.Substring(0, match.Index);

                        findByteIndex = byteIndex +
                            FileDetail.Encoding.GetByteCount(pre_text);

                        findByteCount =
                            FileDetail.Encoding.GetByteCount(match.Value);

                        if (byteIndex < startByteIndex
                            && startByteIndex <= findByteIndex)
                        {
                            return false;
                        }

                        return true;
                    }

                    if (LineReader.EndOfFile)
                    {
                        if (startByteIndex == 0)
                        {
                            return false;
                        }

                        byteIndex = 0;
                        fileStream.Position = FileDetail.BomLength;
                        lines.Clear();
                        continue;
                    }

                    var lastByteIndex = byteIndex;

                    if (ReadLineResult.TerminateChars != null)
                    {
                        byteIndex += FileDetail.Encoding.GetByteCount(line);
                        lines.Clear();
                    }
                    else if (lines.Count >= 2)
                    {
                        byteIndex += FileDetail.Encoding.GetByteCount(lines[0].Line);
                        lines.RemoveAt(0);
                    }

                    if (lastByteIndex < startByteIndex
                        && byteIndex >= startByteIndex)
                    {
                        return false;
                    }

                }

            }
        }


        public async Task<bool> FindPrev(
            Regex regex,
            long searchStartByteIndex)
        {
            const int bufferSize = 1024;

            byte[] bytes = null;
            byte[] bytesCurrent = null;
            byte[] bytesLast = new byte[0];

            long startByteIndex = searchStartByteIndex - bufferSize;
            long endByteIndex = searchStartByteIndex;

            Interlocked.Exchange(ref this.byteIndex, startByteIndex);

            findByteIndex = -1;
            findByteCount = 0;

            using (var fileStream = File.OpenRead(FileDetail.Path))
            {
                FileDetail.ByteLength = fileStream.Length;

                await FileDetail.DetectBom(fileStream);

                while (true)
                {
                    if (Stopped)
                    {
                        return false;
                    }

                    Interlocked.Exchange(ref this.byteIndex, startByteIndex);

                    if (endByteIndex == 0)
                    {
                        endByteIndex = FileDetail.ByteLength - FileDetail.BomLength;
                        startByteIndex = endByteIndex - bufferSize;
                        bytesLast = new byte[0];
                    }

                    if (startByteIndex < 0)
                    {
                        startByteIndex = 0;
                    }

                    if (startByteIndex <= searchStartByteIndex
                        && endByteIndex > searchStartByteIndex)
                    {
                        startByteIndex = searchStartByteIndex + 1;
                    }

                    if (startByteIndex == endByteIndex)
                    {
                        return false;
                    }

                    fileStream.Position =
                        FileDetail.BomLength + startByteIndex;

                    long byteSize = endByteIndex - startByteIndex;

                    bytesCurrent = new byte[byteSize];

                    var readCount = await fileStream.ReadAsync(bytesCurrent, 0, bytesCurrent.Length);

                    bytesCurrent = bytesCurrent.Take(readCount);

                    bytes = bytesCurrent.Concat(bytesLast);

                    bytesLast = bytesCurrent;

                    List<LineReader.ReadLineResult> lineDetails =
                        await LineReader.ReadAllLines(FileDetail, bytes);

                    string text = lineDetails.Select(x => x.Line).Join("");

                    var matches = regex.Matches(text);

                    if (matches.Count > 0)
                    {
                        var lastMatch = matches[matches.Count - 1];

                        var preText = text.Substring(0, lastMatch.Index);

                        var preTextByteIndex = FileDetail.Encoding.GetByteCount(preText);

                        findByteIndex = startByteIndex + preTextByteIndex;
                        findByteCount = FileDetail.Encoding.GetByteCount(lastMatch.Value);

                        return true;
                    }

                    if (startByteIndex == searchStartByteIndex)
                    {
                        return false;
                    }

                    endByteIndex = startByteIndex;
                    startByteIndex = endByteIndex - bufferSize;

                }

            }

        }


        public void StopButtonClicked(object sender, EventArgs e)
        {
            this.Stopped = true;
        }

    }
}
