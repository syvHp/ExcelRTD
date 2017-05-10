using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Office = Microsoft.Office.Core;
using VSTO = Microsoft.Office.Tools.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace PnF_MktAssessment._2017.App
{
    public partial class TaskPane_Report : UserControl
    {
        private Excel.Range XLRange;

        public TaskPane_Report()
        {
            InitializeComponent();
            this.Load += TaskPane_Report_Load;
            this.bindingNavigatorMoveNextItem.Click += bindingNavigatorMoveNextItem_Click;            
        }

        void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            return;
        }

        void TaskPane_Report_Load(object sender, EventArgs e)
        {
            this.dtPNFDailyIndicatorsTableAdapter.Fill(this.tASds2.dtPNFDailyIndicators);   //  <--------   This is the magic

        }

        private void NavClickPrevItem(object sender, EventArgs e)
        {
            BindingNavigator bitm = sender as BindingNavigator;
            bindingSource1.MovePrevious();
        }

        private void NavClickFirstItem(object sender, EventArgs e)
        {
            BindingNavigator bitm = sender as BindingNavigator;
            bindingSource1.MoveFirst();
        }

        private void NavClickNextItem(object sender, EventArgs e)
        {
            BindingNavigator bitm = sender as BindingNavigator;
            bindingSource1.MoveNext();
        }

        private void NavClickLastItem(object sender, EventArgs e)
        {
            BindingNavigator bitm = sender as BindingNavigator;
            bindingSource1.MoveLast();
        }


        private void PnFEntry(object sender, EventArgs e)
        {
            PnF_MktAssessment._2017.App.PnFEntry frmPNP2 = new PnFEntry();
            frmPNP2.ShowDialog();            
        }

        private void GetPNFRpt(object sender, EventArgs e)
        {
            VSTO.ListObject lo2; VSTO.Worksheet ws; 
            DataTable dt44 = tASds2.Tables["dtPNFDailyIndicators"];
            DataTable dt55 = tASds2.Tables["GetMarketAssessmentReportData"];
            TASds2TableAdapters.GetMarketAssessmentReportDataTableAdapter dtMktData = new TASds2TableAdapters.GetMarketAssessmentReportDataTableAdapter();

            DateTime test1 = this.dateTimePicker1.Value.Date;
            dtMktData.GetData(test1);

            dtMktData.Fill(this.tASds2.GetMarketAssessmentReportData, test1);

            ws = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveSheet);
            if (ws.ListObjects.Count == 0)
            {
                lo2 = ws.Controls.AddListObject(ws.Range["A5"], "aa");
            }
            else
            {
                lo2 = Globals.Factory.GetVstoObject(ws.ListObjects[1]);
            }

            var data1 = from x in dt55.AsEnumerable()
                        select new
                        {
                            Symbol = x.Field<string>(0),
                            ReadingDate = x.Field<DateTime>(1),
                            Indicator = x.Field<string>(2),
                            IndicatorHasChanged = x.Field<bool>(3),
                            IsInRedZone = x.Field<bool>(4),
                            IsBreakout = x.Field<bool>(5),
                            IsPullBack = x.Field<bool>(6),
                            HeatIndicator_Value = x.Field<Int16>(7)
                        };

            string[] mcol = { "Symbol", "ReadingDate", "Indicator", "IndicatorHasChanged", "IsInRedZone", "IsBreakout", "IsPullBack", "HeatIndicator_Value" };

            lo2.AutoSetDataBoundColumnHeaders = true;

            lo2.SetDataBinding(data1.ToList(),null, mcol);

            // need to define Format Conditions and apply to columns. First should be based on Change in Indicator ( X to O or vice versa)
            foreach (Excel.ListRow clist_row in lo2.ListRows)
            {
                Excel.Range rng = clist_row.Range;
            }            
        }




    }
}
