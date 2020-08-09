using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using LargeTextEditor.Properties;
using Newtonsoft.Json;

namespace LargeTextEditor
{
    public partial class MainForm : Form
    {

        private FileDetail FileDetail { get; set; } = new FileDetail();

        private long CurrentViewStartByteIndex = 0;

        private (long viewStartByteIndex, int start, int length) LastSelection;

        private MessageFilter MessageFilter = null;

        private DisposableFlag EventProcessing = new DisposableFlag();

        private System.Windows.Forms.Timer ScrollTimer = new System.Windows.Forms.Timer();

        private long scrollTimerCounter = 0;

        private event EventHandler scrollEvent;

        private const int BookmarkNameMaxLength = 20;

        private const int SearchStatusInterval = 200;


        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private extern static int SendMessage(int hwnd, int msg, int wParam, int lParam);

        private const int WM_CLEAR = 0x0303;


        public MainForm()
        {
            InitializeComponent();

            this.MessageFilter = new MessageFilter();

            Application.AddMessageFilter(this.MessageFilter);

            BindFileDetailEvent(FileDetail);

            txtMain.MouseWheel += txtMain_MouseWheel;

            ScrollTimer.Interval = 100;
            ScrollTimer.Tick += ScrollTimer_Tick;

            InitListViewBookmark();

            propertyGrid1.SelectedObject = FileDetail;

        }

        private void BindFileDetailEvent(FileDetail FileDetail)
        {
            FileDetail.ViewStartByteIndexUpdated += FileDetail_ViewStartByteIndexUpdated;
            FileDetail.CurrentPositionUpdated += FileDetail_CurrentPositionUpdated;
        }

        #region MenuEvent

        private void menuFile_Click(object sender, EventArgs e)
        {
        }

        private void menuNew_Click(object sender, EventArgs e)
        {
            var FileDetail = new FileDetail();

            this.FileDetail = FileDetail;

            txtMain.Clear();

            propertyGrid1.SelectedObject = FileDetail;
            propertyGrid1.Refresh();

        }

        private async void menuOpen_Click(object sender, EventArgs e)
        {
            var dialogResult = openFileDialog1.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            using (EventProcessing.Set(true))
            {
                await OpenFilesAsync(openFileDialog1.FileNames.ToList());
            }

        }

        private async void menuReload_Click(object sender, EventArgs e)
        {
            using (EventProcessing.Set(true))
            {
                await ReadFileAsync(FileDetail);

                RestoreSelection();

                propertyGrid1.Refresh();
            }
        }

