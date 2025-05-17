using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Diagnostics;

namespace Common
{
    [ServiceContract]
    public interface IIntursionDetectionSystem
    {
        [OperationContract]
        void Natpis();

        [OperationContract]
        void SendMessage(string message, byte[] sign);

        [OperationContract]
        void UpdateIDS(Alarm alarm,string message,byte[] sign);

        [OperationContract]
        string CheckFileIntegrity(string hash, byte[] sign);
    }
}
