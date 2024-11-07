using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExcelAddIn
{
    public partial class Ribbon1
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            var Excel = Globals.ThisAddIn.Application;
            var wb = Excel.ActiveWorkbook;
            var ws = (Worksheet)wb.ActiveSheet;

            var aCell = Excel.ActiveCell;

            long i = -1;

            foreach (Worksheet sht in wb.Worksheets)
            {
                i++;
                //aCell.Offset[i,0].Value = sht.Name;

                aCell.Offset[i, 0].Hyperlinks.Add(Anchor: aCell.Offset[i, 0], Address: "", SubAddress: $"'{sht.Name}'!A1"
                    , TextToDisplay: sht.Name);
            }    
           
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            var Excel = Globals.ThisAddIn.Application;
            var aCell = Excel.ActiveCell;
            long i = -1;

            using (FolderBrowserDialog fbd = new FolderBrowserDialog()) 
            {
                fbd.Description = "Chọn thực mục";
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolder = fbd.SelectedPath;

                    var files = Directory.GetFiles(selectedFolder, "*.xls*", SearchOption.AllDirectories).Concat
                        (Directory.GetFiles(selectedFolder, "*.txt", SearchOption.AllDirectories)).ToList();


                    //List<string> fileTotal = new List<string>();
                    //fileTotal.AddRange(Directory.GetFiles(selectedFolder, "*.xls*", SearchOption.AllDirectories));
                    //fileTotal.AddRange(Directory.GetFiles(selectedFolder, "*.txt", SearchOption.AllDirectories));

                    foreach (var f in files) 
                    {
                        FileInfo fi = new FileInfo(f);
                        i++;
                        aCell.Offset[i, 0].Value = fi.Name;
                        aCell.Offset[i, 1].Value = fi.CreationTime;
                        aCell.Offset[i, 2].Value = fi.Length/1024;
                        aCell.Offset[i, 3].Value = fi.FullName;
                    }
                }    
            }
        }
    }
}
