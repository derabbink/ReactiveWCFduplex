using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Communication.Contracts.Data;
using Communication.Contracts.Service;

namespace Communication.Service.Ping
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class PingService : IPing
    {
        public void Ping(Contracts.Data.Ping ping)
        {
            char[] charArray = ping.Message.ToCharArray();
            Array.Reverse(charArray);
            var reversed = new string(charArray);
            Callback.Pong(new Pong() {OperationId = ping.OperationId, Message = reversed});
        }

        IPong Callback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IPong>();
            }
        }
    }
}
