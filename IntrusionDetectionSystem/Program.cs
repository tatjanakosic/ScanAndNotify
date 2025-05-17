using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CertificateManager;
using Common;
namespace IntrusionDetectionSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            name = name.ToLower();
            Console.WriteLine(name);

            ServiceHost svc = new ServiceHost(typeof(IntrusionDetectionSystem));

            NetTcpBinding bindingIDS = new NetTcpBinding();

            bindingIDS.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            svc.AddServiceEndpoint(typeof(Common.IIntursionDetectionSystem),
                                   bindingIDS,
                                   new Uri("net.tcp://localhost:5001/IIntursionDetectionSystem"));
            svc.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.ChainTrust;
            svc.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            svc.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, name);

            try
            {
                svc.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();

        }
    }
}
