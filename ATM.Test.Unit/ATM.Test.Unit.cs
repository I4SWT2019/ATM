using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace ATM.Test.Unit
{
    [TestFixture]
    [Author("I4SWT2019Gr2")]
    public class ATMUnitTest
    {
        private TestClass _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new TestClass();
        }

        [Test]
        public void Test_ReturnsTrue()
        {
            Assert.That(_uut.Test(),Is.True);
        }
    }
}
