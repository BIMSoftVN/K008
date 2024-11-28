using AutoCAD;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Ribbon;
using Newtonsoft.Json.Linq;
using stdole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Picture = Microsoft.Office.Interop.Excel.Picture;

namespace ExcelWorkbook
{
    public partial class Ribbon1
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private async void button1_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                string lat = tb_ViDo.Text;
                string lon = tb_KinhDo.Text;

                string GetLink = @"https://api.openweathermap.org/data/2.5/forecast?lang=vi&units=metric&lat={0}&lon={1}&appid=11aa4b4963ff5453cf74ef722bb48abf";
                GetLink = string.Format(GetLink, lat, lon);

                using (HttpClient client = new HttpClient())
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    HttpResponseMessage res = await client.GetAsync(GetLink);
                    if (res.IsSuccessStatusCode)
                    {
                        string JsonRes = await res.Content.ReadAsStringAsync();

                        var JData = JToken.Parse(JsonRes);

                        Worksheet ws = (Worksheet)Globals.ThisWorkbook.Sheets["Sheet1"];
                        ws.Cells[1, "B"].Value = JData["city"]["name"].ToString();

                        JToken[] ttList = JData["list"].ToArray();

                        long i = 2;
                        foreach (var tt in ttList)
                        {
                            i++;

                            long unixTimeStamp = (long)tt["dt"];
                            DateTimeOffset dtOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp);
                            DateTime dt = dtOffset.UtcDateTime;

                            ws.Cells[i, "A"].Value = dt;
                            ws.Cells[i, "B"].Value = dt;
                            ws.Cells[i, "C"].Value = tt["main"]["temp"].ToString();
                            ws.Cells[i, "D"].Value = tt["wind"]["speed"].ToString();
                            ws.Cells[i, "E"].Value = tt["weather"][0]["description"].ToString();

                            Range wRange = (Range)ws.Range["F"+i];
                            string wCode = tt["weather"][0]["icon"].ToString();
                            string wLink = @"http://openweathermap.org/img/w/" + wCode + ".png";
                            Picture pic = ws.Shapes.AddPicture(wLink, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue,
                                                   wRange.Left, wRange.Top, wRange.Width, wRange.Height);

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            

        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            var acadApp = Marshal.GetActiveObject("AutoCAD.Application") as AcadApplication;
            var acadDoc = acadApp.ActiveDocument;

            var msa = acadDoc.ModelSpace;

            double[] sp = new double[] { 0, 0, 0 };
            double[] ep = new double[] { 10, 10, 0 };

            var line = msa.AddLine(sp, ep);

            double[] cen = new double[] { 5, 5, 0 };
            double ras = 5;
            var cir = msa.AddCircle(cen, ras);

            acadApp.ZoomExtents();
        }

        private void button3_Click(object sender, RibbonControlEventArgs e)
        {
            var acadApp = new AcadApplication();
            acadApp.Visible = true;
            var acadDoc = acadApp.ActiveDocument;

            var msa = acadDoc.ModelSpace;

            double[] sp = new double[] { 0, 0, 0 };
            double[] ep = new double[] { 10, 10, 0 };

            var line = msa.AddLine(sp, ep);
            line.color = ACAD_COLOR.acGreen;

            double[] cen = new double[] { 5, 5, 0 };
            double ras = 5;
            var cir = msa.AddCircle(cen, ras);
            cir.color = ACAD_COLOR.acRed;

            acadApp.ZoomExtents();
        }

        private void button4_Click(object sender, RibbonControlEventArgs e)
        {
            Worksheet ws = (Worksheet)Globals.ThisWorkbook.Sheets["Sheet2"];
            long lr = ws.Cells[1048576, 1].End[XlDirection.xlUp].Row;

            var acadApp = Marshal.GetActiveObject("AutoCAD.Application") as AcadApplication;
            var acadDoc = acadApp.ActiveDocument;
            var msp =  acadDoc.ModelSpace;

            double[] StartPoint = new double[] { 0, 0, 0 };


            for (int i = 2;i<=lr; i++)
            {
                var b = ws.Cells[i, "A"].value;
                var h = ws.Cells[i, "B"].value;
                var Cover = ws.Cells[i, "C"].value;
                var SLT = ws.Cells[i, "D"].value;
                var DKT = ws.Cells[i, "E"].value;
                var SLD = ws.Cells[i, "F"].value;
                var DKD = ws.Cells[i, "G"].value;

                DrawSection(msp, StartPoint, b,h, Cover, SLT, DKT, SLD, DKD);

                StartPoint[0] = StartPoint[0] + b + 600;
            }

            acadApp.ZoomExtents();
            acadDoc.Regen(AcRegenType.acAllViewports);
        }

