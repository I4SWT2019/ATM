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
        private Plane _plane4;


        [SetUp]
        public void Setup()
        {
            _velocityCalculator = Substitute.For<ICalculator>();
            _headingCalculator = Substitute.For<ICalculator>();
            _areaPrinter = Substitute.For<IPrinter>();
            _plane1 = new Plane();
            _plane2 = new Plane();
            _plane3 = new Plane();
            _plane4 = new Plane();

            _plane1._tag = "ABC123";
            _plane2._tag = "DEF123";
            _plane3._tag = _plane1._tag;
            _plane4._tag = _plane2._tag;
            _plane1._timestamp = "xxxxxxxxxxxx00001";
            _plane2._timestamp = "xxxxxxxxxxxx00002";
            _plane3._timestamp = "xxxxxxxxxxxx00003";
            _plane4._timestamp = "xxxxxxxxxxxx00004";
            _plane1._latitude = 1;
            _plane1._longitude = 1;
            _plane2._latitude = 2;
            _plane2._longitude = 2;
            _plane3._latitude = 3;
            _plane3._longitude = 3;
            _plane4._latitude = 4;
            _plane4._longitude = 4;

            _uut = new ATM(_areaPrinter, _velocityCalculator, _headingCalculator);
        }

        [Test]
        public void Update_FirstPlaneEntryWithEmptyList_NoInterfaceCalls()
        {
            // Arrange
            List<Plane> planes= new List<Plane>();
            // Act
            _uut.Update(planes);
            // Assert
            _areaPrinter.DidNotReceive().Print(planes);
            _velocityCalculator.DidNotReceiveWithAnyArgs().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
            _headingCalculator.DidNotReceiveWithAnyArgs().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
        }

        [Test]
        public void Update_FirstPlaneEntryWithOnePlane_NoInterfaceCalls()
        {
            // Arrange
            List<Plane> planes = new List<Plane>()
            {
                _plane1
            };
            // Act
            _uut.Update(planes);
            // Assert
            _areaPrinter.DidNotReceive().Print(planes);
            _velocityCalculator.DidNotReceiveWithAnyArgs().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
            _headingCalculator.DidNotReceiveWithAnyArgs().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
        }

        [Test]
        public void Update_FirstPlaneEntryWithTwoPlanes_NoInterfaceCalls()
        {
            // Arrange
            List<Plane> planes = new List<Plane>()
            {
                _plane1,
                _plane2
            };
            // Act
            _uut.Update(planes);
            // Assert
            _areaPrinter.DidNotReceive().Print(planes);
            _velocityCalculator.DidNotReceiveWithAnyArgs().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
            _headingCalculator.DidNotReceiveWithAnyArgs().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
        }

        [Test]
        public void Update_TwoPlaneEntriesCurrentEmptyPreviousEmpty_NoInterfaceCalls()
        {
            // Arrange
            List<Plane> planes = new List<Plane>();
            List<Plane> prevPlanes = new List<Plane>();
            // Act
            _uut.Update(prevPlanes);
            _uut.Update(planes);
            // Assert
            _areaPrinter.Received().Print(planes);
            _velocityCalculator.DidNotReceiveWithAnyArgs().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
            _headingCalculator.DidNotReceiveWithAnyArgs().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
        }

        [Test]
        public void Update_TwoPlaneEntriesCurrentEmptyPreviousOnePlane_PrintCalled()
        {
            // Arrange
            List<Plane> planes = new List<Plane>();
            List<Plane> prevPlanes = new List<Plane>()
            {
                _plane1
            };
            // Act
            _uut.Update(prevPlanes);
            _uut.Update(planes);
            // Assert
            _areaPrinter.Received().Print(planes);
            _velocityCalculator.DidNotReceiveWithAnyArgs().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
            _headingCalculator.DidNotReceiveWithAnyArgs().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
        }

        [Test]
        public void Update_TwoPlaneEntriesCurrentEmptyPreviousTwoPlanes_PrinterCalled()
        {
            // Arrange
            List<Plane> planes = new List<Plane>();
            List<Plane> prevPlanes = new List<Plane>()
            {
                _plane1,
                _plane2
            };
            // Act
            _uut.Update(prevPlanes);
            _uut.Update(planes);
            // Assert
            _areaPrinter.Received().Print(planes);
            _velocityCalculator.DidNotReceiveWithAnyArgs().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
            _headingCalculator.DidNotReceiveWithAnyArgs().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
        }

        [Test]
        public void Update_TwoPlaneEntriesCurrentHasOnePreviousIsEmpty_PrintCalled()
        {
            // Arrange
            List<Plane> planes = new List<Plane>()
            {
                _plane1
            };
            List<Plane> prevPlanes = new List<Plane>();
            
            // Act
            _uut.Update(prevPlanes);
            _uut.Update(planes);
            // Assert
            _areaPrinter.Received().Print(planes);
            _velocityCalculator.DidNotReceiveWithAnyArgs().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
            _headingCalculator.DidNotReceiveWithAnyArgs().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
        }

        [Test]
        public void Update_TwoPlaneEntriesCurrentHasOnePreviousHasOne_InterfacesCalled()
        {
            // Arrange
            List<Plane> planes = new List<Plane>()
            {
                _plane3
            };

            List<Plane> prevPlanes = new List<Plane>()
            {
                _plane1
            };
            int[] p1Vel = new Int32[] {1, 1, 00001};
            int[] p3Vel = new Int32[] {3, 3, 00003};

            int[] p1Head = new Int32[] { 1, 1};
            int[] p3Head = new Int32[] { 3, 3};

            // Act
            _uut.Update(prevPlanes);
            _uut.Update(planes);

            // Assert
            _areaPrinter.Received().Print(planes);
            _velocityCalculator.Received(1).Calculate(
                Arg.Is<int[]>(x => p1Vel.SequenceEqual(x)),
                Arg.Is<int[]>(x => p3Vel.SequenceEqual(x))
            );
            _headingCalculator.Received(1).Calculate(
                Arg.Is<int[]>(x => p1Head.SequenceEqual(x)),
                Arg.Is<int[]>(x => p3Head.SequenceEqual(x))
            );
        }

        [Test]
        public void Update_TwoPlaneEntriesCurrentHasOnePreviousHasTwo_InterfacesCalled()
        {
            // Arrange
            List<Plane> planes = new List<Plane>()
            {
                _plane3
            };
            List<Plane> prevPlanes = new List<Plane>()
            {
                _plane1,
                _plane2
            };
            int[] p1Vel = new Int32[] { 1, 1, 00001 };
            int[] p3Vel = new Int32[] { 3, 3, 00003 };

            int[] p1Head = new Int32[] { 1, 1 };
            int[] p3Head = new Int32[] { 3, 3 };

            // Act
            _uut.Update(prevPlanes);
            _uut.Update(planes);
            // Assert
            _areaPrinter.Received().Print(planes);
            _velocityCalculator.Received(1).Calculate(
                Arg.Is<int[]>(x => p1Vel.SequenceEqual(x)),
                Arg.Is<int[]>(x => p3Vel.SequenceEqual(x))
            );
            _headingCalculator.Received(1).Calculate(
                Arg.Is<int[]>(x => p1Head.SequenceEqual(x)),
                Arg.Is<int[]>(x => p3Head.SequenceEqual(x))
            );
        }

        [Test]
        public void Update_TwoPlaneEntriesCurrentHasTwoPreviousEmpty_PrintCalled()
        {
            // Arrange
            List<Plane> planes = new List<Plane>()
            {
                _plane1,
                _plane2
            };

            List<Plane> prevPlanes = new List<Plane>();
            // Act
            _uut.Update(prevPlanes);
            _uut.Update(planes);
            // Assert
            _areaPrinter.Received().Print(planes);
            _velocityCalculator.DidNotReceiveWithAnyArgs().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
            _headingCalculator.DidNotReceiveWithAnyArgs().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
        }

        [Test]
        public void Update_TwoPlaneEntriesCurrentHasTwoPreviousHasOne_InterfaceCalls()
        {
            // Arrange
            List<Plane> planes = new List<Plane>()
            {
                _plane1,
                _plane2
            };
            List<Plane> prevPlanes = new List<Plane>()
            {
                _plane3
            };
            int[] p1Vel = new Int32[] { 1, 1, 00001 };
            int[] p3Vel = new Int32[] { 3, 3, 00003 };
            int[] p1Head = new Int32[] { 1, 1 };
            int[] p3Head = new Int32[] { 3, 3 };
            // Act
            _uut.Update(prevPlanes);
            _uut.Update(planes);
            // Assert
            _areaPrinter.Received(1).Print(planes);
            _velocityCalculator.Received(1).Calculate(
                Arg.Is<int[]>(x => p3Vel.SequenceEqual(x)),
                Arg.Is<int[]>(x => p1Vel.SequenceEqual(x))
            );
            _headingCalculator.Received(1).Calculate(
                Arg.Is<int[]>(x => p3Head.SequenceEqual(x)),
                Arg.Is<int[]>(x => p1Head.SequenceEqual(x))
            );
        }
        [Test]
        public void Update_TwoPlaneEntriesCurrentHasTwoPreviousHasTwo_NoInterfaceCalls()
        {
            // Arrange
            List<Plane> planes = new List<Plane>()
            {
                _plane1,
                _plane2
            };
            List<Plane> prevPlanes = new List<Plane>()
            {
                _plane3,
                _plane4
            };
            int[] p1Vel = new Int32[] { 1, 1, 00001 };
            int[] p3Vel = new Int32[] { 3, 3, 00003 };
            int[] p1Head = new Int32[] { 1, 1 };
            int[] p3Head = new Int32[] { 3, 3 };
            int[] p2Vel = new Int32[] { 2, 2, 00002 };
            int[] p4Vel = new Int32[] { 4, 4, 00004 };
            int[] p2Head = new Int32[] { 2, 2 };
            int[] p4Head = new Int32[] { 4, 4 };
            // Act
            _uut.Update(prevPlanes);
            _uut.Update(planes);
            // Assert
            _areaPrinter.Received(1).Print(planes);
            Received.InOrder(() =>
            {
                _velocityCalculator.Received().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
                _velocityCalculator.Received().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
                _headingCalculator.Received().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
                _headingCalculator.Received().Calculate(Arg.Any<int[]>(), Arg.Any<int[]>());
            });
        }
    }
}
