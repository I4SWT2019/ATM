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
        private PlaneAddedEventArgs _receivedEventArgs;
        private ITransponderReceiver _fakeTransponderReceiver;
        private int EventCount;

        [SetUp]
        public void Setup()
        {
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();

            _uut = new DataHandler(_fakeTransponderReceiver);
            _receivedEventArgs = null;
            EventCount = 0;


            _uut.HandleData("XYZ987;25059;75654;4000;20151006213456789");
            _uut.PlaneAddedEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                    EventCount++;
                };
        }

        [Test]
        public void HandleData_DataIsHandled_EventFired()
        {
            _uut.HandleData("BCD123;10005;85890;12000;20151006213456789");
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void HandleData_DataIsHandled_CorrectNewDataReceived()
        {
            _uut.HandleData("BCD123;10005;85890;12000;20151006213456789");
            Assert.That(_receivedEventArgs.Plane._tag, Is.EqualTo("BCD123"));
            Assert.That(_receivedEventArgs.Plane._latitude, Is.EqualTo(10005));
            Assert.That(_receivedEventArgs.Plane._longitude, Is.EqualTo(85890));
            Assert.That(_receivedEventArgs.Plane._altitude, Is.EqualTo(12000));
            Assert.That(_receivedEventArgs.Plane._timestamp, Is.EqualTo("20151006213456789"));
        }

        [Test]
        public void HandleData_DataIsHandled_TwoEventsFired()
        {
            _uut.HandleData("ABC123;10005;85890;12000;20151006213456789");
            _uut.HandleData("CDE456;10005;85890;12000;20151006213456789");

            Assert.That(EventCount, Is.EqualTo(2));
        }
    }
}
