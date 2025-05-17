using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace AuditManager
{
	public enum AuditEventTypes
	{
        //AuthenticationSuccess = 0,
        //AuthorizationSuccess = 1,
        //AuthorizationFailed = 2
        ThreatDetected = 0
    }

	public class AuditEvents
	{
		private static ResourceManager resourceManager = null;
		private static object resourceLock = new object();

		private static ResourceManager ResourceMgr
		{
			get
			{
				lock (resourceLock)
				{
					if (resourceManager == null)
					{
						resourceManager = new ResourceManager
							(typeof(AuditEventFile).ToString(),
							Assembly.GetExecutingAssembly());
					}
					return resourceManager;
				}
			}
		}

        public static string ThreatDetected
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.ThreatDetected.ToString());
            }
        }
    }
}
