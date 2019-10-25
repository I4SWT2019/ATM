using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfacess;
using NSubstitute;
using NUnit.Framework;

namespace ATM.Test.Unit
{
    [TestFixture]
    [Author("I4SWT2019Gr2")]
    public class ATMUnitTest
    {
        private ATM _uut;
        private ICalculator _velocityCalculator;
        private ICalculator _headingCalculator;
        private IPrinter _areaPrinter;
        private Plane _plane1;
        private Plane _plane2;
        private Plane _plane3;


        [SetUp]
        public void Setup()
        {
            _velocityCalculator = Substitute.For<ICalculator>();
            _headingCalculator = Substitute.For<ICalculator>();
            _areaPrinter = Substitute.For<IPrinter>();
            _plane1 = Substitute.For<Plane>();
            _plane2 = Substitute.For<Plane>();
            _plane3 = Substitute.For<Plane>();

            _uut = new ATM(_areaPrinter, _velocityCalculator, _headingCalculator);
        }

        [Test]
        public void Print_areaPrinterCalled()
        {
            // Nothing to Arrange
            // Act
            _uut.Print();

            // Assert
            _areaPrinter.Received().Print(default);
        }

        [Test]
        public void Update_planesIsUpdated()
        {
            // Arrange
            List<Plane> planes = new List<Plane>
            {
                _plane1,
                _plane2
            };
            List<Plane> newPlanes = new List<Plane>()
            {
                _plane3
            };

            // Act
            _uut.Update(planes);
            _uut.Print();
            _uut.Update(newPlanes);
            _uut.Print();

            // Assert
            Received.InOrder(() =>
            {
                _areaPrinter.Print(planes);
                _areaPrinter.Print(newPlanes);
            });
        }

        [Test]
        public void CalcVelocity_VelocityCalculatorCalledWithValues()
        {
            // Arrange
            _plane1._tag = "ABC123";
            _plane2._tag = "DEF123";
            _plane3._tag = _plane1._tag;
            _plane1._timestamp = "xxxxxxxxxxxx00003";
            _plane2._timestamp = "xxxxxxxxxxxx00001";
            _plane3._timestamp = "xxxxxxxxxxxx00001";
            _plane1._latitude = 1;
            _plane1._longitude = 1;
            _plane3._latitude = 3;
            _plane3._longitude = 3;
            int[] p1 = new int[] {1, 1, 00003};
            int[] p3 = new int[] {3, 3, 00001};
            List<Plane> previousPlanes = new List<Plane>()
            {
                _plane2,
                _plane3
            };
            List<Plane> planes = new List<Plane>()
            {
                _plane1
            };
            _uut.Update(previousPlanes);
            _uut.Update(planes);
            // Act
            _uut.CalcVelocity();

            // Assert
            _velocityCalculator.ReceivedWithAnyArgs(1).Calculate(p3, p1);
        }

        [Test]
        public void CalcVelocity_VelocityCalculatorNotCalled()
        {
            // Arrange
            List<Plane> previousPlanes = new List<Plane>()
            {
                _plane2,
                _plane3
            };
            List<Plane> planes = new List<Plane>()
            {
            };
            _uut.Update(previousPlanes);
            _uut.Update(planes);
            // Act
            _uut.CalcVelocity();

            // Assert
            _velocityCalculator.DidNotReceive().Calculate(default, default);
        }

        [Test]
        public void CalcVelocity_HeadingCalculatorCalledWithValues()
        {
            // Arrange
            _plane1._tag = "ABC123";
            _plane2._tag = "DEF123";
            _plane3._tag = _plane1._tag;
            _plane1._latitude = 1;
            _plane1._longitude = 1;
            _plane3._latitude = 3;
            _plane3._longitude = 3;
            int[] p1 = new int[] { 1, 1 };
            int[] p3 = new int[] { 3, 3 };
            List<Plane> previousPlanes = new List<Plane>()
            {
                _plane2,
                _plane3
            };
            List<Plane> planes = new List<Plane>()
            {
                _plane1
            };
            _uut.Update(previousPlanes);
            _uut.Update(planes);
            // Act
            _uut.CalcHeading();

            // Assert
            _headingCalculator.ReceivedWithAnyArgs(1).Calculate(p3, p1);
        }

        [Test]
        public void CalcVelocity_HeadingCalculatorNotCalled()
        {
            // Arrange
            List<Plane> previousPlanes = new List<Plane>()
            {
                _plane2,
                _plane3
            };
            List<Plane> planes = new List<Plane>()
            {
            };
            _uut.Update(previousPlanes);
            _uut.Update(planes);
            // Act
            _uut.CalcVelocity();

            // Assert
            _headingCalculator.DidNotReceive().Calculate(default, default);
        }
    }
}
