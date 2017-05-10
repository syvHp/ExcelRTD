using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace PnF_MktAssessment._2017.App
{
    class TAS_Logger
    {
        static NLog.Logger MyLogger = NLog.LogManager.GetLogger("LogFile");
        static NLog.Logger MyLoggerCW = NLog.LogManager.GetLogger("cw");

        public void TAS_LogEntry(string LogContent)
        {
            NLog.LogEventInfo theEvent = new NLog.LogEventInfo(NLog.LogLevel.Info, "", LogContent);
            theEvent.Properties["MyValue"] = LogContent;
            MyLogger.Log(theEvent);
        }

        public void TAS_LogEntryConsole(string LogContent)
        {
            NLog.LogEventInfo theEvent = new NLog.LogEventInfo(NLog.LogLevel.Info, "cw",LogContent);
            theEvent.Properties["MyValue"] = LogContent;
            MyLoggerCW.Log(theEvent);
        }

    }
}
