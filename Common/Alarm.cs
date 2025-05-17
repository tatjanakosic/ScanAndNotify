using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
        public enum AlarmCriticality
        {
            Information = 0,
            Warning = 1,
            Critical = 2
        }

        [Serializable]
        public class Alarm
        {
            int id;
            string processName;
            AlarmCriticality criticalityLevel;
            DateTime timestamp;

            public Alarm()
            {
            }

            public Alarm(int id, string processName, AlarmCriticality criticalityLevel, DateTime timestamp)
            {
                this.id = id;
                this.processName = processName;
                this.criticalityLevel = criticalityLevel;
                this.timestamp = timestamp;
            }

            public int Id { get => id; set => id = value; }
            public string ProcessName { get => processName; set => processName = value; }
            public AlarmCriticality CriticalityLevel { get => criticalityLevel; set => criticalityLevel = value; }
            public DateTime Timestamp { get => timestamp; set => timestamp = value; }

            public override bool Equals(object obj)
            {
                var alarm = obj as Alarm;
                return alarm != null &&
                       id == alarm.id &&
                       processName == alarm.processName &&
                       criticalityLevel == alarm.criticalityLevel &&
                       timestamp == alarm.timestamp;
            }
        }
    }
