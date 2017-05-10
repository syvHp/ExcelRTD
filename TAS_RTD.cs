using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using VSTO = Microsoft.Office.Tools.Excel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using NLog.Config;
using NLog.Windows;
using Newtonsoft.Json;

namespace PnF_MktAssessment._2017.App
{
    public class TAS_RTD
    {
        #region "props"

        public int IVRank
        {
            get { return IVRank; }

            set
            {
				//First check delta
                if (Math.Abs(value) - Math.Abs(this.IVRankPrior) > IVChangemoderate) // test for at least moderate change
                {
                    if (value - this.IVRankPrior > IVChangeSharp)   //  see if sharp
                    {
                        this.IVChangeRateFactor = IVChangeRate.Sharp;
                        // Need code to raise alert
                    }
                    else
                    { this.IVChangeRateFactor = IVChangeRate.Moderate; }
                }
                else 
					if(Math.Abs(value) - Math.Abs(this.IVRankPrior) > IVChangeGradual)
                        { this.IVChangeRateFactor = IVChangeRate.Gradual; }

                if (value == this.IVRankPrior)  // if the new value is same as old, then direction must be flat
                {
                    this.IVChangeDir = IVChangeDirection.Flat;
                }

                if (value > this.IVRankPrior)  // if the new value is greater than old, direction must be up
                {
                    this.IVChangeDir = IVChangeDirection.Up;
                }

                if (value < this.IVRankPrior)  // if the new value is less than old, direction must be down
                {
                    this.IVChangeDir = IVChangeDirection.Down;
                }

                if (value > IVUpper )   // raise alert when in upper or lower extreme
                {
                    RaiseAlert(this.RngCurrTmp, AlertMetricsategory.IVRankHigh);
                }

                if (value < IVLower)   // raise alert when in upper or lower extreme
                {
                    RaiseAlert(this.RngCurrTmp, AlertMetricsategory.IVRankLow);
                }

            }
        }

        public IVChangeDirection IVChangeDir { get; set; }
        public int IVRankPrior { get; set; }
        public int EarningsDateOffset { get; set; }
        public int EarningsDateOffsetPrior { get; set; }
        public int Skew { get; set; }
        public int SkewPrior { get; set; }
        public string Symbol { get; set; }
        protected IVChangeRate IVChangeRateFactor { get; set; }
        public DateTime DataCaptureTimestamp { get; set; }
        private Excel.Range RngCurrTmp { get; set; }

        private object[,] rng_prev_ary;
        private byte[] TAS_PrevData; private int EvtCount;
        //private static NLog.Logger MyLogger = NLog.LogManager.GetCurrentClassLogger();
        //private NLog.Config.SimpleConfigurator MyConfig 
        private static bool RTD_Stop = false;

        const int IVUpper = 90;const int IVLower = 10;
        const int IVChangeGradual = 3; const int IVChangemoderate = 6; const int IVChangeSharp = 10;

        public enum IVChangeRate { Gradual, Moderate, Sharp }
        public enum IVChangeDirection { Up, Down, Flat }
        public enum AlertMetricsategory { EarningsOffset, IVRankHigh, IVRankLow, BackMonthSkew, FrontMonthSkew };
        //public enum AlertType { IVExtreme, Other };

        #endregion
        public void RTDProcessor(VSTO.Worksheet wkb_curr)
        {
            wkb_curr.Calculate += wkb_curr_Calculate;
        }

        void wkb_curr_Calculate()
        {
            TAS_Logger tlog = new TAS_Logger();
            string TAS_LogEntrystr = DateTime.Now.ToString() ;
            tlog.TAS_LogEntry(string.Format("Event:{0} -", EvtCount) + TAS_LogEntrystr);
            //tlog.TAS_LogEntryConsole(string.Format("Event:{0} -", EvtCount) + TAS_LogEntrystr);
            Excel.Application xlApp = Globals.ThisAddIn.Application;
            Excel.Workbook wkb = xlApp.ThisWorkbook;
            Excel.Worksheet ws = xlApp .ActiveWorkbook.Worksheets[1];
            Excel.Range rng = ws.ListObjects[1].Range;
            CkCUSTOM3(rng);
            return;

        }


