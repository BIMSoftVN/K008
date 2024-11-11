using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Ribbon;
using Newtonsoft.Json.Linq;
using stdole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
    }
}
