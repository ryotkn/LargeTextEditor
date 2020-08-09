using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargeTextEditor
{
    [ReadOnly(true)]
    class FileLineDetail
    {

        /// <summary>Index of FileLineDetails</summary>
        public long Index { get; set; }

        /// <summary>Line Index from top of the File</summary>
        public long LineIndex { get; set; }

        /// <summary>Character Index in the Line</summary>
        public long ColumnIndex { get; set; }

        /// <summary>Character Index from top of the File</summary>
        public long CharIndex { get; set; }

        /// <summary>Count of Characters in the Line</summary>
        public long TextLength { get; set; }

        /// <summary>Byte Index from top of the File</summary>
        public long ByteIndex { get; set; }

        /// <summary>Byte Size of Text and Terminater of the Line</summary>
        public long LineBytes { get; set; }

        /// <summary>Text in the Line</summary>
        public string Text { get; set; }

        /// <summary>Terminate Characters of the Line</summary>
        public string TerminateChars { get; set; }

    }
}
