using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Client.Test.Util;
using Communication.Service.Ping;
using Microsoft.Test.ApplicationControl;
using NUnit.Framework;

namespace Client.Test
{
    [TestFixture]
    public class PingPongTest
    {
        private AutomatedApplication _hostProcess;
        private PongService _pongService = new PongService();
        private PingPong _pingPong;

        #region setup
        [TestFixtureSetUp]
        public void SetupFixture()
        {
            _hostProcess = ProcessHelper.Start(ConfigurationManager.AppSettings.Get("Client.Test.host-exe-file"));
            _pongService = new PongService();
            _pingPong = new PingPong("Ping", _pongService);
        }
        #endregion

        [Test]
        public void DoCompletes()
        {
            string argument = "hello";
            string expected = "olleh";
            //this should complete quite quickly
            string actual = _pingPong.Do(argument);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException(typeof(Exception), ExpectedMessage = "Error")]
        public void DoErrorCompletes()
        {
            string argument = "hello";
            //this should complete quite quickly
            string actual = _pingPong.DoError(argument);
        }

        #region teardown
        [TestFixtureTearDown]
        public void TeardownFixture()
        {
            ProcessHelper.Stop(_hostProcess);
        }
        #endregion
    }
}