        void wkb_curr_Change(Excel.Range Target)
        {
            if (!RTD_Stop)
            {
                EvtCount = EvtCount + 1;
                Excel.Worksheet ws = Target.Parent;
                Excel.Range n_rng = ws.ListObjects[1].Range;

                if (rng_prev_ary == null)  //  rehydrate previous data
                    using (StreamReader file = File.OpenText(Properties.Settings.Default.PrevDataFilePath))
                    {
                        if (File.Exists(Properties.Settings.Default.PrevDataFilePath))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            object[,] myObj2 = (object[,])serializer.Deserialize(file, typeof(object[,]));
                            rng_prev_ary = myObj2;
                        }
                        else
                        {
                            rng_prev_ary = Target.Value2;
                        }

                    }
                else
                {
                    rng_prev_ary = Target.Value2;
                    TAS_Logger tl = new TAS_Logger();
                    tl.TAS_LogEntry(string.Format("EvtCount {0} : Rehydrate: ", EvtCount) + TAS_ToString(rng_prev_ary));
                }

                CkCUSTOM3(Target);
            }
            else { }    //    <----------------  This is where you need to unregister event  if you can figure out how
        }

        public void StopRTD(Excel.Worksheet ws_curr)
        {
            RTD_Stop = true;
            //Excel.Range rng = ws_curr.ListObjects[1].Range;
            //CkCUSTOM1(rng);
        }

