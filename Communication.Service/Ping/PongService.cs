using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Common;
using Communication.Contracts.Data;
using Communication.Contracts.Service;
using Communication.Service.Events;

namespace Communication.Service.Ping
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class PongService : IPong
    {
        private event EventHandler<PongEventArgs> PongCallback;

        protected virtual void OnPongCallback(PongEventArgs e)
        {
            PongCallback.Raise(this, e);
        }

        public void Pong(Pong pong)
        {
            OnPongCallback(new PongEventArgs(pong.OperationId, pong.Message));
        }

        public void SubscribePong(EventHandler<PongEventArgs> handler)
        {
            PongCallback += handler;
        }
        public void UnsubscribePong(EventHandler<PongEventArgs> handler)
        {
            PongCallback -= handler;
        }

    }
}
