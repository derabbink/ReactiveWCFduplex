using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communication.Service.Events
{
    public class PongEventArgs : EventArgs
    {
        public PongEventArgs(Guid operationId, string message)
        {
            OperationId = operationId;
            Message = message;
        }

        public Guid OperationId { get; private set; }

        public string Message { get; private set; }
    }
}