        /// <summary>
        /// Main RTD event processor.    FROZEN as of 03-MAY-2017 in order to concentrate on using object properties.
        /// </summary>
        /// <param name="r2p"></param>
        void CkCUSTOM1(Excel.Range r2p)
        {
            try
            {
                bool NeedToLog = false; string TAS_LogEntrystr = ""; string TAS_LogEntrystr2 = ""; string TAS_ColName = ""; bool TAS_Test_Mode = true; TAS_Logger tlog = new TAS_Logger();
                object[,] rng_ary;
                Excel.Range n_rng = r2p.Parent.ListObjects[1].Range;

                rng_ary = n_rng.Value2;
                //rng_prev_ary = r2p.Value2;   //   Test use only
                Debug.WriteLine(n_rng.Row);
                for (int i = 1; i <= n_rng.Rows.Count; i++)   // This is supposed to be the loop down the rows
                {
                    for (int c = 1; c <= rng_ary.GetUpperBound(1); c++)  //  this is supposed to tbe loop across the columns
                    {
                        // The second condition caps processing at the 3rd column  && !TAS_Test_Mode
                        if (i > 1 && c < 4)  //  && Convert.ToDouble(rng_ary[i, 2]) - Convert.ToDouble(rng_prev_ary[i, 2]) != 0)
                        {
                            TAS_ColName = rng_ary[i, 1].ToString();
                            if (c > 1)
                            {
                                switch (c)
                                {
                                    case 2:             // Earnings Event offset 
                                        int EDO = Convert.ToInt32(rng_ary[i, c]) - Convert.ToInt32(rng_prev_ary[i, c]);
                                        NeedToLog = true;
                                        if (TAS_Test_Mode)
                                        {
                                            TAS_LogEntrystr = DateTime.Now.ToString() + "-Case 2: c =" + c + " i=" + i + " ~~" + TAS_ColName + "~~new val =" + rng_ary[i, c] + "  -- old: " + rng_prev_ary[i, c];
                                            tlog.TAS_LogEntry(string.Format("Event:{0} -", EvtCount) + TAS_LogEntrystr);
                                            //tlog.TAS_LogEntryConsole(string.Format("Event:{0} -", EvtCount) + TAS_LogEntrystr);
                                        }
                                        break;
                                    case 3:             // IV Rank
                                        int g1 = Convert.ToInt32(rng_ary[i, c]) - Convert.ToInt32(rng_prev_ary[i, c]);
                                        if (g1 > IVUpper) { RaiseAlert(n_rng.Rows[c], AlertMetricsategory.IVRankHigh); NeedToLog = true; };
                                        if (g1 > IVLower) { RaiseAlert(n_rng.Rows[c], AlertMetricsategory.IVRankLow); NeedToLog = true; };
                                        if (TAS_Test_Mode)
                                        {
                                            //TAS_LogEntrystr = DateTime.Now.ToString() + "-Case 3: c =" + c + " i=" + i + "Var g1 =" + g1 + " - ~~" + TAS_ColName + "~~new val =" + rng_ary[i, c] + "  -- old: " + rng_prev_ary[i, c];
                                            TAS_LogEntrystr = "IV Case: c =" + c + " i=" + i + " - ~~" + TAS_ColName + "~~Row Num =" + n_rng[i, c].Row + "  -- Column: " + n_rng[i, c].Column;
                                            tlog.TAS_LogEntryConsole(string.Format("Event:{0} -", EvtCount) + TAS_LogEntrystr);
                                            //tlog.TAS_LogEntry(string.Format("Event:{0} -", EvtCount) + TAS_LogEntrystr);
                                        }
                                        IVRank = g1;
                                        break;
                                    case 4:             // Skew

                                        if (TAS_Test_Mode)
                                        {
                                            TAS_LogEntrystr = DateTime.Now.ToString() + "-Case 4: c =" + c + " i=" + i + "- ~~" + TAS_ColName + "~~new val =" + rng_ary[i, c] + "  -- old: " + rng_prev_ary[i, c];                                            
                                            tlog.TAS_LogEntry(string.Format("Event:{0} - ",EvtCount) + TAS_LogEntrystr);
                                            //tlog.TAS_LogEntryConsole(string.Format("Event:{0} - ", EvtCount) + TAS_LogEntrystr);
                                        }

                                        break;
                                    default:
                                        TAS_LogEntrystr = DateTime.Now.ToString() + "- Default Case: c =" + c + " i=" + i + "- ~~" + TAS_ColName + "~~new val =" + rng_ary[i, c] + "  -- old: " + rng_prev_ary[i, c];
                                         tlog.TAS_LogEntry(string.Format("Event:{0} - ",EvtCount) + TAS_LogEntrystr);
                                         //tlog.TAS_LogEntryConsole(string.Format("Event:{0} - ", EvtCount) + TAS_LogEntrystr);
                                        break;
                                }

                            }  // bottom of if

                            if (TAS_Test_Mode)
                            {
                                //TestSlzProcess(DateTime.Now.ToString() + " - Test mode is On - ~~" + TAS_ColName + "new val =" + rng_ary[i, c] + "  -- old: " + rng_prev_ary[i, c]);
                                TAS_LogEntrystr2 = "Row Counter: " + "Row Num =" + n_rng[i, c].Row + "  -- Column: " + n_rng[i, c].Column + " Range:" + n_rng.AddressLocal.ToString() ;
                                tlog.TAS_LogEntry(string.Format("Event:{0} - ", EvtCount) + TAS_LogEntrystr2);
                                tlog.TAS_LogEntryConsole(string.Format("Event:{0} - ", EvtCount) + TAS_LogEntrystr2);
                            }

                        }  // bottom of column loop
                        
                        if (NeedToLog)
                        {
                            TAS_LogEntrystr = DateTime.Now.ToString() + string.Format("- Need to log {0} ~~",EvtCount) + TAS_ColName + "~~new val =" + rng_ary[i, c] + "  -- old: " + rng_prev_ary[i, c];
                            NLog.LogEventInfo theEvent = new NLog.LogEventInfo(NLog.LogLevel.Info, "", TAS_LogEntrystr);
                            theEvent.Properties["MyValue"] = TAS_LogEntrystr;
                            //tlog.TAS_LogEntryConsole(theEvent);

                            NeedToLog = false;
                        }
                    }
                }
                // dehydrate current data as previous
                rng_prev_ary = rng_ary;
                TAS_Logger tl = new TAS_Logger();
                tl.TAS_LogEntry(string.Format("Dehydrate: {0} -", EvtCount) + TAS_ToString(rng_prev_ary));
            }
            catch (Exception e) 
            {
                Debug.WriteLine(e.Message.ToString());
                //MyLogger.Log(NLog.LogLevel.Info, e.Message);
                return;
            }
        }

