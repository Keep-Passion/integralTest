using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Arm_tyshkj_design
{
    public class Report  
    {  
        private _Application wordApp = null;  
        private _Document wordDoc = null;  
        public _Application Application  
        {  
            get  
            {  
                return wordApp;  
            }  
            set  
            {  
                wordApp = value;  
            }  
        }  
        public _Document Document  
        {  
            get  
            {  
                return wordDoc;  
            }  
            set  
            {  
                wordDoc = value;  
            }  
        }  
  
        //通过模板创建新文档  
        public void CreateNewDocument(string filePath)  
        {  
            killWinWordProcess();  
            wordApp = new Microsoft.Office.Interop.Word.Application();  
            wordApp.DisplayAlerts = WdAlertLevel.wdAlertsNone;  
            wordApp.Visible = false;  
            object missing = System.Reflection.Missing.Value;  
            object templateName = filePath;  
            wordDoc = wordApp.Documents.Open(ref templateName, ref missing,  
                ref missing, ref missing, ref missing, ref missing, ref missing,  
                ref missing, ref missing, ref missing, ref missing, ref missing,  
                ref missing, ref missing, ref missing, ref missing);  
        }  

        //
        public void killWinWordProcess()
        {

            System.Diagnostics.Process[] processes =

            System.Diagnostics.Process.GetProcessesByName("WINWORD");

            foreach (System.Diagnostics.Process process in processes)
            {
                bool b = process.MainWindowTitle == "";
                if (process.MainWindowTitle == "")
                {
                    process.Kill();
                }
            }
        }
  
        //保存新文件  
        public void SaveDocument(string filePath, String saFileName)  
        {  
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "word文件|*.doc";
            saveDlg.FileName = saFileName;
            
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                String lll = saveDlg.FileName;
                object fileName = lll;  
            object format = WdSaveFormat.wdFormatDocument;//保存格式  
            object miss = System.Reflection.Missing.Value;  
            wordDoc.SaveAs(ref fileName, ref format, ref miss,  
                ref miss, ref miss, ref miss, ref miss,  
                ref miss, ref miss, ref miss, ref miss,  
                ref miss, ref miss, ref miss, ref miss,  
                ref miss);  
            //关闭wordDoc，wordApp对象  
            object SaveChanges = WdSaveOptions.wdSaveChanges;  
            object OriginalFormat = WdOriginalFormat.wdOriginalDocumentFormat;  
            object RouteDocument = false;  
            wordDoc.Close(ref SaveChanges, ref OriginalFormat, ref RouteDocument);  
            wordApp.Quit(ref SaveChanges, ref OriginalFormat, ref RouteDocument);  
            }
            
        }


        public Table InsertTable(string bookmark, int rows, int columns, float width)  
        {  
            object miss = System.Reflection.Missing.Value;  
            object oStart = bookmark;  
            Range range = wordDoc.Bookmarks.get_Item(ref oStart).Range;//表格插入位置  
            Table newTable = wordDoc.Tables.Add(range, rows, columns, ref miss, ref miss);  
            //设置表的格式  
            newTable.Borders.Enable = 1;  //允许有边框，默认没有边框(为0时报错，1为实线边框，2、3为虚线边框，以后的数字没试过)  
            newTable.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth050pt;//边框宽度  
            if (width != 0)  
            {  
                newTable.PreferredWidth = width;//表格宽度  
            }  
            newTable.AllowPageBreaks = false;  
            return newTable;  
        }
        //设置表格内容对齐方式 Align水平方向，Vertical垂直方向(左对齐，居中对齐，右对齐分别对应Align和Vertical的值为-1,0,1)  
        public void SetParagraph_Table(Microsoft.Office.Interop.Word.Table table, int Align, int Vertical)
        {
            switch (Align)
            {
                case -1: table.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft; break;//左对齐  
                case 0: table.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter; break;//水平居中  
                case 1: table.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight; break;//右对齐  
            }
            switch (Vertical)
            {
                case -1: table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalTop; break;//顶端对齐  
                case 0: table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter; break;//垂直居中  
                case 1: table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalBottom; break;//底端对齐  
            }
        }

        public void Table_Merge(Microsoft.Office.Interop.Word.Table table, int left, int right)
        {
            table.Cell(left, 1).Merge(table.Cell(right, 1));
        }
        public void Table_Merge_2(Microsoft.Office.Interop.Word.Table table, int left, int right)
        {
            table.Cell(left, 2).Merge(table.Cell(right, 2));
        }

        public void Table_Merge_kaka(Microsoft.Office.Interop.Word.Table table, int left, int right)
        {
            try
            {
                string per = table.Cell(left, 2).Range.Text;
                for (int i = left; i <= right; i++)
                {
                    table.Cell(i, 2).Range.Text = "";
                }
                table.Cell(left, 2).Merge(table.Cell(right, 2));
                table.Cell(left, 2).Range.Text = per;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Table_Merge_kk(Microsoft.Office.Interop.Word.Table table, int up, int down)
        {
            try
            {
                string per = table.Cell(up, 1).Range.Text;
                for (int i = up; i <= down; i++)
                {
                    table.Cell(i, 1).Range.Text = "";
                }
                table.Cell(up, 1).Merge(table.Cell(down, 1));
                table.Cell(up, 1).Range.Text = per;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Table_Merge_Row(Microsoft.Office.Interop.Word.Table table, int left, int right)
        {
            try
            {
                table.Cell(1, left).Merge(table.Cell(1, right));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //设置表格字体  
        public void SetFont_Table(Microsoft.Office.Interop.Word.Table table, string fontName, double size)
        {
            if (size != 0)
            {
                table.Range.Font.Size = Convert.ToSingle(size);
            }
            if (fontName != "")
            {
                table.Range.Font.Name = fontName;
            }
        }

        //是否使用边框,n表格的序号,use是或否  
        public void UseBorder(int n, bool use)
        {
            if (use)
            {
                wordDoc.Content.Tables[n].Borders.Enable = 1;  //允许有边框，默认没有边框(为0时报错，1为实线边框，2、3为虚线边框，以后的数字没试过)  
            }
            else
            {
                wordDoc.Content.Tables[n].Borders.Enable = 2;  //允许有边框，默认没有边框(为0时报错，1为实线边框，2、3为虚线边框，以后的数字没试过)  
            }
        }

        //给表格插入一行,n表格的序号从1开始记  
        public void AddRow(int n)
        {
            object miss = System.Reflection.Missing.Value;
            wordDoc.Content.Tables[n].Rows.Add(ref miss);
        }

        //给表格添加一行  
        public void AddRow(Microsoft.Office.Interop.Word.Table table)
        {
            object miss = System.Reflection.Missing.Value;
            table.Rows.Add(ref miss);
        }

        //给表格插入rows行,n为表格的序号  
        public void AddRow(int n, int rows)
        {
            object miss = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Table table = wordDoc.Content.Tables[n];
            for (int i = 0; i < rows; i++)
            {
                table.Rows.Add(ref miss);
            }
        }

        //给表格中单元格插入元素，table所在表格，row行号，column列号，value插入的元素  
        public void InsertCell(Microsoft.Office.Interop.Word.Table table, int row, int column, string value)
        {
            table.Cell(row, column).Range.Text = value;
        }

        public void PreProcess(Microsoft.Office.Interop.Word.Table table)
        {
            table.Cell(1, 1).Range.Text = "说明";
            table.Cell(1, 3).Range.Text = "整检装置示值";
            table.Cell(2, 3).Range.Text = "比值差";
            
            table.Cell(2, 4).Range.Text = "相位差";
            table.Cell(1, 5).Range.Text = "受检仪器示值";
            table.Cell(2, 5).Range.Text = "比值差";
            table.Cell(2, 6).Range.Text = "相位差";
            table.Cell(1, 7).Range.Text = "误差";
            table.Cell(2, 7).Range.Text = "比值差";
            table.Cell(2, 8).Range.Text = "相位差";
            table.Cell(1, 9).Range.Text = "允许误差（±）";
            table.Cell(2, 9).Range.Text = "比值差";
            table.Cell(2, 10).Range.Text = "相位差";
             
        }

        //给表格中单元格插入元素，n表格的序号从1开始记，row行号，column列号，value插入的元素  
        public void InsertCell(int n, int row, int column, string value)
        {
            wordDoc.Content.Tables[n].Cell(row, column).Range.Text = value;
        }

        //给表格插入一行数据，n为表格的序号，row行号，columns列数，values插入的值  
        public void InsertCell(int n, int row, int columns, string[] values)
        {
            Microsoft.Office.Interop.Word.Table table = wordDoc.Content.Tables[n];
            for (int i = 0; i < columns; i++)
            {
                table.Cell(row, i + 1).Range.Text = values[i];
            }
        }

        //插入书签
        public void insertBookmark(string bookmark, string value)
        {
            try
            {
                object miss = System.Reflection.Missing.Value;
                object oStart = bookmark;
                wordDoc.Bookmarks.get_Item(ref oStart).Range.Text = value;//
            }
            catch(Exception ex){
                Console.WriteLine(ex.ToString());
            }
        }
    }  

}
