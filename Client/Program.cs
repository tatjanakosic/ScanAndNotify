using CertificateManager;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            Console.WriteLine(name);

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;


            bool exit = false;

            using (Client proxy = new Client(binding, new EndpointAddress(new Uri("net.tcp://localhost:5002/IMalwareScanningTool"))))
            {
                proxy.Connect();

                while (!exit)
                {
                    Console.WriteLine("0 - Create Whitelist configuration file.");
                    Console.WriteLine("1 - Delete Whitelist configuration file.");
                    Console.WriteLine("2 - Add new entry to the Whitelist.");
                    Console.WriteLine("3 - Modify existing entry.");
                    Console.WriteLine("4 - Delete entry.");
                    Console.WriteLine("5 - Read Whitelist configuration file.");
                    Console.WriteLine("X - Shut Down.");
                    Console.WriteLine();

                    ConfigurationEntry newEntry;
                    string str;
                    str = Console.ReadLine();
                    int id;
                    Console.WriteLine();

                    switch (str)
                    {
                        case "0":
                            Console.WriteLine(proxy.CreateConfigurationFile());
                            break;
                        case "1":
                            Console.WriteLine(proxy.DeleteConfigurationFile());
                            break;
                        case "2":
                            newEntry = CreateEntry();
                            Console.WriteLine(proxy.AddEntry(newEntry));
                            break;
                        case "3":
                            Console.Write("Enter id of the entry you wish to modify: ");
                            id = int.Parse(Console.ReadLine());
                            newEntry = CreateEntry();
                            newEntry.Id = id;
                            Console.WriteLine(proxy.ModifyEntry(newEntry));
                            break;
                        case "4":
                            Console.Write("Enter id of the entry to delete: ");
                            id = int.Parse(Console.ReadLine());
                            Console.WriteLine(proxy.DeleteEntry(id));
                            break;
                        case "5":
                            List<ConfigurationEntry> entryList = proxy.ReadConfigurationFile();
                            PrintWhitelist(entryList);
                            break;
                        case "X":
                            exit = true;
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine();
                Console.ReadLine();
                proxy.Disconnect();
            }
        }


            private static ConfigurationEntry CreateEntry()
            {
                Console.Write("Enter name of the process: ");
                string processName = Console.ReadLine();
                Console.WriteLine("Enter Users that can start the process (X to finish): ");
                List<string> users = new List<string>();
                string user;
                while (true)
                {
                    user = Console.ReadLine();
                    if (user.Equals("X"))
                        break;
                    users.Add(user);
                }
                return new ConfigurationEntry(1, processName, users);
            }

            private static void PrintWhitelist(List<ConfigurationEntry> entryList)
            {
                Console.WriteLine("Whitelist:");
                Console.WriteLine();
                if(entryList != null)
                {
                    foreach (ConfigurationEntry entry in entryList)
                    {
                        Console.WriteLine("ID: " + entry.Id);
                        Console.WriteLine("Process Name: " + entry.ProcessName);
                        Console.WriteLine("List of allowed Users for this process: ");
                        foreach (string user in entry.Users)
                        {
                            Console.WriteLine("\t" + user);
                        }
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
            }


        }
    }
