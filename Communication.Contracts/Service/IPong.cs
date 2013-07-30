using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Communication.Contracts.Data;

namespace Communication.Contracts.Service
{
    //ServiceContract attribute note required
    //[ServiceContract(Name = "Pong", Namespace = "http://schemas.abbink/wcfcallbacktest/service/pong")]
    public interface IPong
    {
        [OperationContract(IsOneWay = true)]
        void Pong(Pong pong);
    }
}
