using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace PnF_MktAssessment._2017.App
{
    public partial class TASMarketDataEntry : Form
    {
        public TASMarketDataEntry()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This form is where you enter datily readings. Submit should write into db
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitMktData(object sender, EventArgs e)
        {
            //TASDailyMarketMetrics tdm = new TASDailyMarketMetrics();

            //tdm.MetricDate = this.dateTimePicker1.Value;

            ////NYSE Bullish %
            //if (this.textBox1.TextLength == 0)  // negtive logic - if this one is zero then must be other one
            //{
            //    tdm.BPNYA = Convert.ToDecimal(this.textBox2.Text);
            //    tdm.BPNYA_FP = "O";
            //}
            //else
            //{
            //    tdm.BPNYA = Convert.ToDecimal(this.textBox1.Text);
            //    tdm.BPNYA_FP = "X";
            //}

            ////NASDAQ Bullish %
            //if (this.textBox3.TextLength == 0)  
            //{
            //    tdm.BPCOMPQ = Convert.ToDecimal(this.textBox4.Text);
            //    tdm.BPCOMPQ_FP = "O";
            //}
            //else
            //{
            //    tdm.BPCOMPQ = Convert.ToDecimal(this.textBox3.Text);
            //    tdm.BPCOMPQ_FP = "X";
            //}

            ////SP 500 Bullish %
            //if (this.textBox7.TextLength == 0)
            //{
            //    tdm.BPSPX = Convert.ToDecimal(this.textBox8.Text);
            //    tdm.BPSPX_FP = "O";
            //}
            //else
            //{
            //    tdm.BPSPX = Convert.ToDecimal(this.textBox7.Text);
            //    tdm.BPSPX_FP = "X";
            //}

            ////SP500 Percent of Stocks Above 50 Day Moving Average
            //if (this.textBox5.TextLength == 0)
            //{
            //    tdm.SPXA50R = Convert.ToDecimal(this.textBox6.Text);
            //    tdm.SPXA50R_FP = "O";
            //}
            //else
            //{
            //    tdm.SPXA50R = Convert.ToDecimal(this.textBox5.Text);
            //    tdm.SPXA50R_FP = "X";
            //}

            ////NYSE High-Low Index
            //if (this.textBox9.TextLength == 0)
            //{
            //    tdm.NYHILO = Convert.ToDecimal(this.textBox10.Text);
            //    tdm.NYHILO_FP = "O";
            //}
            //else
            //{
            //    tdm.NYHILO = Convert.ToDecimal(this.textBox9.Text);
            //    tdm.NYHILO_FP = "X";
            //}

            ////Nasdaq High-Low Index
            //if (this.textBox11.TextLength == 0)
            //{
            //    tdm.NAHILO = Convert.ToDecimal(this.textBox12.Text);
            //    tdm.NAHILO_FP = "O";
            //}
            //else
            //{
            //    tdm.NAHILO = Convert.ToDecimal(this.textBox11.Text);
            //    tdm.NAHILO_FP = "X";
            //}

            ////AMEX High-Low Index
            //if (this.textBox13.TextLength == 0)
            //{
            //    tdm.AMHILO = Convert.ToDecimal(this.textBox14.Text);
            //    tdm.AMHILO_FP = "O";
            //}
            //else
            //{
            //    tdm.AMHILO = Convert.ToDecimal(this.textBox13.Text);
            //    tdm.AMHILO_FP = "X";
            //}

            ////AMEX Advance-Decline Issues
            //if (this.textBox15.TextLength == 0)
            //{
            //    tdm.AMAD = Convert.ToDecimal(this.textBox16.Text);
            //    tdm.AMAD_FP = "O";
            //}
            //else
            //{
            //    tdm.AMAD = Convert.ToDecimal(this.textBox15.Text);
            //    tdm.AMAD_FP = "X";
            //}

            ////Nasdaq Advance-Decline Issues
            //if (this.textBox17.TextLength == 0)
            //{
            //    tdm.NAAD = Convert.ToDecimal(this.textBox18.Text);
            //    tdm.NAAD_FP = "O";
            //}
            //else
            //{
            //    tdm.NAAD = Convert.ToDecimal(this.textBox17.Text);
            //    tdm.NAAD_FP = "X";
            //}

            ////NYSE Advance-Decline Issues
            //if (this.textBox19.TextLength == 0)
            //{
            //    tdm.NYAD = Convert.ToDecimal(this.textBox20.Text);
            //    tdm.NYAD_FP = "O";
            //}
            //else
            //{
            //    tdm.NYAD = Convert.ToDecimal(this.textBox19.Text);
            //    tdm.NYAD_FP = "X";
            //}

            ////Nasdaq Percent of Stocks Above 50 Day Moving Average
            //if (this.textBox21.TextLength == 0)
            //{
            //    tdm.NAA50R = Convert.ToDecimal(this.textBox22.Text);
            //    tdm.NAA50R_FP = "O";
            //}
            //else
            //{
            //    tdm.NAA50R = Convert.ToDecimal(this.textBox21.Text);
            //    tdm.NAA50R_FP = "X";
            //}

            ////NYSE Percent of Stocks Above 50 Day Moving Average
            //if (this.textBox23.TextLength == 0)
            //{
            //    tdm.NYA50R = Convert.ToDecimal(this.textBox24.Text);
            //    tdm.NYA50R_FP = "O";
            //}
            //else
            //{
            //    tdm.NYA50R = Convert.ToDecimal(this.textBox23.Text);
            //    tdm.NYA50R_FP = "X";
            //}

            ////NASDAQ Percents of stocks above 30 Week MA
            //if (this.textBox25.TextLength == 0)
            //{
            //    tdm.NAA150R = Convert.ToDecimal(this.textBox26.Text);
            //    tdm.NAA150R_FP = "O";
            //}
            //else
            //{
            //    tdm.NAA150R = Convert.ToDecimal(this.textBox25.Text);
            //    tdm.NAA150R_FP = "X";
            //}

            ////NYSE Percent of Stocks above 150 DMA
            //if (this.textBox27.TextLength == 0)
            //{
            //    tdm.NYA150R = Convert.ToDecimal(this.textBox28.Text);
            //    tdm.NYA150R_FP = "O";
            //}
            //else
            //{
            //    tdm.NYA150R = Convert.ToDecimal(this.textBox27.Text);
            //    tdm.NYA150R_FP = "X";
            //}

            ////S&P 500 Percent of Stocks Above 150 Day Moving Average
            //if (this.textBox29.TextLength == 0)
            //{
            //    tdm.SPXA150R = Convert.ToDecimal(this.textBox30.Text);
            //    tdm.SPXA150R_FP = "O";
            //}
            //else
            //{
            //    tdm.SPXA150R = Convert.ToDecimal(this.textBox29.Text);
            //    tdm.SPXA150R_FP = "X";
            //}


            //tdm.MetricDate = dateTimePicker1.Value;

            //tdm.InsertDailyMetricRecord();



        }
    }
}
