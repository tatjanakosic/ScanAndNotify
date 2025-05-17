using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Client : ChannelFactory<IMalwareScanningTool>, IMalwareScanningTool
    {
        IMalwareScanningTool factory;

        public Client(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public void Connect()
        {
            try
            {
                factory.Connect();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        public void Disconnect()
        {
            try
            {
                factory.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        public string CreateConfigurationFile()
        {
            string message="";
            try
            {
                message=factory.CreateConfigurationFile();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }

            return message;

        }

        public string DeleteConfigurationFile()
        {
            string message = "";
            try
            {
                message=factory.DeleteConfigurationFile();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }

            return message;
        }

        public string AddEntry(ConfigurationEntry entry)
        {
            string message = "";

            try
            {
               message= factory.AddEntry(entry);
            }
            catch (Exception e)
            {
                Console.WriteLine("ErrorAE: {0}", e.Message);
            }

            return message;
        }

        public string ModifyEntry(ConfigurationEntry entry)
        {
            string message = "";
            try
            {
                message=factory.ModifyEntry(entry);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }

            return message;

        }

        public string DeleteEntry(int id)
        {
            string message = "";
            try
            {
                message=factory.DeleteEntry(id);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return message;
        }

        public List<ConfigurationEntry> ReadConfigurationFile()
        {
            List<ConfigurationEntry> entryList = null;
            try
            {
                entryList = factory.ReadConfigurationFile();
            }
            catch (Exception e)
            {
                Console.WriteLine("ErrorRead: {0}", e.Message);
            }
            return entryList;
        }
    }
}
