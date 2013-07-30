using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Communication.Proxy
{
    /// <summary>
    /// generates wrapped client channels
    /// </summary>
    /// <typeparam name="TService">service contract</typeparam>
    public class ClientFactory<TService>
    {
        //creating this object is expensive
        private readonly ChannelFactory<TService> _channelFactory;

        /// <summary>
        /// </summary>
        /// <param name="endpointName">name of the endpoint configuration</param>
        public ClientFactory(string endpointName)
        {
            _channelFactory = new ChannelFactory<TService>(endpointName);
        }

        public Client<TService> GetClient()
        {
            TService channel = _channelFactory.CreateChannel();
            return new Client<TService>(channel);
        }
    }
}
