#Reactive WCF Duplex demo

This test app demonstrates the use of a WCF duplex service where the callback endpoint is wrapped in an IObservable.
Normally wrapping the callback event in an `IObservable.Replay()` (as done in `Client.PingPong.Do()`) would cause a deadlock when waiting for the `First()` element to arrive from WCF.
The reason for this behavior is that waiting for this `First()` element halts the executing thread, but the callback service (`Pong`) is also hosted on that thread. This would not only block the `Client`, but also the `Host`, since its message cannot be delivered.

**Solution:** Replacnig the `IObservable.Replay()` with `IObservable.Replay(Scheduler.Default)` (in `Client.PingPong.Do()`), which causes any waiting for Rx elements to happen on a different thread in the thread pool.
That way the `Pong` `ServiceHost` is not blocked any more, and the entire method can complete.

## How to run
 * Open the solution (in VS2010 SP1) and build all projects.
 * Either run an instance of the `Host` project, followed by running an instance of the `Client` project,...
 * ...or run all NUnit tests (there is just one) in `Client.Test`.