using System.Linq;
using NUnit.Framework;
using Domain.GameLogic;


namespace Tests
{
    [TestFixture]
    public class TestGame
    {
        private Game GetGame() => new FactoryViaApi().Create(new Player());
        
        [Test]
        public void TestStrikes()
        {
            var game = GetGame();
            var countStrikes = game.CurrentLevel.Map.Strikes.Count();
            for (var i = 0; i < 16; i++)
                game.MakeGameIteration();
            Assert.Less(countStrikes, game.CurrentLevel.Map.Strikes.Count());
        }

        [Test]
        public void TestMove()
        {
            var game = GetGame();
            var positions = game.CurrentLevel.Map.Malefactors.Select(malefactor => malefactor.Position);
            game.MakeGameIteration();
            var newPositions = game.CurrentLevel.Map.Malefactors.Select(malefactor => malefactor.Position);
            foreach (var oldPosition in positions)
                Assert.False(newPositions.All(position => position != oldPosition));
        }

        [Test]
        public void TestMalefactorDead()
        {
            var game = GetGame();
            var malefactor = game.CurrentLevel.Map.GetMalefactorFromLine(2).First();
            for (var i = 0; i < 1000; i++)
                game.MakeGameIteration();
            Assert.True(malefactor.IsDead);
        }

        [Test]
        public void TestCreationGems()
        {
            var game = GetGame();
            var countGems = game.CurrentLevel.Map.Gems.Count();
            for (var i = 0; i < 1000; i++)
                game.MakeGameIteration();
            Assert.Less(countGems, game.CurrentLevel.Map.Gems.Count());
        }

        [Test]
        public void TestAddingMalefactors()
        {
            var game = GetGame();
            var countMalefactors = game.CurrentLevel.Map.Malefactors.Count();
            for (var i = 0; i < 10000; i++)
                game.MakeGameIteration();
            Assert.Less(countMalefactors, game.CurrentLevel.Map.Malefactors.Count());
        }
        
    }
}