        /// <summary>
        /// New Main RTD Event process. Use property more
        /// </summary>
        /// <param name="r2p"></param>
        void CkCUSTOM3(Excel.Range r2p)
        {
            try
            {
                bool NeedToLog = false; string TAS_LogEntrystr = ""; string TAS_LogEntrystr2 = ""; string TAS_ColName = ""; bool TAS_Test_Mode = true; TAS_Logger tlog = new TAS_Logger();
                object[,] rng_ary;
                Excel.Range n_rng = r2p.Parent.ListObjects[1].Range;

                rng_ary = n_rng.Value2;
                //rng_prev_ary = r2p.Value2;   //   Test use only
                Debug.WriteLine(n_rng.Row);
                for (int i = 1; i <= n_rng.Rows.Count; i++)   // This is supposed to be the loop down the rows
                {
                    for (int c = 1; c <= rng_ary.GetUpperBound(1); c++)  //  this is supposed to tbe loop across the columns
                    {
                        // The second condition caps processing at the 3rd column
                        if (i > 1 && c < 4)
                        {
                            TAS_ColName = rng_ary[i, 1].ToString();
                            if (c > 1)
                            {
                                switch (c)
                                {
                                    case 2:             // Earnings Event offset 
                                        this.EarningsDateOffset = Convert.ToInt32(rng_ary[i, c]) - Convert.ToInt32(rng_prev_ary[i, c]);
                                        if (TAS_Test_Mode)
                                        {
                                            TAS_LogEntrystr = DateTime.Now.ToString() + "-Case 2: c =" + c + " i=" + i + " ~~" + TAS_ColName + "~~new val =" + rng_ary[i, c] + "  -- old: " + rng_prev_ary[i, c];
                                            tlog.TAS_LogEntry(string.Format("Event:{0} -", EvtCount) + TAS_LogEntrystr);
                                            //tlog.TAS_LogEntryConsole(string.Format("Event:{0} -", EvtCount) + TAS_LogEntrystr);
                                        }
                                        break;
                                    case 3:             // IV Rank
                                        // When the IV Rank property is Set, additional logic is triggered
                                        this.RngCurrTmp = n_rng[i];
                                        this.IVRankPrior = Convert.ToInt32(rng_prev_ary[i, c]);  //  Should this be set automatically?
                                        this.IVRank = Convert.ToInt32(rng_ary[i, c]);
                                        if (TAS_Test_Mode)
                                        {
                                            //TAS_LogEntrystr = "IV Case: c =" + c + " i=" + i + " - ~~" + TAS_ColName + "~~Row Num =" + n_rng[i, c].Row + "  -- Column: " + n_rng[i, c].Column;
                                            TAS_LogEntrystr = "IV Rank pprior = " + this.IVRankPrior.ToString() + " IV Rank current = " + this.IVRank.ToString();
                                            tlog.TAS_LogEntryConsole(string.Format("Event:{0} -", EvtCount) + TAS_LogEntrystr);
                                        }

                                        break;
                                    case 4:             // Skew

                                        if (TAS_Test_Mode)
                                        {
                                            TAS_LogEntrystr = DateTime.Now.ToString() + "-Case 4: c =" + c + " i=" + i + "- ~~" + TAS_ColName + "~~new val =" + rng_ary[i, c] + "  -- old: " + rng_prev_ary[i, c];
                                            tlog.TAS_LogEntry(string.Format("Event:{0} - ", EvtCount) + TAS_LogEntrystr);
                                            //tlog.TAS_LogEntryConsole(string.Format("Event:{0} - ", EvtCount) + TAS_LogEntrystr);
                                        }

                                        break;
                                    default:
                                        TAS_LogEntrystr = DateTime.Now.ToString() + "- Default Case: c =" + c + " i=" + i + "- ~~" + TAS_ColName + "~~new val =" + rng_ary[i, c] + "  -- old: " + rng_prev_ary[i, c];
                                        tlog.TAS_LogEntry(string.Format("Event:{0} - ", EvtCount) + TAS_LogEntrystr);
                                        //tlog.TAS_LogEntryConsole(string.Format("Event:{0} - ", EvtCount) + TAS_LogEntrystr);
                                        break;
                                }

                            }  // bottom of if

                            if (TAS_Test_Mode)
                            {
                                //TestSlzProcess(DateTime.Now.ToString() + " - Test mode is On - ~~" + TAS_ColName + "new val =" + rng_ary[i, c] + "  -- old: " + rng_prev_ary[i, c]);
                                TAS_LogEntrystr2 = "Row Counter: " + "Row Num =" + n_rng[i, c].Row + "  -- Column: " + n_rng[i, c].Column + " Range:" + n_rng.AddressLocal.ToString();
                                tlog.TAS_LogEntry(string.Format("Event:{0} - ", EvtCount) + TAS_LogEntrystr2);
                                tlog.TAS_LogEntryConsole(string.Format("Event:{0} - ", EvtCount) + TAS_LogEntrystr2);
                            }

                        }  // bottom of column loop

                        if (NeedToLog)
                        {
                            TAS_LogEntrystr = DateTime.Now.ToString() + string.Format("- Need to log {0} ~~", EvtCount) + TAS_ColName + "~~new val =" + rng_ary[i, c] + "  -- old: " + rng_prev_ary[i, c];
                            NLog.LogEventInfo theEvent = new NLog.LogEventInfo(NLog.LogLevel.Info, "", TAS_LogEntrystr);
                            theEvent.Properties["MyValue"] = TAS_LogEntrystr;
                            //tlog.TAS_LogEntryConsole(theEvent);

                            NeedToLog = false;
                        }
                    }
                    EvtCount++;

                }
                // dehydrate current data as previous
                rng_prev_ary = rng_ary;
                TAS_Logger tl = new TAS_Logger();
                tl.TAS_LogEntry(string.Format("Dehydrate: {0} -", EvtCount) + TAS_ToString(rng_prev_ary));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message.ToString());
                //MyLogger.Log(NLog.LogLevel.Info, e.Message);
                return;
            }
        }

        private void RaiseAlert(Excel.Range t_rng, AlertMetricsategory alert_type)
        {

            switch (alert_type)
            {
                case AlertMetricsategory.IVRankHigh:
                    t_rng.EntireRow.Interior.Color = Excel.XlRgbColor.rgbRed;
                    break;

                case AlertMetricsategory.IVRankLow:
                    t_rng.EntireRow.Interior.Color = Excel.XlRgbColor.rgbGoldenrod;
                    break;

                case AlertMetricsategory.EarningsOffset:
                    t_rng.EntireRow.Interior.Color = Excel.XlRgbColor.rgbGray;
                    break;

                case AlertMetricsategory.BackMonthSkew:
                    break;
                case AlertMetricsategory.FrontMonthSkew:
                    break;
            }

            string TAS_LogEntrystr = t_rng.Value2[1, 1].ToString();
            NLog.Windows.Forms.MessageBoxTarget tgt_m = new NLog.Windows.Forms.MessageBoxTarget();
            tgt_m.Caption = TAS_LogEntrystr;
            tgt_m.Name="cw";
            // MyLogger.Debug(tgt_m);
            
        }

        private void LowerAlert(Excel.Range t_rng, AlertMetricsategory alert_type)
        {
            t_rng.EntireRow.Interior.Color = Excel.XlRgbColor.rgbGold;
        }

        public void TAS_CleanUp()
        {
            try
            {
                Random rnd = new Random();
                rnd.Next();
                string fname = "TAS_RTD" + rnd.ToString();
                string fpath = System.Environment.SpecialFolder.LocalApplicationData.ToString() + @"\" + fname;
                Properties.Settings.Default.PrevDataFilePath = fpath;

                string json = File.ReadAllText(fpath);
                object[,] myObj = JsonConvert.DeserializeObject<object[,]>(json);

            }

            catch (Exception r1)
            {
            }
        }

        public void TestSrlz(Excel.Worksheet ws)
        {
            Excel.Range rng = ws.ListObjects[1].Range;
            object[,] obj = rng.Value2;
            System.IO.MemoryStream ms2 = new System.IO.MemoryStream();
            byte[] bt1 = TAS_OBJ.TAS_Serialize(obj);
            object obj2 = TAS_OBJ.TAS_DeSerialize(bt1);
        }

        public void TestSlzProcess(string LogEntry)
        {
            NLog.LogEventInfo theEvent = new NLog.LogEventInfo(NLog.LogLevel.Info, "", LogEntry);
            theEvent.Properties["MyValue"] = LogEntry;
            //MyLogger.Log(theEvent);
        }

        private string TAS_ToString(object[,] obj)
        {
            string result = "";
            foreach (object value in obj)
            {
                result = result + "-" + Convert.ToString(value);
                Console.WriteLine("Converted the {0} value {1} to the {2} value {3}.", value.GetType().Name, value,result.GetType().Name, result);
            }
            return result;
        }
    }    



    //  -----------------------------------------------------------------------------------------------  //
    public static class TAS_OBJ
    {

        public static Object TAS_DeSerialize(byte[] arrBytes)
        {
            using (var ms = new MemoryStream())
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                ms.Write(arrBytes, 0, arrBytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
                return bformatter.Deserialize(ms);
            }
        }

        public static byte[] TAS_Serialize(this Object obj)
        {
            if (obj == null) { return null; }

            using (var memoryStream = new MemoryStream())
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        public static void TAS_DeSerializeToFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public static T TAS_ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(fileContents);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

    }

}  // End of Name space