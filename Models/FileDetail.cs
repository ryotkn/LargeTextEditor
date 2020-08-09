using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LargeTextEditor
{
    class FileDetail
    {

        [ReadOnly(true)]
        public string Name { get; set; }

        [ReadOnly(true)]
        public string Path { get; set; }

        [ReadOnly(true)]
        public long ByteLength { get; set; }

        [ReadOnly(true)]
        public long ViewCharLength { get; set; }

        [ReadOnly(true)]
        public long ViewByteLength { get; set; }

        [ReadOnly(true)]
        public long ViewLineCount { get; set; }

        [ReadOnly(true)]
        public int MaxColumns { get; set; }

        [ReadOnly(true)]
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        [ReadOnly(true)]
        public int BomLength { get; set; } = 0;

        public const int ScrollMaximum = 100;


        public event AsyncEventHandler ViewStartByteIndexUpdated;

        private long ViewStartByteIndex_;

        public long ViewStartByteIndex
        {
            get
            {
                return ViewStartByteIndex_;
            }
            set
            {
                value = value.Limit(0, this.ByteLength);

                if (ViewStartByteIndex_ == value)
                {
                    return;
                }

                ViewStartByteIndex_ = value;

                ViewStartByteIndexUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public int ViewScrollPosition
        {
            get
            {
                long scrollPosition;

                if (ScrollMaximum < ByteLength)
                {
                    scrollPosition = (long)Math.Floor(1.0 * ViewStartByteIndex_ / ByteLength * ScrollMaximum);
                }
                else
                {
                    scrollPosition = ViewStartByteIndex_;
                }

                return (int)scrollPosition;
            }
            set
            {
                long byteIndex;

                if (ScrollMaximum < ByteLength)
                {
                    byteIndex = (long)Math.Floor(1.0 * value / ScrollMaximum * ByteLength);
                }
                else
                {
                    byteIndex = value;
                }

                byteIndex = byteIndex.Limit(0, ByteLength);

                if (ViewStartByteIndex_ == byteIndex)
                {
                    return;
                }

                ViewStartByteIndex_ = byteIndex;

                ViewStartByteIndexUpdated?.Invoke(this, EventArgs.Empty);
            }
        }



        public event AsyncEventHandler CurrentPositionUpdated;


        private long CurrentLine_;

        public long CurrentLine
        {
            get
            {
                return CurrentLine_;
            }
            set
            {
                //value = value.Limit(0, this.Lines - 1);

                if (CurrentLine_ == value)
                {
                    return;
                }

                CurrentLine_ = value;
                CurrentColumn_ = 0;
                CurrentCharLength_ = 0;

                CurrentPositionUpdated?.Invoke(this, EventArgs.Empty);

                //Task.Run(async () =>
                //{
                //    //var lineDetails = FileLineDetails
                //    //    .Where(d => d.LineIndex == CurrentLine_)
                //    //    .ToList();
                //
                //    //var currentLineChars =
                //    //    lineDetails.Sum(d => d.TextLength + d.TerminateChars);
                //
                //    //CurrentColumn_ = CurrentColumn_.Limit(0, currentLineChars - 1);
                //
                //    //var lineDetail = lineDetails
                //    //    .Where(d => d.ColumnIndex <= CurrentColumn_)
                //    //    .LastOrDefault();
                //
                //    //long currentLineCharIndex =
                //    //    lineDetails.FirstOrDefault().CharIndex;
                //
                //    //CurrentCharIndex_ = currentLineCharIndex + CurrentColumn_;
                //
                //    //var byteIndexRef = new Ref<long>();
                //
                //    //await GetByteIndex(
                //    //    lineDetails, CurrentColumn_,
                //    //    byteIndexRef);
                //
                //    //CurrentByteIndex_ = byteIndexRef.Value;
                //
                //    CurrentPositionUpdated?.Invoke(this, EventArgs.Empty);
                //});

            }
        }



        private long CurrentColumn_;

        public long CurrentColumn
        {
            get
            {
                return CurrentColumn_;
            }
            set
            {
                //var currentLineDetails = FileLineDetails
                //    .Where(d => d.LineIndex == CurrentLine_)
                //    .ToList();
                //
                //var currentLineChars =
                //    currentLineDetails.Sum(d => d.TextLength + d.TerminateChars);
                //
                //value = value.Limit(0, currentLineChars - 1);

                if (CurrentColumn_ == value)
                {
                    return;
                }

                CurrentColumn_ = value;

                //Task.Run(async () =>
                //{
                //    long currentLineCharIndex =
                //        currentLineDetails.FirstOrDefault().CharIndex;
                //
                //    CurrentCharIndex_ = currentLineCharIndex + CurrentColumn_;
                //
                //    var byteIndexRef = new Ref<long>();
                //
                //    await GetByteIndex(
                //        currentLineDetails, CurrentColumn_,
                //        byteIndexRef);
                //
                //    CurrentByteIndex_ = byteIndexRef.Value;
                //
                //    CurrentPositionUpdated?.Invoke(this, EventArgs.Empty);
                //});

            }
        }



        private long CurrentCharIndex_;

        public long CurrentCharIndex
        {
            get
            {
                return CurrentCharIndex_;
            }
            set
            {
                value = value.Limit(0, ViewCharLength);

                if (CurrentCharIndex_ == value)
                {
                    return;
                }

                CurrentCharIndex_ = value;

                //Task.Run(async () =>
                //{
                //    var currentLineDetail = FileLineDetails
                //        .Where(d => d.CharIndex <= CurrentCharIndex_)
                //        .LastOrDefault();
                //
                //    CurrentLine_ = currentLineDetail.LineIndex;
                //
                //    var lineDetails = FileLineDetails
                //        .Where(d => d.LineIndex == CurrentLine_)
                //        .ToList();
                //
                //    var lineDetail = lineDetails.FirstOrDefault();
                //
                //    CurrentColumn_ = CurrentCharIndex_ - lineDetail.CharIndex;
                //
                //    var byteIndexRef = new Ref<long>();
                //
                //    await GetByteIndex(
                //        lineDetails, CurrentColumn_,
                //        byteIndexRef);
                //
                //    CurrentByteIndex_ = byteIndexRef.Value;
                //
                //    CurrentPositionUpdated?.Invoke(this, EventArgs.Empty);
                //});

            }
        }



        private long CurrentCharLength_;

        public long CurrentCharLength
        {
            get
            {
                return CurrentCharLength_;
            }
            set
            {
                long maxValue = ViewCharLength - CurrentCharIndex_;
                
                value = value.Limit(0, maxValue);

                if (CurrentCharLength_ == value)
                {
                    return;
                }

                CurrentCharLength_ = value;

                CurrentPositionUpdated?.Invoke(this, EventArgs.Empty);
            }
        }



        private long CurrentByteIndex_;

        public long CurrentByteIndex
        {
            get
            {
                return CurrentByteIndex_;
            }
            set
            {
                value = value.Limit(0, this.ByteLength);

                if (CurrentByteIndex_ == value)
                {
                    return;
                }

                CurrentByteIndex_ = value;

                CurrentPositionUpdated?.Invoke(this, EventArgs.Empty);

                //Task.Run(async () =>
                //{
                //    var lineDetail = FileLineDetails
                //        .Where(d => d.ByteIndex <= CurrentByteIndex_)
                //        .LastOrDefault();
                //
                //    this.CurrentLine_ = lineDetail.LineIndex;
                //
                //    if (lineDetail.ByteIndex < CurrentByteIndex_)
                //    {
                //        await ReadFileAsync(new[] { lineDetail });
                //
                //        var text = lineDetail.Text;
                //
                //        long byteLength = CurrentByteIndex_ - lineDetail.ByteIndex;
                //
                //        var textDest = FileDetail.Encoding.GetString(text, (int)byteLength);
                //
                //        this.CurrentColumn_ = textDest.Length;
                //        this.CurrentCharIndex_ = lineDetail.CharIndex + this.CurrentColumn_;
                //    }
                //    else
                //    {
                //        this.CurrentColumn_ = 0;
                //        this.CurrentCharIndex_ = lineDetail.CharIndex;
                //    }
                //
                //    CurrentPositionUpdated?.Invoke(this, EventArgs.Empty);
                //
                //});
            }
        }



        private long CurrentByteLength_;

        public long CurrentByteLength
        {
            get
            {
                return CurrentByteLength_;
            }
            set
            {
                long maxValue = ByteLength - CurrentByteIndex_;
                
                value = value.Limit(0, maxValue);

                if (CurrentByteLength_ == value)
                {
                    return;
                }

                CurrentByteLength_ = value;

                CurrentPositionUpdated?.Invoke(this, EventArgs.Empty);
            }
        }


        [ReadOnly(true)]
        public FileLineDetails FileLineDetails { get; set; }
            = new FileLineDetails();

        public List<FileLineDetail> GetLineDetails(IEnumerable<string> lines)
        {
            var current = new FileLineDetail();

            var lineDetails = lines.Select((line, lineIndex) =>
            {
                var detail = new FileLineDetail()
                {
                    CharIndex = current.CharIndex,
                    LineIndex = lineIndex,
                    ColumnIndex = 0,
                    TextLength = line.Length,
                    ByteIndex = current.ByteIndex,
                    LineBytes = this.Encoding.GetByteCount(line),
                    Text = line
                };

                current.CharIndex += detail.TextLength + 2;
                current.ByteIndex += detail.LineBytes + 2;

                return detail;

            }).ToList();

            return lineDetails;
        }


        public void GetFilePosition(
            List<FileLineDetail> viewLineDetails,
            long viewCharIndex,
            out long lineIndex, out long columnIndex,
            out long charIndex, out long byteIndex)
        {
            long viewLineIndex = 0;
            long viewColumnIndex = 0;
            string columnText = "";

            var viewLineDetail = viewLineDetails
                .Where(d => d.CharIndex <= viewCharIndex)
                .LastOrDefault();

            if (viewLineDetail != null)
            {
                viewLineIndex = viewLineDetail.LineIndex;
                viewColumnIndex = viewCharIndex - viewLineDetail.CharIndex;

                columnText = viewLineDetail.Text;
            }

            if (viewColumnIndex < columnText.Length)
            {
                columnText = columnText.Substring(0, (int)viewColumnIndex);
            }

            long columnByteIndex = this.Encoding.GetByteCount(columnText);

            if (viewLineIndex >= FileLineDetails.Count)
            {
                lineIndex = 0;
                columnIndex = 0;
                charIndex = 0;
                byteIndex = 0;

                return;
            }

            var fileLineDetail = FileLineDetails[(int)viewLineIndex];

            lineIndex = fileLineDetail.LineIndex;
            columnIndex = fileLineDetail.ColumnIndex + viewColumnIndex;
            charIndex = fileLineDetail.CharIndex + viewColumnIndex;
            byteIndex = fileLineDetail.ByteIndex + columnByteIndex;

        }


        public void GetViewPosition(
            List<FileLineDetail> viewLineDetails,
            long byteIndex, out long charIndex,
            out long lineIndex, out long columnIndex)
        {
            charIndex = 0;
            lineIndex = 0;
            columnIndex = 0;

            long viewByteIndex = byteIndex - this.ViewStartByteIndex;

            var lineDetail = FileLineDetails.Indexed()
                .Where(d => d.value.ByteIndex <= viewByteIndex)
                .LastOrDefault();

            if (lineDetail.value == null
                || lineDetail.index >= viewLineDetails.Count)
            {
                return;
            }

            lineIndex = lineDetail.index;

            var viewLineDetail = viewLineDetails[lineDetail.index];

            var columnByteIndex = viewByteIndex
                - lineDetail.value.ByteIndex;

            var preText = Encoding.GetString(
                viewLineDetail.Text, (int)columnByteIndex);

            columnIndex = preText.Length;

            charIndex = viewLineDetail.CharIndex + columnIndex;

        }


        public async Task DetectBom(FileStream fileStream)
        {
            var bytes = new byte[4];

            var readCount = await fileStream.ReadAsync(bytes, 0, bytes.Length);

            var destBytes = new byte[readCount];

            Array.Copy(bytes, destBytes, readCount);

            bytes = destBytes;


            if ((bytes[0] == 0xff) && (bytes[1] == 0xfe) &&
                (bytes[2] == 0x00) && (bytes[3] == 0x00))
            {
                this.Encoding = new System.Text.UTF32Encoding(false, true); //UTF-32 LE
                this.BomLength = this.Encoding.GetPreamble().Length;

                return;
            }

            else if ((bytes[0] == 0x00) && (bytes[1] == 0x00) &&
                    (bytes[2] == 0xfe) && (bytes[3] == 0xff))
            {
                this.Encoding = new System.Text.UTF32Encoding(true, true); //UTF-32 BE
                this.BomLength = this.Encoding.GetPreamble().Length;

                return;
            }

            else if ((bytes[0] == 0xef) && (bytes[1] == 0xbb) && (bytes[2] == 0xbf))
            {
                this.Encoding = new System.Text.UTF8Encoding(true, true); //UTF-8
                this.BomLength = this.Encoding.GetPreamble().Length;

                return;
            }

            else if ((bytes[0] == 0xfe) && (bytes[1] == 0xff))
            {
                this.Encoding = new System.Text.UnicodeEncoding(true, true); //UTF-16 BE
                this.BomLength = this.Encoding.GetPreamble().Length;

                return;
            }

            else if ((bytes[0] == 0xff) && (bytes[1] == 0xfe))
            {
                this.Encoding = new System.Text.UnicodeEncoding(false, true); //UTF-16 LE
                this.BomLength = this.Encoding.GetPreamble().Length;

                return;
            }

            this.Encoding = Encoding.UTF8;
            this.BomLength = 0;
        }

    }
}
