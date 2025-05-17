using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuditManager
{
    public static class FileIntegrityMonitoring
    {

		private static string exposedHashValue = "";
		private static string SourceName = "MST.Audit";
		private static string LogName = "MalwareScanningTool";




		public static string GetHashValue()
		{

			if (exposedHashValue.Equals(""))
			{

				SetHashValue(GetHashOfLogFile());

			}
			return exposedHashValue;


		}


		public static void SetHashValue(string hash)
		{

			exposedHashValue = hash;

		}



		public static string GetHashOfLogFile()
		{


			StringBuilder sb = new StringBuilder();
			string expectedHash = "";
			EventLog eventLog = new EventLog(LogName, Environment.MachineName, SourceName);

			foreach (EventLogEntry entry in eventLog.Entries)
			{

				sb.AppendLine($"Entry Message: {entry.Message}, Entry Type: {entry.EntryType}");


			}

			using (SHA256 sha256Hash = SHA256.Create())
			{
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
				expectedHash = BitConverter.ToString(bytes).Replace("-", String.Empty);

			}


			eventLog.Close();

			return expectedHash;

		}








	}
}
