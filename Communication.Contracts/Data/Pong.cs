using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Communication.Contracts.Data
{
    [DataContract(Namespace = "http://schemas.abbink/wcfcallbacktest/data/pong")]
    public class Pong
    {
        [DataMember]
        public Guid OperationId { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