        private void menuSave_Click(object sender, EventArgs e)
        {

        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {

        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuEdit_Click(object sender, EventArgs e)
        {
            menuUndo.Enabled = txtMain.CanUndo;
        }

        private void menuUndo_Click(object sender, EventArgs e)
        {
            txtMain.Undo();
        }

        private void menuRedo_Click(object sender, EventArgs e)
        {
        }

        private void menuCut_Click(object sender, EventArgs e)
        {
            if (txtMain.Focused)
            {
                txtMain.Cut();

            }

        }

        private void menuCopy_Click(object sender, EventArgs e)
        {
            if (txtMain.Focused)
            {
                txtMain.Copy();

            }

        }

        private void menuPaste_Click(object sender, EventArgs e)
        {
            if (txtMain.Focused)
            {
                txtMain.Paste();

            }

        }

        private void menuDelete_Click(object sender, EventArgs e)
        {
            if (txtMain.Focused)
            {
                //txtMain.SelectedText = ""; // cannot Undo

                SendMessage((int)txtMain.Handle, WM_CLEAR, 0, 0); // can Undo

            }

        }

        private void menuFind_Click(object sender, EventArgs e)
        {
            txtSearchBox.SelectAll();
            this.ActiveControl = txtSearchBox;
        }

        private void menuFindNext_Click(object sender, EventArgs e)
        {
            btnSearchNext_Click(sender, e);
        }

        private void menuFindPrev_Click(object sender, EventArgs e)
        {
            btnSearchPrev_Click(sender, e);
        }

        private void menuReplace_Click(object sender, EventArgs e)
        {
            txtReplaceBox.SelectAll();
            this.ActiveControl = txtReplaceBox;
        }

        private void menuReplaceNext_Click(object sender, EventArgs e)
        {
            btnReplaceNext_Click(sender, e);
        }

        private void menuReplacePrev_Click(object sender, EventArgs e)
        {
            btnReplacePrev_Click(sender, e);
        }

        private void menuSelectLine_Click(object sender, EventArgs e)
        {
            if (txtMain.Focused == false)
            {
                return;
            }

            long start = txtMain.SelectionStart;
            long end = start + txtMain.SelectionLength;

            var lines = txtMain.Lines.DefaultIfEmpty("");

            var lineDetails = FileDetail.GetLineDetails(lines);

            var startLineDetail = lineDetails.Where(d => d.CharIndex <= start).LastOrDefault();
            start = startLineDetail.CharIndex;

            var endLineDetail = lineDetails.Where(d => d.CharIndex <= end).LastOrDefault();
            end = endLineDetail.CharIndex + lines[endLineDetail.LineIndex].Length;

            txtMain.SelectionStart = (int)start;
            txtMain.SelectionLength = (int)(end - start);

        }

        #endregion


        #region SearchEvent

        private async void btnSearchNext_Click(object sender, EventArgs e)
        {
            await SearchExecute(SearchManager.Direction.Forward);
        }

        private async void btnSearchPrev_Click(object sender, EventArgs e)
        {
            await SearchExecute(SearchManager.Direction.Backward);
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {

        }

        private void btnReplaceNext_Click(object sender, EventArgs e)
        {

        }

        private void btnReplacePrev_Click(object sender, EventArgs e)
        {

        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {

        }

        #endregion


        #region OtherEvent

        private async void MainForm_Load(object sender, EventArgs e)
        {
            var files = Environment.GetCommandLineArgs().Skip(1)
                .Where(arg => (arg.StartsWith("/") || arg.StartsWith("-")) == false)
                .ToList();

            if (files.Count == 0)
            {
                return;
            }

            using (EventProcessing.Set(true))
            {
                await OpenFilesAsync(files);
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.RemoveMessageFilter(this.MessageFilter);
        }

        private async void txtMain_MouseWheel(object sender, MouseEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine($"{DateTime.Now} txtMain_MouseWheel Delta: {e.Delta}");

            var ScrollBytes = Settings.Default.ScrollBytes;

            long value = FileDetail.ViewStartByteIndex -
                ScrollBytes * e.Delta / 120;

            value = value.Limit(
                0, FileDetail.ByteLength);

            if (FileDetail.ViewStartByteIndex == value)
            {
                return;
            }

            using (EventProcessing.Set(true))
            {
                FileDetail.ViewStartByteIndex = value;

                await ReadFileAsync(FileDetail);

                RestoreSelection();

                vScrollBar1.Value = FileDetail.ViewScrollPosition;

                propertyGrid1.Refresh();
            }
        }

        private void txtMain_KeyDown(object sender, KeyEventArgs e)
        {
            toolStripStatusLabel1.Text = "";

            Debug.WriteLine("");
            Debug.WriteLine("txtMain_KeyDown");


        }

        private void txtMain_KeyUp(object sender, KeyEventArgs e)
        {
            toolStripStatusLabel1.Text = "";

            Debug.WriteLine("");
            Debug.WriteLine("txtMain_KeyUp");

            txtMain_SelectionChanged(sender, null);
        }

        private void txtMain_MouseDown(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "";

            Debug.WriteLine("");
            Debug.WriteLine("txtMain_MouseDown. " +
                $"MouseEventArgs: {JsonConvert.SerializeObject(e)}");


        }

        private void txtMain_MouseUp(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "";

            Debug.WriteLine("");
            Debug.WriteLine("txtMain_MouseUp. " +
                $"MouseEventArgs: {JsonConvert.SerializeObject(e)}");

            txtMain_SelectionChanged(sender, null);
        }

        private void txtMain_SelectionChanged(object sender, MouseEventArgs e)
        {
            long start = txtMain.SelectionStart;
            long length = txtMain.SelectionLength;
            long end = start + length;

            Debug.WriteLine("");
            Debug.WriteLine($"SelectionChanged " +
                $"start: {start}, length: {length}");

            if (EventProcessing.Value)
            {
                Debug.WriteLine("SelectionChanged Skipped");
                return;
            }

            if (LastSelection.viewStartByteIndex == CurrentViewStartByteIndex
                && LastSelection.start == start
                && LastSelection.length == length)
            {
                return;
            }

            using (EventProcessing.Set(true))
            {
                //Debug.WriteLine("");
                //Debug.WriteLine("SelectionChanged Value Changed");

                var lines = txtMain.Lines;

                var viewLineDetails = FileDetail.GetLineDetails(lines);

                FileDetail.GetFilePosition(
                    viewLineDetails, start,
                    out long startLineIndex, out long startColumnIndex,
                    out long startCharIndex, out long startByteIndex);

                FileDetail.GetFilePosition(
                    viewLineDetails, end,
                    out long endLineIndex, out long endColumnIndex,
                    out long endCharIndex, out long endByteIndex);

                Debug.WriteLine($"SelectionChanged " +
                    $"startLineIndex: {startLineIndex}, " +
                    $"startColumnIndex: {startColumnIndex}");

                Debug.WriteLine($"SelectionChanged " +
                    $"endLineIndex: {endLineIndex}, " +
                    $"endColumnIndex: {endColumnIndex}");

                FileDetail.CurrentLine = startLineIndex;
                FileDetail.CurrentColumn = startColumnIndex;
                FileDetail.CurrentCharIndex = startCharIndex;
                FileDetail.CurrentCharLength = endCharIndex - startCharIndex;
                FileDetail.CurrentByteIndex = FileDetail.ViewStartByteIndex + startByteIndex;
                FileDetail.CurrentByteLength = endByteIndex - startByteIndex;

                Debug.WriteLine($"SelectionChanged " +
                    $"CurrentLine: {FileDetail.CurrentLine}, " +
                    $"CurrentColumn: {FileDetail.CurrentColumn}, " +
                    $"CurrentCharIndex: {FileDetail.CurrentCharIndex}, " +
                    $"CurrentLength: {FileDetail.CurrentCharLength}, " +
                    $"CurrentByteIndex: {FileDetail.CurrentByteIndex}, " +
                    $"CurrentByteLength: {FileDetail.CurrentByteLength}");

                propertyGrid1.Refresh();

                LastSelection.viewStartByteIndex = CurrentViewStartByteIndex;
                LastSelection.start = (int)start;
                LastSelection.length = (int)length;

            }

        }

        private void txtSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.BeginInvoke((Action)(() =>
                {
                    btnSearchNext.PerformClick();
                }));
                
            }
        }

        private void txtReplaceBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.BeginInvoke((Action)(() =>
                {
                    btnReplaceNext.PerformClick();
                }));

            }
        }

        private async void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Debug.WriteLine("");
            Debug.WriteLine($"vScrollBar1_Scroll " +
                $"ViewScrollPosition: {FileDetail.ViewScrollPosition}, " +
                $"NewValue: {e.NewValue}, " +
                $"OldValue: {e.OldValue}, " +
                $"ScrollOrientation: {e.ScrollOrientation}");

            if (EventProcessing.Value
                || FileDetail.ViewScrollPosition == e.NewValue)
            {
                return;
            }

            using (EventProcessing.Set(true))
            {
                FileDetail.ViewScrollPosition = e.NewValue;

                await ReadFileAsync(FileDetail);

                RestoreSelection();

                propertyGrid1.Refresh();
            }

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            var ScrollBytes = Settings.Default.ScrollBytes;

            FileDetail.ViewStartByteIndex -= ScrollBytes;

        }

