using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using Communication.Contracts.Data;
using Communication.Contracts.Service;
using Communication.Proxy;
using Communication.Service.Events;
using Communication.Service.Ping;

namespace Client
{
    public class PingPong
    {
        private readonly PongService _pong;
        private readonly DuplexClient<IPing> _pingClient;
        private IObservable<PongEventArgs> _pongs;
        
        public PingPong(string pingEndpointConfigName, PongService pong)
        {
            _pong = pong;
            DuplexClientFactory<IPing, IPong> pingFactory = new DuplexClientFactory<IPing, IPong>(pingEndpointConfigName, pong);
            _pingClient = pingFactory.GetClient();

            SetupObservables();
        }

        private void SetupObservables()
        {
            _pongs = Observable.FromEventPattern<PongEventArgs>(_pong.SubscribePong, _pong.UnsubscribePong)
                .Select(ePattern => ePattern.EventArgs);
        }

        private IPing Channel { get { return _pingClient.Channel; } }

        public string Do(string message)
        {
            Guid operationId = Guid.NewGuid();
            IObservable<string> resultObs = _pongs
                .Where(eArgs => eArgs.OperationId == operationId)
                .Select(eArgs => eArgs.Message);

            IObservable<string> finalObs = Observable.Create((IObserver<string> observer) =>
                {
                    var resultOrErrorObs = Observable.Never<string>().Amb(resultObs);
                    var replay = resultOrErrorObs.Replay(Scheduler.Default);
                    replay.Connect();

                    var msg = new Ping() {OperationId = operationId, Message = message};
                    Channel.Ping(msg);

                    //no deadlock here, since replay is running on the default threadpool
                    var result = replay.First();
                    observer.OnNext(result);
                    observer.OnCompleted();
                    return Disposable.Empty;
                });
            return finalObs.Last();
        }
    }
}
