using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Communication.Contracts.Data
{
    [DataContract(Namespace = "http://schemas.abbink/wcfcallbacktest/data/ping")]
    public class Ping
    {
        [DataMember]
        public Guid OperationId { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