        private void btnUp_MouseClick(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("btnUp_MouseClick");
        }

        private void btnUp_MouseDown(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("btnUp_MouseDown");

            scrollEvent += btnUp_Click;

            ScrollTimer.Start();
        }

        private void btnUp_MouseUp(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("btnUp_MouseUp");

            scrollEvent -= btnUp_Click;

            ScrollTimer.Stop();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            var ScrollBytes = Settings.Default.ScrollBytes;

            FileDetail.ViewStartByteIndex += ScrollBytes;

        }

        private void btnDown_MouseClick(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("btnDown_MouseClick");
        }

        private void btnDown_MouseDown(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("btnDown_MouseDown");

            scrollEvent += btnDown_Click;

            ScrollTimer.Start();
        }

        private void btnDown_MouseUp(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("btnDown_MouseUp");

            scrollEvent -= btnDown_Click;

            ScrollTimer.Stop();
        }

        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            scrollTimerCounter++;

            Debug.WriteLine($"ScrollTimer_Tick scrollTimerCounter: {scrollTimerCounter}");

            scrollEvent?.Invoke(this, EventArgs.Empty);

        }

        private void btnBookmarkAdd_Click(object sender, EventArgs e)
        {
            int index = 0;
            string baseName = "Name";
            string name = "";

            if (txtMain.SelectionLength > 0)
            {
                var text = txtMain.SelectedText;

                baseName = (text.Length <= BookmarkNameMaxLength) ?
                    text : text.Substring(0, BookmarkNameMaxLength) + "..";
            }
            else
            {
                var startIndex = txtMain.SelectionStart;

                var lines = txtMain.Lines;

                var lineDetails = FileDetail.GetLineDetails(lines);

                var lineDetail = lineDetails
                    .Where(d => d.CharIndex < startIndex)
                    .LastOrDefault();

                var text = lineDetail.Text?.Trim();

                if (text.Length > 0)
                {
                    baseName = text;

                    if (text.Length <= BookmarkNameMaxLength)
                    {
                        baseName = text.Substring(0, BookmarkNameMaxLength) + "..";
                    }
                }

            }

            while (true)
            {
                index++;

                name = baseName + index;

                var ListViewItems = listViewBookmark.Items.AsEnumerable();

                if (ListViewItems.Any(item => item.Name == name))
                {
                    continue;
                }

                break;
            }

            var byteIndex = FileDetail.CurrentByteIndex;

            var ListViewItem = new ListViewItem(new[] { name, $"{byteIndex}" });

            ListViewItem.Name = name;

            listViewBookmark.Items.Add(ListViewItem);

        }

