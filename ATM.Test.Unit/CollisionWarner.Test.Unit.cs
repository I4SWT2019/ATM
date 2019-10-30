using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ATM.Interfacess;
using ATM.Printers;
using NSubstitute;
using NUnit.Framework;

namespace ATM.Test.Unit
{
    [TestFixture]
    [Author("I4SWT2019Gr2")]
    public class CollisionWarnerUnitTest
    {
        private CollisionWarner _uut;
        private IPrinter _separationPrinter;
        private Plane _plane1;
        private Plane _plane2;
        private Plane _plane3;


        [SetUp]
        public void Setup()
        {
            _separationPrinter = Substitute.For<IPrinter>();
            _plane1 = new Plane();
            _plane2 = new Plane();
            _plane3 = new Plane();
            _uut = new CollisionWarner(_separationPrinter);
        }

        [Test]
        public void Print_OnePlaneInList()
        {
            //Arrange
            List<Plane> planes = new List<Plane>()
            {
                _plane1
            };
            //Act
            _uut.Print(planes);
            //Assert
            _separationPrinter.Received(1).Print(planes);
        }

        [Test]
        public void Print_TwoPlanesInList()
        {
            //Arrange
            List<Plane> planes = new List<Plane>()
            {
                _plane1,
                _plane2
            };
            //Act
            _uut.Print(planes);
            //Assert
            _separationPrinter.Received(1).Print(planes);
        }

        [Test]
        public void WarningCheck_NoPlanesInList()
        {
            //Arrange
            List<Plane> planes = new List<Plane>();
            //Act
            _uut.WarningCheck(planes);
            //Assert
            _separationPrinter.DidNotReceiveWithAnyArgs().Print(default);

        }

        [Test]
        public void WarningCheck_AltitudeOkay()
        {
            //Arrange
            _plane1._altitude = 1000;
            _plane2._altitude = 2000;
            List<Plane> planes = new List<Plane>()
            {
                _plane1,
                _plane2
            };
            //Act
            _uut.WarningCheck(planes);
            //Assert
            _separationPrinter.DidNotReceive().Print(planes);
        }

        [Test]
        public void WarningCheck_AltitudeAndDistanceOkay()
        {
            //Arrange
            _plane1._altitude = 1000;
            _plane1._latitude = 1000;
            _plane1._longitude = 1000;
            _plane2._altitude = 1300;
            _plane2._latitude = 3500;
            _plane2._longitude = 3500;
            List<Plane> planes = new List<Plane>()
            {
                _plane1,
                _plane2
            };
            //Act
            _uut.WarningCheck(planes);
            //Assert
            _separationPrinter.DidNotReceive().Print(planes);
        }

        [Test]
        public void WarningCheck_AltitudeNotOkayDistanceOkay()
        {
            //Arrange
            _plane1._altitude = 1000;
            _plane1._latitude = 1000;
            _plane1._longitude = 1000;
            _plane2._altitude = 1299;
            _plane2._latitude = 3500;
            _plane2._longitude = 3500;
            List<Plane> planes = new List<Plane>()
            {
                _plane1,
                _plane2
            };
            //Act
            _uut.WarningCheck(planes);
            //Assert
            _separationPrinter.DidNotReceive().Print(planes);
        }

        [Test]
        public void WarningCheck_AltitudeOkayDistanceNotOkay()
        {
            //Arrange
            _plane1._altitude = 1000;
            _plane1._latitude = 1000;
            _plane1._longitude = 1000;
            _plane2._altitude = 1300;
            _plane2._latitude = 3499;
            _plane2._longitude = 3500;
            List<Plane> planes = new List<Plane>()
            {
                _plane1,
                _plane2
            };
            //Act
            _uut.WarningCheck(planes);
            //Assert
            _separationPrinter.DidNotReceive().Print(planes);
        }

        [Test]
        public void WarningCheck_AltitudeNotOkayDistanceNotOkay()
        {
            //Arrange
            _plane1._altitude = 1000;
            _plane1._latitude = 1000;
            _plane1._longitude = 1000;
            _plane2._altitude = 1299;
            _plane2._latitude = 3500;
            _plane2._longitude = 3499;
            List<Plane> planes = new List<Plane>()
            {
                _plane1,
                _plane2
            };
            //Act
            _uut.WarningCheck(planes);
            //Assert
            _separationPrinter.Received(1).Print(Arg.Any<List<Plane>>());
        }

        [Test]
        public void Update_PlanesTooClose_SeparationPrinterCalled()
        {
            //Arrange
            _plane1._altitude = 1000;
            _plane1._latitude = 1000;
            _plane1._longitude = 1000;
            _plane2._altitude = 1299;
            _plane2._latitude = 3500;
            _plane2._longitude = 3500;
            List<Plane> planes = new List<Plane>()
            {
                _plane1,
                _plane2
            };
            //Act
            _uut.Update(planes);
            //Assert
            _separationPrinter.DidNotReceive().Print(planes);

        }

        [Test]
        public void WarningCheck_ThirdPlaneWithDifferentAltitude()
        {
            //Arrange
            _plane1._altitude = 1000;
            _plane1._latitude = 1000;
            _plane1._longitude = 1000;
            _plane2._altitude = 1000;
            _plane2._latitude = 5000;
            _plane2._longitude = 5000;
            _plane3._altitude = 1500;
            _plane3._latitude = 1000;
            _plane3._longitude = 1000;
            List<Plane> planes = new List<Plane>()
            {
                _plane1,
                _plane2,
                _plane3
            };
            //Act
            _uut.Update(planes);
            //Assert
            _separationPrinter.DidNotReceive().Print(planes);
        }
        
        [Test]
        public void WarningCheck_FirstPlaneWithTwoWarnings()
        {
            //Arrange
            _plane1._altitude = 2000;
            _plane1._latitude = 2000;
            _plane1._longitude = 2000;
            _plane2._altitude = 2299;
            _plane2._latitude = 1499;
            _plane2._longitude = 1499;
            _plane3._altitude = 1701;
            _plane3._latitude = 2499;
            _plane3._longitude = 2499;
            List<Plane> planes = new List<Plane>()
            {
                _plane1, 
                _plane2, 
                _plane3
            };
            //Act
            _uut.Update(planes);
            //Assert
            _separationPrinter.ReceivedWithAnyArgs(2).Print(default);
        }
    }
}
