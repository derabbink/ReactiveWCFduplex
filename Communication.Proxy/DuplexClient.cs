using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Communication.Proxy
{
    /// <summary>
    /// Wrapper class for WCF duplex channel that can disposed of without throwing exeptions
    /// </summary>
    /// <typeparam name="TService">service contract</typeparam>
    public class DuplexClient<TService> : IDisposable
    {
        public TService Channel { get; private set; }

        public DuplexClient(TService channel)
        {
            Channel = channel;
        }

        public void Dispose()
        {
            //Dispose() on ICommunicationObject can throw
            var channel = Channel as ICommunicationObject;
            try
            {
                channel.Close();
            }
            catch (CommunicationException)
            {
                channel.Abort();
            }
            catch (TimeoutException)
            {
                channel.Abort();
            }
        }
    }
}