        private void btnBookmarkDelete_Click(object sender, EventArgs e)
        {
            var SelectedItems = listViewBookmark.SelectedItems.AsEnumerable().ToList();

            SelectedItems.RemoveAll(v => v.Index == 0 || v.Index == 1);

            if (SelectedItems.Count < 1)
            {
                return;
            }

            for (int index = 0; index < SelectedItems.Count; index++)
            {
                var SelectedItem = SelectedItems[0];

                SelectedItem.Remove();

            }

        }


        private async void listViewBookmark_DoubleClick(object sender, EventArgs e)
        {
            var SelectedItems = listViewBookmark.SelectedItems;

            if (SelectedItems.Count < 1)
            {
                return;
            }

            var SelectedItem = SelectedItems[0];

            var SubItem = SelectedItem.SubItems[1];

            if (SubItem.Text == null ||
                false == long.TryParse(SubItem.Text, out long byteIndex))
            {
                return;
            }

            if (byteIndex < 0)
            {
                byteIndex += FileDetail.ByteLength + 1;
            }

            byteIndex = byteIndex.Limit(
                0, FileDetail.ByteLength);

            using (EventProcessing.Set(true))
            {
                await MoveTo(byteIndex, 0);

                propertyGrid1.Refresh();

                this.ActiveControl = txtMain;
            }

        }


        public async Task FileDetail_ViewStartByteIndexUpdated(object sender, EventArgs e)
        {
            Debug.WriteLine("");
            Debug.WriteLine($"ViewStartByteIndexUpdated: {FileDetail.ViewStartByteIndex}");

            if (EventProcessing.Value)
            {
                Debug.WriteLine(" ViewStartByteIndexUpdated Skipped");
                return;
            }

            using (EventProcessing.Set(true))
            {
                await ReadFileAsync(FileDetail);

                int scrollValue = FileDetail.ViewScrollPosition;

                if (vScrollBar1.Value != scrollValue)
                {
                    vScrollBar1.Value = (int)scrollValue;
                }

                RestoreSelection();

                propertyGrid1.Refresh();
            }
        }

