using NUnit.Framework;
using Domain.Infrastructure;

namespace Tests
{
    [TestFixture]
    public class TestTickController
    {
        [TestCase(10)]
        [TestCase(1)]
        [TestCase(9999999)]
        public void TestIsAvailable(int baseCountTick)
        {
            var tickController = new TickСontroller(baseCountTick);
            for (var i = 0; i < baseCountTick; i++)
                Assert.False(tickController.Check());
            Assert.True(tickController.Check());
        }
    }
}