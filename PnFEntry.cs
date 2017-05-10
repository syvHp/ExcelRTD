using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PnF_MktAssessment._2017.App
{
    public partial class PnFEntry : Form
    {
        private TASds2 ds;
        private TASds2TableAdapters.dtSymbolsTableAdapter dtaTAS;
        private TASds2TableAdapters.DailyReadingsTableAdapter dtaDailyReadings;
        private DataTable dtPNFReadings;
        private DataTable dtHEATIndicator;
        private DataView dvTemp;
        //private DataTable qryCalcInd;
        private SqlConnection cn;
        private bool IndicatorKeyIsGood { get; set; }

        private List<DailyReadings> dr_list = new List<DailyReadings>();

        public PnFEntry()
        {
            InitializeComponent();

            ds = new TASds2();
            ds.EnforceConstraints = false;      //      <-----------  This is needed for data processing purposes.

            dtaTAS = new TASds2TableAdapters.dtSymbolsTableAdapter();
            dtaDailyReadings = new TASds2TableAdapters.DailyReadingsTableAdapter();
            dtHEATIndicator = ds.DailyReadings_HeatIndicator;
            dtPNFReadings = ds.DailyReadings;
            
            dtaTAS.Fill(ds.dtSymbols);
            cn = new SqlConnection(Properties.Settings.Default.TASConnectionString);
            cn.Open();
        }

        private void PnFEntry_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tASds2.dtSymbols' table. You can move, or remove it, as needed.
            this.dtSymbolsTableAdapter.FillByPrimaryIndicators(this.tASds2.dtSymbols);

        }

        private void CaptureRow(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var dg = sender as DataGridView;

                if (dg.IsCurrentRowDirty)
                {
                    DailyReadings dr_var = new DailyReadings();

                    dr_var.TestMode = true;

                    if (dr_var.TestMode)
                        {
                            SqlCommand cmd = cn.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "Select DATEADD(d,1, max(ReadingDate)) from pnf.DailyReadings_PrimaryPNFIndicators";
                            dr_var.ReadingDate = Convert.ToDateTime(cmd.ExecuteScalar());
                        }


                    if (!dr_var.TestMode) { dr_var.ReadingDate = this.dateTimePicker1.Value; }
                    dr_var.IsBreakout = Convert.ToBoolean(dg.CurrentRow.Cells[2].Value);
                    dr_var.IsPullback = Convert.ToBoolean(dg.CurrentRow.Cells[3].Value);
                    dr_var.IsInRedZone = Convert.ToBoolean(dg.CurrentRow.Cells[4].Value);
                    dr_var.HeatIndicator = Convert.ToInt16(dg.CurrentRow.Cells[5].Value);
                    dr_var.Note = Convert.ToString(dg.CurrentRow.Cells[6].Value);
                    dr_var.SymbolID = Convert.ToInt16(dg.CurrentRow.Cells[7].Value);

                    dr_list.Add(dr_var);
                }
            }

            catch (Exception e1)
            { }
        }

        /// <summary>
        /// Saves daily reading data. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePnFData(object sender, EventArgs e)
        {
            
            DailyReadings.SaveReadings(dr_list);
            dr_list.Clear();
            dtSymbolsBindingSource.ResetBindings(false);
        }

        private void CkIndicatorCell(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                DataGridView dgv = sender as DataGridView;
                //if() {dgv.CurrentCell.Value = dgv.CurrentCell.Value.ToString().ToUpper();}

                /// Validate Indicator is either X or O and convert uppper case
                if (e.ColumnIndex == 1)
                {
                    switch( e.FormattedValue.ToString().ToUpper())
                    {
                        case "X":
                            break;
                        case "O":
                            break;
                        default:
                            e.Cancel = true;
                            break;
                    }

                }
                // Check to see if last row
                if (e.RowIndex == dgv.RowCount)
                {
                    btnSave.Enabled = true;
                }

            }
            catch (Exception e3)
            { }                
        }

        private void ShowPrimaryIndicators(object sender, EventArgs e)
        {
            this.dataGridView1.ClearSelection();
            this.dtSymbolsTableAdapter.FillByPrimaryIndicators(this.tASds2.dtSymbols);
        }

        private void ShowSectorIndicators(object sender, EventArgs e)
        {
            this.dataGridView1.ClearSelection();
            this.dtSymbolsTableAdapter.FillBySectors(this.tASds2.dtSymbols);
        }

        private void fillByPrimaryIndicatorsToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.dtSymbolsTableAdapter.FillByPrimaryIndicators(this.tASds2.dtSymbols);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }

}