        public async Task FileDetail_CurrentPositionUpdated(object sender, EventArgs e)
        {
            Debug.WriteLine("");
            Debug.WriteLine($"CurrentPositionUpdated");

            if (EventProcessing.Value)
            {
                Debug.WriteLine(" CurrentPositionUpdated Skipped");
                return;
            }

            using (EventProcessing.Set(true))
            {
                await MoveTo(
                    FileDetail.CurrentByteIndex,
                    FileDetail.CurrentByteLength);

                propertyGrid1.Refresh();

                this.BeginInvoke((Action)(() =>
                {
                    txtMain.Focus();
                }));
            }
        }

        #endregion



        private async Task OpenFilesAsync(List<string> FileNames)
        {
            Debug.WriteLine("");
            Debug.WriteLine($"OpenFilesAsync FileNames: {FileNames}");

            var filesNotExists = FileNames.Where(file => false == File.Exists(file)).ToList();

            if (0 < filesNotExists.Count
                && FileNames.Count == filesNotExists.Count)
            {
                var ret = MessageBox.Show("ファイルが存在しません。\r\n\r\n" +
                    filesNotExists.Join("\r\n"));

                return;
            }
            else if (0 < filesNotExists.Count
                && FileNames.Count > filesNotExists.Count)
            {
                var ret = MessageBox.Show(
                    "次のファイルが存在しません。\r\n" +
                    "残りのファイルを開きますか？\r\n\r\n" +
                    filesNotExists.Join("\r\n"),
                    "",
                    MessageBoxButtons.YesNo);

                if (ret != DialogResult.Yes)
                {
                    return;
                }
            }

            var thisApp = Environment.GetCommandLineArgs().First();

            foreach (var FileNameEach in FileNames.Skip(1))
            {
                Process.Start(thisApp, $@"""{FileNameEach}""");
            }

            var FileName = FileNames.FirstOrDefault();

            if (FileName != null)
            {
                await OpenFileAsync(FileName);
            }
        }

        private async Task ReadFileAsync(FileDetail FileDetail)
        {
            Debug.WriteLine("");
            Debug.WriteLine($"ReadFileAsync ViewStartByteIndex: {FileDetail.ViewStartByteIndex}");

            if (FileDetail.Path == null)
            {
                return;
            }

            int MaxColumns = Settings.Default.MaxColumns;

            this.CurrentViewStartByteIndex = FileDetail.ViewStartByteIndex;

            var Lines = new Ref<List<string>>();

            using (var reader =
                new StreamReader(FileDetail.Path))
            {
                FileDetail.ByteLength = reader.BaseStream.Length;

                reader.BaseStream.Position =
                    FileDetail.BomLength +
                    FileDetail.ViewStartByteIndex;

                var LineReader = new LineReader(reader, MaxColumns);

                await ReadFileAsync(LineReader, FileDetail, Lines);
            }

            txtMain.Lines = Lines.Value.ToArray();
            txtMain.SelectionLength = 0;

            //txtMain_SelectionChanged(sender, null);

        }

