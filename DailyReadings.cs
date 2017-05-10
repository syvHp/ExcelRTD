using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace PnF_MktAssessment._2017.App
{
    class DailyReadings : IComparable<DailyReadings>, IFormattable
    {
        public int ReadingID { get; set; }
        public DateTime ReadingDate { get; set; }
        public int SymbolID { get; set; }
        public char Indicator { get; set; }
        public bool IndicatorHasChanged {get; set;}
        public bool IsInRedZone { get; set; }
        public bool IsBreakout { get; set; }
        public bool IsPullback { get; set; }
        public int HeatIndicator { get; set; }
        public string Note { get; set; }
        public bool TestMode { get; set; }

        private static SqlConnection cn;
        

        public DailyReadings()
        {
            if (cn == null || cn.State == 0) { cn = new SqlConnection(Properties.Settings.Default.TASConnectionString); cn.Open(); }
            
            //TestMode = true;   // comment out for prod. Test Mode automatically increments the date whatever is "next" in the database
        }

        public DailyReadings(int SymbolID, DateTime ReadingDate)
        {
            this.SymbolID = SymbolID;
            this.ReadingDate = ReadingDate;
        }

        public static void SaveReadings(List<DailyReadings> ReadingList)
        {
            TASds2.dtPNFDailyIndicatorsDataTable dr_tbl = new TASds2.dtPNFDailyIndicatorsDataTable();
            TASds2TableAdapters.dtPNFDailyIndicatorsTableAdapter dt_a = new TASds2TableAdapters.dtPNFDailyIndicatorsTableAdapter();
            
            foreach (DailyReadings dr1 in ReadingList)
            {
                // This is where we implement logic
                DailyReadings dr_prev = new DailyReadings(dr1.SymbolID, dr1.ReadingDate);

                // This is a database call
                GetPriorDayInfo(dr_prev);                

                if (dr_prev.Indicator != dr1.Indicator) { dr1.IndicatorHasChanged = true; }  //  Set the value of today's setting by comparing to yesterday's

                //save current record to db

                TASds2.dtPNFDailyIndicatorsRow dr_curr = dr_tbl.NewdtPNFDailyIndicatorsRow();
                dr_curr["ReadingDate"] = dr1.ReadingDate;
                dr_curr["SymbolID"] = dr1.SymbolID;
                dr_curr["Indicator"] = dr1.Indicator;
                dr_curr["IndicatorHasChanged"] = dr1.IndicatorHasChanged;
                dr_curr["IsInRedZone"] = dr1.IsInRedZone;
                dr_curr["IsBreakout"] = dr1.IsBreakout;
                dr_curr["IsPullBack"] = dr1.IsPullback;

                dr_tbl.AdddtPNFDailyIndicatorsRow(dr_curr);
            }

            // Need to improve error handling here
            dt_a.Update(dr_tbl);
            
        }


        private static void GetPriorDayInfo(DailyReadings dr)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("pnf.GetPriorReading", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SymbolID", dr.SymbolID);
                cmd.Parameters.AddWithValue("@ReadingDate", dr.ReadingDate);  // send in today's date and stored proc will figure out prior date

                cmd.Parameters.Add("@Indicator",SqlDbType.Char,1);
                cmd.Parameters["@Indicator"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add("@IndicatorHasChanged", SqlDbType.Bit);
                cmd.Parameters["@IndicatorHasChanged"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add("@IsInRedZone", SqlDbType.Bit);
                cmd.Parameters["@IsInRedZone"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add("@IsBreakout", SqlDbType.Bit);
                cmd.Parameters["@IsBreakout"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add("@IsPullBack", SqlDbType.Bit);
                cmd.Parameters["@IsPullBack"].Direction = System.Data.ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                dr.Indicator = Convert.ToChar(cmd.Parameters["@Indicator"].Value);

                // No current business logic requirements but may be in future
                dr.IsInRedZone = Convert.ToBoolean(cmd.Parameters["@IsInRedZone"].Value);
                dr.IsBreakout = Convert.ToBoolean(cmd.Parameters["@IsBreakout"].Value);
                dr.IsPullback = Convert.ToBoolean(cmd.Parameters["@IsPullback"].Value);

            }

            catch (Exception e3)
            { }
        }


        public string ToString(string format, IFormatProvider formatProvider)
        {
            return "";
        }

        public int CompareTo(DailyReadings other)
        {
            return this.SymbolID;
        }


    }
}
