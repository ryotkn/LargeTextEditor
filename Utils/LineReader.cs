using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargeTextEditor
{
    class LineReader
    {

        const int bufferSize = 1024;
        char[] buffer = new char[bufferSize];
        int maxColumns;
        StreamReader streamReader;
        StringBuilder builder = new StringBuilder();

        public LineReader(
            StreamReader streamReader,
            int maxColumns)
        {
            this.streamReader = streamReader;
            this.maxColumns = maxColumns;
        }

        public class ReadLineResult
        {
            public string Line;
            public string Text;
            public string TerminateChars;

        }

        public async Task<ReadLineResult> ReadLineAsync()
        {
            var length = 0;
            var charIndex = -1;
            var result = new ReadLineResult();

            while (true)
            {
                charIndex++;

                if (charIndex >= builder.Length - 2
                    && streamReader.EndOfStream == false)
                {
                    int readCount = await streamReader.ReadAsync(buffer, 0, bufferSize);

                    if (readCount > 0)
                    {
                        builder.Append(buffer, 0, readCount);
                    }
                }

                if (charIndex >= builder.Length)
                {
                    if (charIndex == 0)
                    {
                        result.Text = "";
                    }
                    else
                    {
                        result.Text = builder.ToString(0, charIndex);

                        builder.Remove(0, charIndex);
                    }

                    result.Line = result.Text;
                    result.TerminateChars = null;

                    return result;
                }

                var c = builder[charIndex];
                var c2 = (charIndex + 1 >= builder.Length) ? 0 : builder[charIndex + 1];

                /// 先頭バイトが欠損すると後続バイトが0xFFFDと変換されるが
                /// 0xFFFDは2バイトと換算され合計バイト数が狂うので、
                /// シングルバイトの '?' に置き換える

                if (c == 0xFFFD)
                {
                    builder[charIndex] = '?';
                }

                if (c2 == 0xFFFD)
                {
                    builder[charIndex + 1] = '?';
                }

                if ('\r' == c && '\n' == c2)
                {
                    result.Text = builder.ToString(0, charIndex);
                    result.Line = builder.ToString(0, charIndex + 2);
                    result.TerminateChars = "\r\n";

                    builder.Remove(0, charIndex + 2);

                    return result;
                }
                else if ('\r' == c || '\n' == c)
                {
                    result.Text = builder.ToString(0, charIndex);
                    result.Line = builder.ToString(0, charIndex + 1);
                    result.TerminateChars = "" + builder[charIndex];

                    builder.Remove(0, charIndex + 1);

                    return result;
                }
                else if (length >= maxColumns)
                {
                    result.Text = builder.ToString(0, charIndex);
                    result.Line = result.Text;
                    result.TerminateChars = null;

                    builder.Remove(0, charIndex);

                    return result;
                }

                if (c >= 0x09 && c <= 0x0d ||
                    c >= 0x20 && c <= 0x7e ||
                    c >= 0xff61 && c <= 0xff9f)
                {
                    length++;
                }
                else
                {
                    length += 2;
                }
            }
        }

        public bool EndOfFile
        {
            get
            {
                return builder.Length == 0
                    && streamReader.EndOfStream;
            }
        }

        public static async Task<List<LineReader.ReadLineResult>> ReadAllLines(
            FileDetail FileDetail,
            byte[] bytes)
        {
            var lines = new List<LineReader.ReadLineResult>();

            using (var memoryStream = new MemoryStream(bytes))
            using (var reader = new StreamReader(memoryStream))
            {
                var LineReader = new LineReader(
                    reader, FileDetail.MaxColumns);

                while (true)
                {
                    var ReadLineResult = await LineReader.ReadLineAsync();

                    lines.Add(ReadLineResult);

                    if (LineReader.EndOfFile)
                    {
                        return lines;
                    }

                }

            }

        }

    }
}
