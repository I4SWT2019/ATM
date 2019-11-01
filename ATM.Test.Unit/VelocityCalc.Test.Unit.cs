using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATM;

namespace ATM.Test.Unit
{
    [TestFixture]

    public class VelocityCalcTestUnit
    {
        Calculators.VelocityCalculator uut;

        [SetUp]
        public void Setup()
        {
            uut = new Calculators.VelocityCalculator();
        }

        [Test]
        public void Calculate_ReceiveTwoPositiveDataPoints_returnVelocity()
        {
            //Arrange
            int[] oldData = new int[]
                { 0000, 0000, 00000};

            int[] newData = new int[]
                { 0003, 0004, 01000};

            double result;
            //Act

            result = uut.Calculate(oldData, newData);

            //Assert

            Assert.That(result, Is.EqualTo(5));

        }

        [Test]
        public void Calculate_ReceiveTwoPositiveDataPointsReverseOrder_returnVelocity()
        {
            //this data would return a negative number, if the Absolute-keyword values does not work
            //Arrange
            int[] oldData = new int[]
                { 0003, 0004, 01000};

            int[] newData = new int[]
                { 0000, 0000, 00000};

            double result = 0;
            //Act

            result = uut.Calculate(oldData, newData);

            //Assert

            Assert.That(result, Is.EqualTo(5));

        }

        [Test]
        public void Calculate_ReceiveTwoDataPointsMoreThanFiftySecApart_returnVelocity()
        {
            //Data overflow on time measured at 59.5 secs and 0.5 secs, can it calculate the correct speed?
            //Arrange
            int[] oldData = new int[]
                { 0003, 0004, 59500};

            int[] newData = new int[]
                { 0000, 0000, 00500};

            double result = 0;
            //Act

            result = uut.Calculate(oldData, newData);

            //Assert

            Assert.That(result, Is.EqualTo(5.00));

        }

        [Test]
        public void Calculate_ReceiveOneDataPointDivisionByZero_returnZero()
        {
            //Division by zero, avoid system crash
            //Arrange
            int[] oldData = new int[]
                { 0000, 0000, 00000};

            int[] newData = new int[]
                { 0000, 0000, 00000};

            double result = 0;
            //Act

            result = uut.Calculate(oldData, newData);

            //Assert

            Assert.That(result, Is.EqualTo(0.00));

        }
    }
}
