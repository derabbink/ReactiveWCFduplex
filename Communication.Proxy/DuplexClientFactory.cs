using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Communication.Proxy
{
    /// <summary>
    /// generates wrapped client channels for duplex clients
    /// </summary>
    /// <typeparam name="TService">service contract</typeparam>
    /// <typeparam name="TCallbackService">callback service contract</typeparam>
    public class DuplexClientFactory<TService, TCallbackService>
    {
        //creating this object is expensive
        private readonly DuplexChannelFactory<TService> _channelFactory;
        private readonly InstanceContext _instanceContext;

        /// <summary>
        /// </summary>
        /// <param name="endpointName">name of the endpoint configuration</param>
        /// <param name="callbackHandler">implementation of callback handler</param>
        public DuplexClientFactory(string endpointName, TCallbackService callbackHandler)
        {
            // Construct InstanceContext to handle messages on callback interface
            _instanceContext = new InstanceContext(callbackHandler);

            _channelFactory = new DuplexChannelFactory<TService>(_instanceContext, endpointName);
        }

        public DuplexClient<TService> GetClient()
        {
            TService channel = _channelFactory.CreateChannel();
            return new DuplexClient<TService>(channel);
        }
    }
}
