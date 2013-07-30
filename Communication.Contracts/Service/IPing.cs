using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Communication.Contracts.Data;

namespace Communication.Contracts.Service
{
    [ServiceContract(Name = "Ping", Namespace = "http://schemas.abbink/wcfcallbacktest/service/ping", CallbackContract = typeof(IPong))]
    public interface IPing
    {
        [OperationContract(IsOneWay = true)]
        void Ping(Ping ping);
    }
}