        private async Task ReadFileAsync(
            LineReader LineReader,
            FileDetail FileDetail,
            Ref<List<string>> Lines)
        {
            Debug.WriteLine("");
            Debug.WriteLine($"ReadFileAsync");

            long lineIndex = 0;
            long columnIndex = 0;
            long charIndex = 0;
            long byteIndex = 0;

            int CacheLines = Settings.Default.CacheLines;

            int lineCount = 0;
            var lines = new List<string>();
            int readCounter = -1;

            FileDetail.FileLineDetails.Clear();

            while (true)
            {
                readCounter++;

                //Debug.WriteLine($" LineReader.ReadLineAsync " +
                //    $"readCounter: {readCounter}, " +
                //    $"lineIndex: {lineIndex}, " +
                //    $"columnIndex: {columnIndex}, " +
                //    $"charIndex: {charIndex}, " +
                //    $"byteIndex: {byteIndex}");

                var ReadLineResult = await LineReader.ReadLineAsync();

                if (readCounter == 0 &&
                    ReadLineResult.TerminateChars != null &&
                    FileDetail.ViewStartByteIndex != 0)
                {
                    FileDetail.ViewStartByteIndex +=
                        FileDetail.Encoding.GetByteCount(ReadLineResult.Line);
                    continue;
                }

                lines.Add(ReadLineResult.Text);

                var FileLineDetail = new FileLineDetail()
                {
                    Index = FileDetail.FileLineDetails.Count,
                    ByteIndex = byteIndex,
                    CharIndex = charIndex,
                    LineIndex = lineIndex,
                    ColumnIndex = columnIndex,
                    TextLength = ReadLineResult.Text.Length,
                    Text = ReadLineResult.Text,
                    TerminateChars = ReadLineResult.TerminateChars,
                    LineBytes = FileDetail.Encoding.GetByteCount(ReadLineResult.Line)
                };

                FileDetail.FileLineDetails.Add(FileLineDetail);

                lineCount++;

                FileDetail.ViewLineCount = lineCount;
                FileDetail.ViewCharLength = charIndex + ReadLineResult.Line.Length;
                FileDetail.ViewByteLength = byteIndex + FileLineDetail.LineBytes;

                if (LineReader.EndOfFile
                    || lineCount >= CacheLines)
                {
                    break;
                }

                if (ReadLineResult.TerminateChars != null)
                {
                    lineIndex++;
                    columnIndex = 0;
                }
                else
                {
                    columnIndex += ReadLineResult.Text.Length;
                }

                byteIndex += FileLineDetail.LineBytes;
                charIndex += ReadLineResult.Line.Length;

            }

            Lines.Value = lines;
        }

        private async Task OpenFileAsync(string FileName)
        {
            Debug.WriteLine("");
            Debug.WriteLine($"OpenFileAsync FileName: {FileName}");

            int MaxColumns = Settings.Default.MaxColumns;

            var FileDetail = new FileDetail();
            FileDetail.Path = FileName;
            FileDetail.Name = Path.GetFileName(FileName);
            FileDetail.MaxColumns = MaxColumns;

            var FileLineDetails = new FileLineDetails();
            FileDetail.FileLineDetails = FileLineDetails;

            this.FileDetail = FileDetail;

            propertyGrid1.SelectedObject = FileDetail;
            propertyGrid1.Refresh();

            BindFileDetailEvent(FileDetail);

            vScrollBar1.Value = 0;

            var Lines = new Ref<List<string>>();

            using (var fileStream = File.OpenRead(FileName))
            {
                FileDetail.ByteLength = fileStream.Length;

                await FileDetail.DetectBom(fileStream);

                fileStream.Position = FileDetail.BomLength;

                using (var reader =
                    new StreamReader(fileStream))
                {
                    var LineReader = new LineReader(reader, MaxColumns);

                    await ReadFileAsync(LineReader, FileDetail, Lines);
                }
            }

            txtMain.Lines = Lines.Value.ToArray();
            txtMain.SelectionLength = 0;

            InitListViewBookmark();

        }


