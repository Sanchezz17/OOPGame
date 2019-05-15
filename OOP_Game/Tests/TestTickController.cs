using NUnit.Framework;
using OOP_Game.Infrastructure;

namespace OOP_Game.Tests
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