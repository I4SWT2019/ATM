using System;
using System.Collections.Generic;
using System.IO;
using ATM.Printers;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace ATM.Test.Unit
{
    [TestFixture]
    [Author("I4SWT2019Gr2")]
    public class SeparationPrinterUnitTest
    {
        private SeparationPrinter _uut;
        private Plane _plane1;
        private Plane _plane2;
        private StringWriter _sw;

        [SetUp]
        public void Setup()
        {
            _plane1 = Substitute.For<Plane>();
            _plane2 = Substitute.For<Plane>();
            _sw = Substitute.For<StringWriter>();
            _uut = new SeparationPrinter();


        }

        [Test]
        public void Print_PrintEmptyList()
        {
            // Arrange
            List<Plane> planes = new List<Plane>()
            {
            };
            Console.SetOut(_sw);
            // Act
            _uut.Print(planes);
            // Assert
            _sw.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Print_PrintListOfOnePlane()
        {
            // Arrange
            List<Plane> planes = new List<Plane>()
            {
                _plane1
            };
            Console.SetOut(_sw);
            // Act
            _uut.Print(planes);
            // Assert
            _sw.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Print_PrintListOfTwoPlanes()
        {
            // Arrange
            List<Plane> planes = new List<Plane>()
            {
                _plane1,
                _plane2
            };
            Console.SetOut(_sw);
            // Act
            _uut.Print(planes);
            // Assert
            _sw.Received(2).WriteLine(Arg.Any<string>());
        }
    }
}