        private async Task MoveTo(
            long findByteIndex,
            long findByteCount)
        {
            var ScrollBytes = Settings.Default.ScrollBytes;

            if ((findByteIndex < FileDetail.ViewStartByteIndex)
                || (findByteIndex + findByteCount >
                FileDetail.ViewStartByteIndex + FileDetail.ViewByteLength))
            {
                FileDetail.ViewStartByteIndex =
                    (findByteIndex - ScrollBytes).Min(0);

                await ReadFileAsync(FileDetail);
            }

            var lines = txtMain.Lines;

            var viewLineDetails = FileDetail.GetLineDetails(lines);

            FileDetail.GetViewPosition(
                viewLineDetails, findByteIndex,
                out long startFindCharIndex,
                out long startLineIndex, out long startColumnIndex);

            FileDetail.GetViewPosition(
                viewLineDetails, findByteIndex + findByteCount,
                out long endFindCharIndex,
                out long endLineIndex, out long endColumnIndex);

            long charCount = endFindCharIndex - startFindCharIndex;

            Debug.Print($"MoveTo " +
                $"ViewStartByteIndex: {FileDetail.ViewStartByteIndex}, " +
                $"findCharIndex: {startFindCharIndex}, charCount: {charCount}, " +
                $"lineIndex: {startLineIndex}, columnIndex: {startColumnIndex}");

            txtMain.SelectionStart = (int)startFindCharIndex;
            txtMain.SelectionLength = (int)charCount;

            LastSelection.start = (int)startFindCharIndex;
            LastSelection.length = (int)charCount;

            FileDetail.CurrentByteIndex = findByteIndex;
            FileDetail.CurrentByteLength = findByteCount;
            FileDetail.CurrentCharIndex = startFindCharIndex;
            FileDetail.CurrentCharLength = charCount;
            FileDetail.CurrentLine = startLineIndex;
            FileDetail.CurrentColumn = startColumnIndex;

            vScrollBar1.Value = FileDetail.ViewScrollPosition;

        }


        private void InitListViewBookmark()
        {
            listViewBookmark.Items.Clear();


            var ListViewItem = new ListViewItem("ファイル先頭");

            ListViewItem.Name = ListViewItem.Text;
            ListViewItem.SubItems.Add("0");

            listViewBookmark.Items.Add(ListViewItem);


            ListViewItem = new ListViewItem("ファイル末尾");

            ListViewItem.Name = ListViewItem.Text;
            ListViewItem.SubItems.Add("-1");

            listViewBookmark.Items.Add(ListViewItem);

        }


        private Regex GetSearchRegex()
        {
            Regex regex;

            var searchText = txtSearchBox.Text;

            Debug.Print($"SearchText: {searchText}, " +
                $"UpperLower: {chkSearchUpperLower.Checked}, " +
                $"AsWord: {chkSearchWord.Checked}");

            if (chkSearchRegex.Checked)
            {
                try
                {
                    RegexOptions options = RegexOptions.None;

                    if (chkSearchUpperLower.Checked == false)
                    {
                        options |= RegexOptions.IgnoreCase;
                    }

                    if (chkSearchWord.Checked)
                    {
                        searchText = "\\b" + searchText + "\\b";
                    }

                    regex = new Regex(searchText, options);
                }
                catch
                {
                    toolStripStatusLabel1.Text = "正規表現が不正です。";
                    return null;
                }
            }
            else
            {
                searchText = searchText.Escape();

                RegexOptions options = RegexOptions.Multiline;

                if (chkSearchUpperLower.Checked == false)
                {
                    options |= RegexOptions.IgnoreCase;
                }

                if (chkSearchWord.Checked)
                {
                    searchText = "\\b" + searchText + "\\b";
                }

                regex = new Regex(searchText, options);
            }

            return regex;
        }


