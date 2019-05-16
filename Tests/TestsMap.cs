using System;
using System.Linq;
using System.Windows;
using NUnit.Framework;
using Domain.GameLogic;
using Domain.Units;
using Domain.Units.Heroes;
using Domain.Units.OOP_Game.Units.MaleFactors;

namespace Tests
{
    [TestFixture]
    public class TestsMap
    {
        [TestCase(1, 0, true)]
        [TestCase(0, 0, true)]
        [TestCase(9, 0, true)]
        [TestCase(0, 4, true)]
        [TestCase(9, 4, true)]
        [TestCase(10, 0, false)]
        [TestCase(-1, 0, false)]
        [TestCase(0, -5, false)]
        [TestCase(100, 500, false)]
        public void TestContains(int x, int y, bool isContains)
        {
            var map = new Map(5, 10);
            Assert.AreEqual(isContains, map.Contains(new Vector(x, y)));
        }

        [TestCase(typeof(IronMan))]
        [TestCase(typeof(Thanos))]
        [TestCase(typeof(Vision))]
        [TestCase(typeof(CaptainAmerica))]
        [TestCase(typeof(Thanos))]
        public void TestAdd(Type type)
        {
            var map = new Map(5, 10);
            var ctor = type.GetConstructor(new[] {typeof(int), typeof(Vector)});
            var gameObject = (IGameObject) ctor.Invoke(new object [] {1, new Vector(1, 1)});
            map.Add(gameObject);
            Assert.True(map.GetGameObjects().Contains(gameObject));
        }

        [TestCase(typeof(IronMan))]
        [TestCase(typeof(Thanos))]
        [TestCase(typeof(Vision))]
        [TestCase(typeof(CaptainAmerica))]
        [TestCase(typeof(Thanos))]
        public void TestRemove(Type type)
        {
            var map = new Map(5, 10);
            var ctor = type.GetConstructor(new[] {typeof(int), typeof(Vector)});
            var gameObject = (IGameObject) ctor.Invoke(new object [] {1, new Vector(1, 1)});
            map.Add(gameObject);
            map.Delete(gameObject);
            Assert.False(map.GetGameObjects().Contains(gameObject));
        }
        
        [TestCase(1, 1)]
        [TestCase(3, 1)]
        public void TestHeroOnLine(int x, int y)
        {
            var player = Player.Instance;
            var map = new Map(5, 10);
            var hero = new IronMan(player.GetHeroParametres(typeof(IronMan)).Parameters, new Vector(x, y));
            map.Add(hero);
            Assert.True(map.GetHeroesFromLine(y).Contains(hero));
            for (var i = 0; i < map.Height; i++)
            {
                if (i == y)
                    continue;
                Assert.False(map.GetHeroesFromLine(i).Contains(hero));
            }
        }
        
        
    }
}