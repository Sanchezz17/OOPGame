using System.Linq;
using NUnit.Framework;
using Domain.GameLogic;


namespace Tests
{
    [TestFixture]
    public class TestGame
    {
        [Test]
        public void TestStrikes()
        {
            var player = new Player();
            var game = GameFactory.GetStandardGame(player);
            var countStrikes = game.CurrentLevel.Map.Strikes.Count();
            for (var i = 0; i < 16; i++)
                game.MakeGameIteration();
            Assert.Less(countStrikes, game.CurrentLevel.Map.Strikes.Count());
        }

        [Test]
        public void TestMove()
        {
            var player = new Player();
            var game = GameFactory.GetStandardGame(player);
            var positions = game.CurrentLevel.Map.Malefactors.Select(malefactor => malefactor.Position);
            game.MakeGameIteration();
            var newPositions = game.CurrentLevel.Map.Malefactors.Select(malefactor => malefactor.Position);
            foreach (var oldPosition in positions)
                Assert.False(newPositions.All(position => position != oldPosition));
        }

        [Test]
        public void TestMalefactorDead()
        {
            var player = new Player();
            var game = GameFactory.GetStandardGame(player);
            var malefactor = game.CurrentLevel.Map.GetMalefactorFromLine(2).First();
            for (var i = 0; i < 1000; i++)
                game.MakeGameIteration();
            Assert.True(malefactor.IsDead);
        }

        [Test]
        public void TestCreationGems()
        {
            var player = new Player();
            var game = GameFactory.GetStandardGame(player);
            var countGems = game.CurrentLevel.Map.Gems.Count();
            for (var i = 0; i < 1000; i++)
                game.MakeGameIteration();
            Assert.Less(countGems, game.CurrentLevel.Map.Gems.Count());
        }

        [Test]
        public void TestAddingMalefactors()
        {
            var player = new Player();
            var game = GameFactory.GetStandardGame(player);
            var countMalefactors = game.CurrentLevel.Map.Malefactors.Count();
            for (var i = 0; i < 10000; i++)
                game.MakeGameIteration();
            Assert.Less(countMalefactors, game.CurrentLevel.Map.Malefactors.Count());
        }
        
    }
}