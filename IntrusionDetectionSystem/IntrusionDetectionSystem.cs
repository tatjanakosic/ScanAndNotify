using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;
using System.Runtime.Serialization;
using System.Security.Principal;
using CertificateManager;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using AuditManager;

namespace IntrusionDetectionSystem
{
    public class IntrusionDetectionSystem : IIntursionDetectionSystem
    {
        string srvCertCNNSign = "mstSign";
        static int br = 0;

        

        public void Natpis()
        {
            Console.WriteLine("Communication from MST to IDS");
            Console.WriteLine("Test Case:");
            Console.WriteLine("Mena: Any");
            Console.WriteLine("Engi: Add");
            Console.WriteLine("Chief: Create/Delete");
            Console.WriteLine();
        }

        public void SendMessage(string message, byte[] sign)
        {
            string srvCertCNN = CertificateManager.Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            string srvCertCNSign = srvCertCNN + "Sign";
            //Console.WriteLine(srvCertCNSign);
            X509Certificate2 certificate = CertManager.GetCertificateFromStorage(StoreName.My,
                StoreLocation.LocalMachine, srvCertCNNSign);

            
                X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);

                foreach (var certificate123 in store.Certificates)
                {
                if (certificate123.Subject.Contains(srvCertCNNSign))
                {
                    store.Close();
                    //Console.WriteLine("pronadjeno1");

                }
                }

            /// Verify signature using SHA1 hash algorithm
            if (DigitalSignature.Verify(message, HashAlgorithm.SHA1, sign, certificate))
            {
                Console.WriteLine("Sign is valid\n");
                
                
            }
            else
            {
                Console.WriteLine("Sign is invalid\n");
            }
        }

        public void UpdateIDS(Alarm alarm,string message,byte[] sign)
        {
            string srvCertCNN = CertificateManager.Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            string srvCertCNSign = srvCertCNN + "Sign";
            //Console.WriteLine(srvCertCNSign);
            X509Certificate2 certificate = CertManager.GetCertificateFromStorage(StoreName.My,
                StoreLocation.LocalMachine, srvCertCNNSign);


            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            foreach (var certificate123 in store.Certificates)
            {
                if (certificate123.Subject.Contains(srvCertCNNSign))
                {
                    store.Close();
                    //Console.WriteLine("pronadjeno1");

                }
            }

            /// Verify signature using SHA1 hash algorithm
            if (DigitalSignature.Verify(message, HashAlgorithm.SHA1, sign, certificate))
            {
                Console.WriteLine($"Process: {alarm.ProcessName,-30}  {alarm.CriticalityLevel,-20}  {alarm.Timestamp,-20} {br++}");

            }
            else
            {
                Console.WriteLine("Sign is invalid\n");

            }
        }



        public string CheckFileIntegrity(string hash, byte[] sign)
        {

            string srvCertCNN = CertificateManager.Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            string srvCertCNSign = srvCertCNN + "Sign";
            //Console.WriteLine(srvCertCNSign);
            X509Certificate2 certificate = CertManager.GetCertificateFromStorage(StoreName.My,
                StoreLocation.LocalMachine, srvCertCNNSign);


            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            foreach (var certificate123 in store.Certificates)
            {
                if (certificate123.Subject.Contains(srvCertCNNSign))
                {
                    store.Close();
                    //Console.WriteLine("pronadjeno1");

                }
            }

            /// Verify signature using SHA1 hash algorithm
            if (DigitalSignature.Verify(hash, HashAlgorithm.SHA1, sign, certificate))
            {
                string[] commas = hash.Split('+');


                if (commas[0].Equals(commas[1]))
                {

                    Console.WriteLine($"File integrity checksum verifeid. {DateTime.Now}");
                    return "File integrity verified.";

                }
                else
                {

                    Console.WriteLine($"\nFile integrity checksum failed. {DateTime.Now}");
                    Console.WriteLine($"Valid hash value: {commas[0],-30}");
                    Console.WriteLine($"Invalidalid hash value: {commas[1],-30}\n");
                    return "File integrity check failed.";

                }
            }
            else
            {
                Console.WriteLine("Sign is invalid\n");
                return "";
            }




        }




    }
}
