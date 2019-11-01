using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;
using ATM.Interfacess;

namespace ATM.Test.Unit
{
    [TestFixture]
    [Author("I4SWT2019Gr2")]
    public class AreaMonitorUnitTest
    {
        private AreaMonitor _uut;
        private DataHandler _dataHandler;
        private PlaneAddedEventArgs _receivedPlaneEventArgs;
        private int _planeEventCount;
        private IObserver _fakeObserver;
        private IObserver _fakeObserver1;
        private IDataHandler _fakeDataHandler;

        private Plane _plane1;
        private Plane _plane2;


        [SetUp]
        public void SetUp()
        {
            _fakeDataHandler = Substitute.For<IDataHandler>();
            _fakeObserver = Substitute.For<IObserver>();
            _receivedPlaneEventArgs = null;
            _planeEventCount = 0;

            _plane1 = new Plane();
            _plane2 = new Plane();

            _uut = new AreaMonitor(_fakeDataHandler);
        }


        [Test]
        public void Attach_ObserverAddedToListOfObservers()
        {

            _uut.Attach(_fakeObserver);

            Assert.That(_uut._observers.Contains(_fakeObserver),Is.True);

        }
        [Test]
        public void Detach_ObserverRemovedFromListOfObservers()
        {
            _uut.Detach(_fakeObserver);

            Assert.That(_uut._observers.Contains(_fakeObserver), Is.False);
        }

        [Test]
        public void UpdateArea_PlaneInAreaAddedToList()
        {
            _plane1.setAll(_plane1, "ABC123", 10005, 85890, 12000, "20151006213456789");

            _uut.UpdateArea(_plane1, true);

            Assert.That(_uut._planesInArea.First()._tag, Is.EqualTo(_plane1._tag));
        }

        [Test]
        public void HandleReceivedData_ReceptionWorkingWithTransponder()
        {
            _plane1.setAll(_plane1, "ABC123",10005,85890,12000,"20151006213456789");
            _plane2.setAll(_plane2, "DEF123", 10005, 85890, 12000, "20151006213456789");

            _fakeDataHandler.PlaneAddedEvent
                += Raise.EventWith(this, new PlaneAddedEventArgs(_plane1));

            _uut.HandleReceivedData(this,new PlaneAddedEventArgs(_plane1));

            Assert.That(_uut.EventFromDataHandlerReceived, Is.True);
        }
    }
}
