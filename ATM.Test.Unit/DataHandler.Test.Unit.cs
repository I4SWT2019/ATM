using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;
using ATM.Interfacess;
using TransponderReceiver;

namespace ATM.Test.Unit
{
    [TestFixture]
    [Author("I4SWT2019Gr2")]
    public class DataHandlerUnitTest
    {
        private DataHandler _uut;
        private PlaneAddedEventArgs _receivedPlaneEventArgs;
        private ITransponderReceiver _fakeTransponderReceiver;
        private int _planeEventCount;
        private RawTransponderDataEventArgs _receivedDataEventArgs;

        [SetUp]
        public void Setup()
        {
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();

            _uut = new DataHandler(_fakeTransponderReceiver);
            _receivedPlaneEventArgs = null;
            _planeEventCount = 0;

            _uut.PlaneAddedEvent +=
                (o, args) =>
                {
                    _receivedPlaneEventArgs = args;
                    _planeEventCount++;
                };
        }

        [Test]
        public void HandleData_ReceptionWorkingWithTransponder()
        {
            List<string> testData = new List<string>();
            testData.Add("ABC123;10005;85890;12000;20151006213456789");
            testData.Add("DEF123;10005;85890;12000;20151006213456789");
            testData.Add("GHJ123;10005;85890;12000;20151006213456789");

            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));
            
            _uut.ReceivedData(this, new RawTransponderDataEventArgs(testData));

            Assert.That(_uut.EventFromTransponderReceived, Is.True);
        }

        [Test]
        public void HandleData_DataIsHandled_EventFired()
        {
            _uut.HandleData("BCD123;10005;85890;12000;20151006213456789");
            Assert.That(_receivedPlaneEventArgs, Is.Not.Null);
        }

        [Test]
        public void HandleData_DataIsHandled_CorrectNewDataReceived()
        {
            _uut.HandleData("BCD123;10005;85890;12000;20151006213456789");
            Assert.Multiple(() =>
            {
                Assert.That(_receivedPlaneEventArgs.Plane._tag, Is.EqualTo("BCD123"));
                Assert.That(_receivedPlaneEventArgs.Plane._latitude, Is.EqualTo(10005));
                Assert.That(_receivedPlaneEventArgs.Plane._longitude, Is.EqualTo(85890));
                Assert.That(_receivedPlaneEventArgs.Plane._altitude, Is.EqualTo(12000));
                Assert.That(_receivedPlaneEventArgs.Plane._timestamp, Is.EqualTo("20151006213456789"));
            });
        }

        [Test]
        public void HandleData_DataIsHandled_TwoEventsFired()
        {
            _uut.HandleData("ABC123;10005;85890;12000;20151006213456789");
            _uut.HandleData("CDE456;10005;85890;12000;20151006213456789");

            Assert.That(_planeEventCount, Is.EqualTo(2));
        }
    }
}
