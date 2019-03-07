using System;
using FluentAssertions;
using NUnit.Framework;

namespace TechnicalTestUnitTests
{
    [TestFixture]
    public class UnitTest1
    {
        [TestCase()]
        public void TestMethod1()
        {
            bool t = true;
            t.Should().BeTrue();
        }
    }
}