        private void DrawSection(AcadModelSpace msp, double[] StartPoint, double b, double h, double Cover,
             double SLT, double DKT,
             double SLD, double DKD)
        {
            double[] pLinePoints = new double[]
            {
                StartPoint[0], StartPoint[1],
                StartPoint[0] + b, StartPoint[1],
                StartPoint[0] + b, StartPoint[1] +h,
                StartPoint[0] , StartPoint[1] + h,
            };


            double[] st1 = new double[] { StartPoint[0], StartPoint[1] - 100, 0 };
            double[] st2 = new double[] { StartPoint[0] + b, StartPoint[1] - 100, 0 };
            double[] st3 = new double[] { StartPoint[0] + b/2, StartPoint[1] - 350, 0 };

            var dim = msp.AddDimAligned(st1, st2, st3);
            dim.StyleName = "vista_dim_25";

            st1 = new double[] { StartPoint[0]- 30, StartPoint[1], 0 };
            st2 = new double[] { StartPoint[0] - 30, StartPoint[1] + h, 0 };
            st3 = new double[] { StartPoint[0] - 150, StartPoint[1] +h/2, 0 };

            dim = msp.AddDimAligned(st1, st2, st3);
            dim.StyleName = "vista_dim_25";

            var poly = msp.AddLightWeightPolyline(pLinePoints);
            poly.Closed = true;
            poly.Layer = "Concrete";

            var ThepDai = poly.Offset(-Cover)[0] as AcadLWPolyline;
            ThepDai.Layer = "ThepDai";



            double[] pText = new double[]
            {
                StartPoint[0] +b +200,
                StartPoint[1] +h +100,
                0
            };

            //var t1 = msp.AddText(SLT.ToString() + "#" + DKT.ToString(), pText, 70);

            var oBlock = msp.InsertBlock(pText, "SHT", 1, 1, 1, 0);
            var atts = oBlock.GetAttributes();

            foreach (var att in atts)
            {
                if (att.TagString == "SH")
                {
                    att.TextString = "1";
                }

                if (att.TagString == "DK")
                {
                    att.TextString = SLT.ToString() + "ø" + DKT.ToString();
                }
            }    


            double KCT = (b - 2 * Cover - DKT) / (SLT - 1);
            for (int i = 0; i<SLT; i++)
            {
                double[] Cen = new double[]{
                    StartPoint[0] + Cover + DKT/2 +i*KCT,
                    StartPoint[1] + h - Cover  - DKT/2,
                    0};

                var oRebar = msp.InsertBlock(Cen, "Rebar", DKT, DKT, DKT, 0);
                //var ThepChu = msp.AddCircle(Cen, DKT/2);
                oRebar.Layer = "Rebar";

                var parray = new double[]
                {
                    Cen[0],Cen[1],0,
                    StartPoint[0]+b/2.0,pText[1],0,
                    pText[0],pText[1],0
                };
                msp.AddLeader(parray, null, AcLeaderType.acLineWithArrow);
            }


            pText = new double[]
            {
                StartPoint[0] +b + 200,
                StartPoint[1] - 200,
                0
            };

            //t1 = msp.AddText(SLT.ToString() + "#" + DKT.ToString(), pText, 70);

            oBlock = msp.InsertBlock(pText, "SHT", 1, 1, 1, 0);
            atts = oBlock.GetAttributes();

            foreach (var att in atts)
            {
                if (att.TagString == "SH")
                {
                    att.TextString = "2";
                }

                if (att.TagString == "DK")
                {
                    att.TextString = SLD.ToString() + "ø" + DKD.ToString();
                }
            }




            double KCD = (b - 2 * Cover - DKD) / (SLD - 1);
            for (int i = 0; i < SLD; i++)
            {
                double[] Cen = new double[]{
                    StartPoint[0] + Cover + DKD/2 +i*KCD,
                    StartPoint[1] + Cover  + DKD/2,
                    0};
                //var ThepChu = msp.AddCircle(Cen, DKD / 2);
                //ThepChu.Layer = "Rebar";

                var oRebar = msp.InsertBlock(Cen, "Rebar", DKD, DKD, DKD, 0);
                oRebar.Layer = "Rebar";

                var parray = new double[]
                {
                    Cen[0],Cen[1],0,
                    Cen[0],pText[1],0,
                    pText[0],pText[1],0
                };
                msp.AddLeader(parray, null, AcLeaderType.acLineWithArrow);
            }
        }
    }
}
