using CSiAPIv1;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace EtabsAddIns
{
    public partial class Ribbon1
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                cHelper myHelper = new CSiAPIv1.Helper();
                cOAPI myETABSObject = default(cOAPI);

                myETABSObject = myHelper.GetObject("CSI.ETABS.API.ETABSObject");

                cSapModel mySapModel = default(cSapModel);
                mySapModel = myETABSObject.SapModel;

                int ret = -1;


                ret = mySapModel.SetPresentUnits(eUnits.kN_m_C);


                var Excel = Globals.ThisAddIn.Application;
                var aCell = Excel.ActiveCell;

                int NumberStories = -1;
                string[] StoryNames = new string[] {};
                double[] StoryHeights = new double[] { };
                double[] StoryElevations = new double[] { };
                bool[] IsMasterStory = new bool[] { };
                string[] SimilarToStory = new string[] { };
                bool[] SpliceAbove = new bool[] { };
                double[] SpliceHeight = new double[] { };

                int stt = -1;

                ret = mySapModel.Story.GetStories(ref NumberStories, ref StoryNames, ref StoryElevations, ref StoryHeights, ref IsMasterStory, ref SimilarToStory, ref SpliceAbove, ref SpliceHeight);

                if (ret ==0)
                {
                    if (NumberStories>0)
                    {
                        for (int i = 0; i< NumberStories; i++)
                        {
                            if (StoryNames[i].ToUpper() != "BASE")
                            {
                                stt++;
                                aCell.Offset[stt, 0].Value = StoryNames[i];
                                aCell.Offset[stt, 1].Value = StoryHeights[i];
                                aCell.Offset[stt, 2].Value = StoryElevations[i];
                            }    
                            
                        }    
                    }    
                }    
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                cHelper myHelper = new CSiAPIv1.Helper();
                cOAPI myETABSObject = default(cOAPI);

                myETABSObject = myHelper.GetObject("CSI.ETABS.API.ETABSObject");

                cSapModel mySapModel = default(cSapModel);
                mySapModel = myETABSObject.SapModel;

                int ret = -1;


                ret = mySapModel.SetPresentUnits(eUnits.kN_m_C);

                ret = mySapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();

                ret = mySapModel.Results.Setup.SetComboSelectedForOutput("Comb1", true);
                ret = mySapModel.Results.Setup.SetComboSelectedForOutput("Comb2", false);
                ret = mySapModel.Results.Setup.SetComboSelectedForOutput("Comb3", true);


                var Excel = Globals.ThisAddIn.Application;
                var aCell = Excel.ActiveCell;
                int stt = -1;

                int NumberNames = -1;
                string[] MyName = new string[] { };
                string[] PropName = new string[] { };
                string[] StoryName = new string[] { };
                string[] StoryName_F = new string[] { };
                string[] PointName1 = new string[] { };
                string[] PointName2 = new string[] { };
                double[] Point1X = new double[] { };
                double[] Point1Y = new double[] { };
                double[] Point1Z = new double[] { };
                double[] Point2X = new double[] { };
                double[] Point2Y = new double[] { };
                double[] Point2Z = new double[] { };
                double[] Angle = new double[] { };
                double[] Offset1X = new double[] { };
                double[] Offset2X = new double[] { };
                double[] Offset1Y = new double[] { };
                double[] Offset2Y = new double[] { };
                double[] Offset1Z = new double[] { };
                double[] Offset2Z = new double[] { };
                int[] CardinalPoint = new int[] { };
                string csys = "Global";


                ret = mySapModel.FrameObj.GetAllFrames(ref NumberNames, ref MyName,
                                            ref PropName,
                                            ref StoryName,
                                            ref PointName1, ref PointName2, ref Point1X, ref Point1Y, ref Point1Z,
                                            ref Point2X, ref Point2Y, ref Point2Z,
                                            ref Angle,
                                            ref Offset1X, ref Offset2X, ref Offset1Y, ref Offset2Y, ref Offset1Z,
                                            ref Offset2Z,
                                            ref CardinalPoint, csys);

                if (ret == 0 && NumberNames>0)
                {
                    foreach (var name in MyName) 
                    {
                        var deOri = eFrameDesignOrientation.Null;
                        if (mySapModel.FrameObj.GetDesignOrientation(name, ref deOri) == 0 &&
                            deOri == eFrameDesignOrientation.Column)
                        {

                            string Label = null;
                            string Story = null;

                            ret = mySapModel.FrameObj.GetLabelFromName(name, ref Label, ref Story);


                            int NumberResults = -1;
                            string[] Obj = new string[] { };
                            double[] ObjSta = new double[] { };
                            string[] Elm = new string[] { };
                            double[] ElmSta = new double[] { };
                            string[] LoadCase = new string[] { };
                            string[] StepType = new string[] { };
                            double[] StepNum = new double[] { };
                            double[] P = new double[] { };
                            double[] V2 = new double[] { };
                            double[] V3 = new double[] { };
                            double[] T = new double[] { };
                            double[] M2 = new double[] { };
                            double[] M3 = new double[] { };

                            ret = mySapModel.Results.FrameForce(name, eItemTypeElm.Element, ref NumberResults, ref Obj, ref ObjSta, ref Elm,
                                                            ref ElmSta,
                                                            ref LoadCase, ref StepType,
                                                            ref StepNum, ref P, ref V2, ref V3, ref T, ref M2,
                                                            ref M3);

                            if (ret == 0)
                            {
                                for (int i = 0; i < NumberResults; i++)
                                {
                                    stt++;
                                    aCell.Offset[stt, 0].Value = name;
                                    aCell.Offset[stt, 1].Value = Label;
                                    aCell.Offset[stt, 2].Value = Story;
                                    aCell.Offset[stt, 3].Value = LoadCase[i];
                                    aCell.Offset[stt, 4].Value = P[i];
                                    aCell.Offset[stt, 5].Value = M2[i];
                                    aCell.Offset[stt, 6].Value = M3[i];
                                }
                            }    
                        }
                    }
                }    
            }
            catch (Exception ea)
            {

            }
        }
    }
}
