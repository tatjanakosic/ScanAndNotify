using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class ConfigurationEntry
    {
        private int id;
        private string processName;
        private List<string> users;

        public ConfigurationEntry()
        {
        }

        public ConfigurationEntry(int id, string processName, List<string> users)
        {
            this.id = id;
            this.processName = processName;
            this.users = users;
        }

        public int Id { get => id; set => id = value; }
        public string ProcessName { get => processName; set => processName = value; }
        public List<string> Users { get => users; set => users = value; }

        public override bool Equals(object obj)
        {
            var entry = obj as ConfigurationEntry;
            return entry != null &&
                   Id == entry.Id &&
                   ProcessName == entry.ProcessName &&
                   EqualityComparer<List<string>>.Default.Equals(Users, entry.Users);
        }
    }
}
