using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ATM.Calculators;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ATM.Test.Unit
{
    
    [TestFixture]
    [Author("I4SWT2019Gr2")]
    public class HeadingCalcTestUnit
    {

        HeadingCalculator uut;


        [SetUp]
        public void Setup()
        { uut = new HeadingCalculator();}

        [Test]
        public void Calculate_ReceivedZeroAsDataPoints_ReturnZero()
        {

            //Arrange
            int[] oldData = new int[] 
                {0000, 0000};

            int[] newData = new int[]
                {0000, 0000};

            double Heading = 0;
            //Act

            Heading = uut.Calculate(oldData, newData);

            //Assert

            Assert.That(Heading, Is.EqualTo(0));
        }

        [Test]
        public void Calculate_ReceivedTwoDataPoints_ReturnAngle()
        {

            //Arrange
            int[] oldData = new int[]
                {0000, 0000};
                // x     y
            int[] newData = new int[]
                {0000, 0001};

            double Heading = 0;
            //Act

            Heading = uut.Calculate(oldData, newData);

            //Assert

            Assert.That(Heading, Is.EqualTo(90));
        }

        [Test]
        public void Calculate_ReceivedDataPoints_ReturnBigAngleValue()
        {

            //Arrange
            int[] oldData = new int[]
                {0000, 0001};
                // x     y
            int[] newData = new int[]
                {0000, 0000};

            double Heading = 0;
            //Act

            Heading = uut.Calculate(oldData, newData);

            //Assert

            Assert.That(Heading, Is.EqualTo(270.00));
        }

        [Test]
        public void Calculate_ReceivedDataPoints_ReturnBiggerAngleValue()
        {

            //Arrange
            int[] oldData = new int[]
                {0000, 0001};
                // x     y
            int[] newData = new int[]
                {0001, 0000};

            double Heading = 0;
            //Act

            Heading = uut.Calculate(oldData, newData);

            //Assert

            Assert.That(Heading, Is.EqualTo(315.00));
        }

        [Test]
        public void Calculate_ReceivedDataPoints_ReturnZeroAgain()
        {

            //Arrange
            int[] oldData = new int[]
                {0000, 0000};
            // x     y
            int[] newData = new int[]
                {0001, 0000};

            double Heading = 0;
            //Act

            Heading = uut.Calculate(oldData, newData);

            //Assert

            Assert.That(Heading, Is.EqualTo(0.00));
        }
    }
}
