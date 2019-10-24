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


        [SetUp]
        public void Setup()
        {
            _uut = new ATM(_areaPrinter, _velocityCalculator, _headingCalculator);

            _velocityCalculator = Substitute.For<ICalculator>();
            _headingCalculator = Substitute.For<ICalculator>();
            _areaPrinter = Substitute.For<IPrinter>();
        }

        [Test]
        public void Print_areaPrinterCalled()
        {
            // Arrange
            Plane plane = new Plane();
            List<Plane> planes = new List<Plane>();
            planes.Add(plane);

            // Act
            _uut.Print();
        
            // Assert
            _areaPrinter.Received().Print(planes);
        }

    }
}
