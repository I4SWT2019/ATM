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
    public class AreaPrinter
    {
        private AreaMonitorPrinter _uut;
        private Plane _plane1;
        private Plane _plane2;
        private StringWriter _sw;

        [SetUp]
        public void Setup()
        {
            _plane1 = Substitute.For<Plane>();
            _plane2 = Substitute.For<Plane>();
            _sw = Substitute.For<StringWriter>();
            _uut = new AreaMonitorPrinter();
            

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
            // WriteLine is called 1 + 6*n where n is number of Plane in planes
            _uut.Print(planes);
            // Assert
            _sw.Received(1+(planes.Count*6)).WriteLine(Arg.Any<string>());
        }
    }
}
