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

        [TestCase(1500,1800)]
        [TestCase(1500,1801)]
        [TestCase(500,20000)]
        [Test]
        public void WarningCheck_AltitudeOkay_NoPrintCall(int altitude1, int altitude2)
        {
            //Arrange
            _plane1._altitude = altitude1;
            _plane2._altitude = altitude2;
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

        [TestCase(1500, 1799)]
        [TestCase(1500,1798)]
        [TestCase(500,500)]
        [Test]
        public void WarningCheck_AltitudeNotOkay_PrintReceivedCall(int altitude1, int altitude2)
        {
            //Arrange
            _plane1._altitude = altitude1;
            _plane2._altitude = altitude2;
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

        [TestCase(1500, 1800, 2300, 2300, 2500, 7500)]
        [TestCase(1500, 1800, 2300, 2300, 2500, 7501)]
        [TestCase(1500, 1800, 2500, 7501, 2300, 2300)]
        [TestCase(1500, 1800, 2500, 7500, 2300, 2300)]
        [TestCase(1500, 1800, 10000, 90000, 10000, 90000)]
        [TestCase(1500, 1800, 90000, 10000, 90000, 10000)]
        [Test]
        public void WarningCheck_AltitudeAndDistanceOkay_NoPrinterCall(
            int altitude1, int altitude2,
            int latitude1, int latitude2,
            int longitude1, int longitude2)
        {
            //Arrange
            _plane1._altitude = 1000;
            _plane1._latitude = 1000;
            _plane1._longitude = 1000;
            _plane2._altitude = 1300;
            _plane2._latitude = 3500;
            _plane2._longitude = 5500;
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

        [TestCase(1500, 1800, 2500, 7499, 2300, 2300)]
        [TestCase(1500, 1800, 2300, 2300, 2500, 7499)]
        [TestCase(1499, 1800, 2500, 7499, 2300, 2300)]
        [TestCase(1499, 1800, 2300, 2300, 2500, 7499)]
        [Test]
        public void WarningCheck_AltitudeOkayDistanceNotOkay_NoPrintCall(
            int altitude1, int altitude2,
            int latitude1, int latitude2,
            int longitude1, int longitude2)
        {
            //Arrange
            _plane1._altitude = altitude1;
            _plane1._latitude = latitude1;
            _plane1._longitude = longitude1;
            _plane2._altitude = altitude2;
            _plane2._latitude = latitude2;
            _plane2._longitude = longitude2;
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

        [TestCase(1500, 1799, 2500, 7500, 2300, 2300)]
        [TestCase(1500, 1799, 2300, 2300, 2500, 7500)]
        [TestCase(500, 500, 2500, 7500, 2300, 2300)]
        [TestCase(500, 500, 2300, 2300, 2500, 7500)]
        [TestCase(1500, 1799, 2500, 7501, 2300, 2300)]
        [TestCase(1500, 1799, 2300, 2300, 2500, 7501)]
        [TestCase(500, 500, 2500, 7501, 2300, 2300)]
        [TestCase(500, 500, 2300, 2300, 2500, 7501)]
        [Test]
        public void WarningCheck_AltitudeNotOkayDistanceOkay_NoPrintCall(
            int altitude1, int altitude2,
            int latitude1, int latitude2,
            int longitude1, int longitude2)
        {
            //Arrange
            _plane1._altitude = altitude1;
            _plane1._latitude = latitude1;
            _plane1._longitude = longitude1;
            _plane2._altitude = altitude2;
            _plane2._latitude = latitude2;
            _plane2._longitude = longitude2;
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

        [TestCase(1500, 1799, 2500, 7499, 2300, 2300)]
        [TestCase(1500, 1799, 2300, 2300, 2500, 7499)]
        [TestCase(500, 500, 2500, 7499, 2300, 2300)]
        [TestCase(500, 500, 2300, 2300, 2500, 7499)]
        [TestCase(1500, 1799, 2500, 2500, 2300, 2300)]
        [TestCase(1500, 1799, 2300, 2300, 2500, 2500)]
        [TestCase(500, 500, 2500, 2500, 2300, 2300)]
        [TestCase(500, 500, 2300, 2300, 2500, 2500)]
        [Test]
        public void WarningCheck_AltitudeAndDistanceNotOkay_PrintCalled(
            int altitude1, int altitude2,
            int latitude1, int latitude2,
            int longitude1, int longitude2)
        {
            //Arrange
            _plane1._altitude = altitude1;
            _plane1._latitude = latitude1;
            _plane1._longitude = longitude1;
            _plane2._altitude = altitude2;
            _plane2._latitude = latitude2;
            _plane2._longitude = longitude2;
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

        [TestCase(1500, 1799, 1800, 2500, 7499, 7499, 2300, 2300, 2300)]
        [TestCase(1500, 1799, 1801, 2500, 7499, 7499, 2300, 2300, 2300)]
        [TestCase(1500, 1799, 1800, 2300, 2300, 2300, 2500, 7499, 7499)]
        [TestCase(1500, 1799, 1801, 2300, 2300, 2300, 2500, 7499, 7499)]
        [TestCase(1500, 1799, 1800, 2300, 2300, 2300, 2500, 2500, 2500)]
        [TestCase(1500, 1799, 1801, 2300, 2300, 2300, 2500, 2500, 2500)]
        [Test]
        public void WarningCheck_ThreePlanesDistanceAndAltitudeNotOkayThirdAltitudeOkay_PrintCalledOnce(
            int altitude1, int altitude2, int altitude3,
            int latitude1, int latitude2, int latitude3,
            int longitude1, int longitude2, int longitude3)
        {
            //Arrange
            _plane1._altitude = altitude1;
            _plane1._latitude = latitude1;
            _plane1._longitude = longitude1;
            _plane2._altitude = altitude2;
            _plane2._latitude = latitude2;
            _plane2._longitude = longitude2;
            _plane3._altitude = altitude3;
            _plane3._latitude = latitude3;
            _plane3._longitude = longitude3;
            List<Plane> planes = new List<Plane>()
            {
                _plane1,
                _plane2,
                _plane3
            };
            //Act
            _uut.Update(planes);
            //Assert
            // Print is called 2 times, Plane1 & Plane2 warning and Plane2 and Plane3 warning
            _separationPrinter.ReceivedWithAnyArgs(2).Print(default);
        }

        [TestCase(1000, 701, 1299, 2500, 7499, 7499, 2300, 2300, 2300)]
        [TestCase(1000, 701, 1299, 2300, 2300, 2300, 2500, 7499, 7499)]
        [TestCase(1000, 701, 1299, 2500, 2500, 2500, 2300, 2300, 2300)]
        [Test]
        // BVA
        public void WarningCheck_FirstPlaneWithTwoWarnings(
            int altitude1, int altitude2, int altitude3,
            int latitude1, int latitude2, int latitude3,
            int longitude1, int longitude2, int longitude3)
        {
            //Arrange
            _plane1._altitude = altitude1;
            _plane1._latitude = latitude1;
            _plane1._longitude = longitude1;
            _plane2._altitude = altitude2;
            _plane2._latitude = latitude2;
            _plane2._longitude = longitude2;
            _plane3._altitude = altitude3;
            _plane3._latitude = latitude3;
            _plane3._longitude = longitude3;
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
