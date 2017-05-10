using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;

namespace PnF_MktAssessment._2017.App
{
    public partial class ThisAddIn
    {
        private TaskPane_Report MyNavUserControl;
        private Microsoft.Office.Tools.CustomTaskPane  MyCustomTP;
        public Microsoft.Office.Tools.CustomTaskPane PnFTaskPane { get { return MyCustomTP; } }
        private bool TestMode = true;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Globals.Ribbons.rbnMain.tab1.RibbonUI.ActivateTab("tab1");
            MyNavUserControl = new TaskPane_Report();
            MyCustomTP = this.CustomTaskPanes.Add(MyNavUserControl, "PnF Navigator");          
        }


        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