        private async Task SearchExecute(SearchManager.Direction Direction)
        {
            toolStripStatusLabel1.Text = "";

            var startByteIndex = FileDetail.CurrentByteIndex;

            var byteLength = FileDetail.Encoding
                .GetByteCount(txtMain.SelectedText);

            Debug.Print($"btnSearchNext_Click " +
                $"CurrentByteIndex: {FileDetail.CurrentByteIndex}, " +
                $"startByteIndex: {startByteIndex}, " +
                $"byteLength: {byteLength}");

            if (FileDetail.Path == null)
            {
                Debug.Print("Search Stopped.");

                toolStripStatusLabel1.Text = "ファイルが開かれていません。";

                return;
            }

            Regex searchRegex = GetSearchRegex();

            var SearchManager = new SearchManager(
                FileDetail,
                chkSearchRegex.Checked,
                chkSearchUpperLower.Checked,
                chkSearchWord.Checked);

            var Stopwatch = new Stopwatch();

            var timer = new System.Timers.Timer(SearchStatusInterval);

            timer.Elapsed += new System.Timers.ElapsedEventHandler(
                (sender1, e1) =>
                {
                    toolStripStatusLabel1.Text = "検索中... " +
                        $"Time: {Stopwatch.Elapsed.ToString("hh\\:mm\\:ss\\.ff")}, " +
                        $"Index: {SearchManager.byteIndex}";
                });

            bool matched = false;

            try
            {
                btnSearchStop.Click += SearchManager.StopButtonClicked;

                btnSearchNext.Enabled = false;
                btnSearchPrev.Enabled = false;
                //btnSearchAll.Enabled = false;
                btnSearchStop.Enabled = true;

                toolStripStatusLabel1.Text = "検索中...";

                Stopwatch.Start();
                timer.Start();

                await Task.Run(async () =>
                {
                    if (Direction == SearchManager.Direction.Forward)
                    {
                        matched = await SearchManager.FindNext(
                            searchRegex,
                            startByteIndex);
                    }
                    else if (Direction == SearchManager.Direction.Backward)
                    {
                        matched = await SearchManager.FindPrev(
                            searchRegex,
                            startByteIndex);
                    }
                    else
                    {
                        throw new NotSupportedException("Invalid Search Direction");
                    }
                });

                timer.Stop();
                Stopwatch.Stop();

                if (matched == false)
                {
                    if (SearchManager.Stopped)
                    {
                        toolStripStatusLabel1.Text = "検索が中断されました。" +
                            $"Time: {Stopwatch.Elapsed.ToString("hh\\:mm\\:ss\\.ff")}, " +
                            $"Index: {SearchManager.byteIndex}";
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "検索対象が見つかりません。" +
                            $"Time: {Stopwatch.Elapsed.ToString("hh\\:mm\\:ss\\.ff")}";
                    }

                    txtMain.Focus();
                    return;
                }

                long findByteIndex = SearchManager.findByteIndex;
                long findByteCount = SearchManager.findByteCount;

                Debug.Print($"btnSearchNext_Click " +
                    $"findByteIndex: {findByteIndex}, " +
                    $"findByteCount: {findByteCount}, " +
                    $"ViewStartByteIndex: {FileDetail.ViewStartByteIndex}, " +
                    $"ViewByteLength: {FileDetail.ViewByteLength}");

                toolStripStatusLabel1.Text = "検索完了 " +
                        $"Time: {Stopwatch.Elapsed.ToString("hh\\:mm\\:ss\\.ff")}, " +
                        $"Index: {findByteIndex}";

                using (EventProcessing.Set(true))
                {
                    await MoveTo(findByteIndex, findByteCount);

                    txtMain.Focus();

                    propertyGrid1.Refresh();
                }

            }
            finally
            {
                timer.Stop();
                Stopwatch.Stop();

                btnSearchStop.Click -= SearchManager.StopButtonClicked;

                btnSearchNext.Enabled = true;
                btnSearchPrev.Enabled = true;
                //btnSearchAll.Enabled = true;
                btnSearchStop.Enabled = false;

            }

        }


        private void RestoreSelection()
        {
            var lines = txtMain.Lines;

            var viewLineDetails = FileDetail.GetLineDetails(lines);

            FileDetail.GetViewPosition(
                viewLineDetails,
                FileDetail.CurrentByteIndex,
                out long startCharIndex,
                out long startLineIndex, out long startColumnIndex);

            FileDetail.GetViewPosition(
                viewLineDetails,
                FileDetail.CurrentByteIndex +
                FileDetail.CurrentByteLength,
                out long endCharIndex,
                out long endLineIndex, out long endColumnIndex);

            long charCount = endCharIndex - startCharIndex;

            FileDetail.CurrentCharIndex = startCharIndex;
            FileDetail.CurrentCharLength = charCount;
            FileDetail.CurrentLine = startLineIndex;
            FileDetail.CurrentColumn = startColumnIndex;

            txtMain.SelectionStart = (int)startCharIndex;
            txtMain.SelectionLength = (int)charCount;

        }

    }
}
