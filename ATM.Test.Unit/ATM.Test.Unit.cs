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
            _plane1 = new Plane();
            _plane2 = new Plane();
            _plane3 = new Plane();

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
            _uut.Update(newPlanes);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(planes, Is.EqualTo(_uut._previousPlanes));
                Assert.That(newPlanes, Is.EqualTo(_uut._planes));
            });
        }

        [Test]
        public void CalcVelocity_NoPlanesIn_planes()
        {
            // Nothing to Arrange
            // Act
            _uut.CalcVelocity();

            //Assert
            _velocityCalculator.DidNotReceive().Calculate(default, default);
        }

        [Test]
        public void CalcVelocity_OnlyPlanesIn_Planes()
        {
            // Arrange
            List<Plane> previousPlanes = new List<Plane>()
            {
            };
            List<Plane> planes = new List<Plane>()
            {
                _plane2,
                _plane3
            };
            _uut._planes = planes;
            _uut._previousPlanes = previousPlanes;

            // Act
            _uut.CalcVelocity();

            // Assert
            _velocityCalculator.DidNotReceive().Calculate(default, default);
        }

        [Test]
        public void CalcVelocity_OnlyPlanesIn_previousPlanes()
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
            _uut._planes = planes;
            _uut._previousPlanes = previousPlanes;

            // Act
            _uut.CalcVelocity();

            // Assert
            _velocityCalculator.DidNotReceive().Calculate(default, default);
        }

        [Test]
        public void CalcVelocity_VelocityCalculatorCalledWithValues()
        {
            // Arrange
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
            _uut._planes = planes;
            _uut._previousPlanes = previousPlanes;
            // Act
            _uut.CalcVelocity();

            // Assert
            _velocityCalculator.Received(1).Calculate(
                Arg.Is<int[]>(x => p3.SequenceEqual(x)),
                Arg.Is<int[]>(x => p1.SequenceEqual(x))
            );
        }

        [Test]
        public void CalcHeading_NoPlanesIn_planes()
        {
            // Nothing to Arrange
            // Act
            _uut.CalcHeading();

            //Assert
            _headingCalculator.DidNotReceive().Calculate(default, default);
        }

        [Test]
        public void CalcHeading_OnlyPlanesIn_previousPlanes()
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
            _uut._planes = planes;
            _uut._previousPlanes = previousPlanes;
            // Act
            _uut.CalcHeading();

            // Assert
            _headingCalculator.DidNotReceive().Calculate(default, default);
        }

        [Test]
        public void CalcHeading_OnlyPlanesIn_Planes()
        {
            // Arrange
            List<Plane> previousPlanes = new List<Plane>()
            {
            };
            List<Plane> planes = new List<Plane>()
            {
                _plane2,
                _plane3
            };
            _uut._planes = planes;
            _uut._previousPlanes = previousPlanes;
            // Act
            _uut.CalcHeading();

            // Assert
            _headingCalculator.DidNotReceive().Calculate(default, default);
        }
        [Test]
        public void CalcHeading_HeadingCalculatorCalledWithValues()
        {
            // Arrange
           
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
            _uut._planes = planes;
            _uut._previousPlanes = previousPlanes;
            // Act
            _uut.CalcHeading();

            // Assert
            _headingCalculator.Received(1).Calculate(
                Arg.Is<int[]>(x => p3.SequenceEqual(x)),
                Arg.Is<int[]>(x => p1.SequenceEqual(x))
                );
        }
    }
}
