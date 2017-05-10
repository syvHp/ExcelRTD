namespace PnF_MktAssessment._2017.App
{
    partial class rbnMain : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public rbnMain()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.btnMktAssess = this.Factory.CreateRibbonButton();
            this.ckNavToggler = this.Factory.CreateRibbonCheckBox();
            this.btnStartRTD = this.Factory.CreateRibbonButton();
            this.btnStopRTD = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "TAS";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.btnMktAssess);
            this.group1.Items.Add(this.ckNavToggler);
            this.group1.Items.Add(this.separator1);
            this.group1.Items.Add(this.btnStopRTD);
            this.group1.Items.Add(this.btnStartRTD);
            this.group1.Label = "group1";
            this.group1.Name = "group1";
            // 
            // btnMktAssess
            // 
            this.btnMktAssess.Label = "Market Assessment";
            this.btnMktAssess.Name = "btnMktAssess";
            // 
            // ckNavToggler
            // 
            this.ckNavToggler.Label = "Show Nav Pane";
            this.ckNavToggler.Name = "ckNavToggler";
            this.ckNavToggler.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ToggleNav);
            // 
            // btnStartRTD
            // 
            this.btnStartRTD.Label = "Start RTD";
            this.btnStartRTD.Name = "btnStartRTD";
            this.btnStartRTD.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.StartRTD);
            // 
            // btnStopRTD
            // 
            this.btnStopRTD.Label = "Stop RTD";
            this.btnStopRTD.Name = "btnStopRTD";
            this.btnStopRTD.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.StopRTD2);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            // 
            // rbnMain
            // 
            this.Name = "rbnMain";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.rbnMain_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnMktAssess;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox ckNavToggler;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnStartRTD;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnStopRTD;
    }

    partial class ThisRibbonCollection
    {
        internal rbnMain rbnMain
        {
            get { return this.GetRibbon<rbnMain>(); }
        }
    }
}
