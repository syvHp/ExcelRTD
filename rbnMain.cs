using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Office.Tools.Ribbon;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using VSTO = Microsoft.Office.Tools.Excel;

namespace PnF_MktAssessment._2017.App
{
    public partial class rbnMain
    {
        //public  VSTO.Workbook v_wkb; public  VSTO.Worksheet v_ws;
        //public  Excel.Workbook wkb; public  Excel.Worksheet ws;

        private TaskPane_Report MyNavUC;
        private Microsoft.Office.Tools.CustomTaskPane MyCustomTP;
        private NLog.Logger MyLogger = NLog.LogManager.GetCurrentClassLogger();

        private void rbnMain_Load(object sender, RibbonUIEventArgs e)
        {
            MyNavUC = new TaskPane_Report();
            MyCustomTP = Globals.ThisAddIn.CustomTaskPanes.Add(MyNavUC, "PnF Navigator");
        }

        private void ToggleNav(object sender, RibbonControlEventArgs e)
        {
            RibbonCheckBox rck = sender as RibbonCheckBox;

            if (MyCustomTP.Visible == true)
            {
                MyCustomTP.Visible = false;
                rck.Checked = false;
            }
            else
            {
                MyCustomTP.Visible = true;
                rck.Checked = true;
            }
        }

        private void StartRTD(object sender, RibbonControlEventArgs e)
        {
            //TAS_RTD tas = new TAS_RTD();
            //v_wkb =  //sender as VSTO.Workbook;
            //wkb = v_wkb.InnerObject; //this.Application.ThisWorkbook;  //Globals.ThisWorkbook //ThisApplication.ThisWorkbook; //(Excel.Workbook)v_wkb;

            //ws = wkb.Worksheets[1];
            //v_ws = Globals.Factory.GetVstoObject(wkb.Worksheets[1]);   //  Need to refer to the INterop object
            //tas.RTDProcessor(v_ws);

            //Excel.Application xlApp = Globals.ThisAddIn.Application;
            ////Excel.Workbook wkb = xlApp.ThisWorkbook;
            //Excel.Worksheet ws = xlApp.ActiveWorkbook.Worksheets[1];
            //Excel.Range rng = ws.ListObjects[1].Range;
            ////VSTO.Workbook wkb = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook);
            //VSTO.Worksheet ws_vsto = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook.Worksheets[1]);
            //VSTO.NamedRange n_rng = ws_vsto.Controls.AddNamedRange(rng, "Data");
            //VSTO.ListObject lo = ws_vsto.ListObjects.get_Item(1);
            //Excel.Range rng2 = ws.ListObjects[1].Range;
            //VSTO.ListObject lo_vsto = ws_vsto.Controls.AddListObject(lo);
            
        }

        private void StopRTD2(object sender, RibbonControlEventArgs e)
        {
            //TAS_RTD tas = new TAS_RTD();
            //Excel.Application xlApp = Globals.ThisAddIn.Application;
            //Excel.Worksheet ws = xlApp.ActiveWorkbook.Worksheets[1];

            //tas.StopRTD(ws);
            //tas.TestSrlz(ws);

        }
    }
}
