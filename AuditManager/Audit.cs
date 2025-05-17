using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
namespace AuditManager
{
	public class Audit : IDisposable
	{

		private static EventLog customLog = null;
		const string SourceName = "MST.Audit";
		const string LogName = "MalwareScanningTool";

		static Audit()
		{
			//Console.WriteLine("Nulta");
			try
			{
                //Console.WriteLine("Nulta");
				if (!EventLog.SourceExists(SourceName))
				{
					//Console.WriteLine("Prva tacka");
					EventLog.CreateEventSource(SourceName, LogName);
				}
				//Console.WriteLine("Druga tacka");
				customLog = new EventLog(LogName,
					Environment.MachineName, SourceName);
               // Console.WriteLine("Trying my best");
            }
			catch (Exception e)
			{
				customLog = null;
				Console.WriteLine("Error while trying to create log handle. Error = {0}", e.Message);
			}
		}

        public static void ThreatDetected(string processName, AlarmCriticality criticality, DateTime timestamp)
        {
            if (customLog != null)
            {
                string ThreatDetected = AuditEvents.ThreatDetected;
                string message = String.Format(ThreatDetected, processName, criticality, timestamp);
                if (criticality == AlarmCriticality.Information)
                    customLog.WriteEntry(message, EventLogEntryType.Information);
                else if (criticality == AlarmCriticality.Warning)
                    customLog.WriteEntry(message, EventLogEntryType.Warning);
                else if (criticality == AlarmCriticality.Critical)
                    customLog.WriteEntry(message, EventLogEntryType.Error);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.ThreatDetected));
            }
        }

        public void Dispose()
		{
			if (customLog != null)
			{
				customLog.Dispose();
				customLog = null;
			}
		}
	}
}
