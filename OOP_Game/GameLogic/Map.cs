using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using OOP_Game.Units;

namespace OOP_Game.GameLogic
{
    public class Map
    {
        private List<HashSet<IMalefactor>> linesMalefactors;
        private List<HashSet<IHero>> linesHeroes;
        private List<HashSet<IStrike>> linesStrikes;
        private List<Gem> gems;
        public IEnumerable<Gem> Gems => gems;
        public int Height { get; }
        public int Width { get; }

        public Map(int height, int width)
        {
            Height = height;
            Width = width;
            linesMalefactors = CreateLines<IMalefactor>(Height);
            linesHeroes = CreateLines<IHero>(Height);
            linesStrikes = CreateLines<IStrike>(Height);
            gems = new List<Gem>();
        }

        private static List<HashSet<T>> CreateLines<T>(int count)
        {
            return Enumerable.Range(0, count)
                .Select(i => new HashSet<T>())
                .ToList();
        }

        public bool Contains(Vector vector)
        {
            return vector.X >= 0
                   && vector.X <= Width
                   && vector.Y >= 0
                   && vector.Y <= Height;
        }

        public IEnumerable<IHero> GetHeroesFromLine(int numberLine)
        {
            return linesHeroes[numberLine];
        }
        
        public IEnumerable<IMalefactor> GetMalefactorFromLine(int numberLine)
        {
            return linesMalefactors[numberLine];
        }

        public IEnumerable<IGameObject> ForEachGameObject()
        {
            foreach (var hero in ForEachHeroes())
                yield return hero;
            foreach (var malefactor in ForEachMalefactors())
                yield return malefactor;
            foreach (var strike in ForEachStrikes())
                yield return strike;
            foreach (var gem in Gems)
                yield return gem;
        }

        public IEnumerable<IHero> ForEachHeroes()
        {
            return linesHeroes.SelectMany(line => line);
        }

        public IEnumerable<IMalefactor> ForEachMalefactors()
        {
            return linesMalefactors.SelectMany(line => line);
        }

        public IEnumerable<IStrike> ForEachStrikes()
        {
            return linesStrikes.SelectMany(strike => strike);
        }

        public void Add(IGameObject gameObject)
        {
            var numberLine = (int)gameObject.Position.Y;
            switch (gameObject)
            {
                case IHero hero:
                    linesHeroes[numberLine].Add(hero);
                    break;
                case IMalefactor malefactor:
                    linesMalefactors[numberLine].Add(malefactor);
                    break;
                case IStrike strike:
                    linesStrikes[numberLine].Add(strike);
                    break;
                case Gem gem:
                    gems.Add(gem);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        
        public void Delete(IGameObject gameObject)
        {
            var numberLine = (int)gameObject.Position.Y;
            switch (gameObject)
            {
                case IHero hero:
                    linesHeroes[numberLine].Remove(hero);
                    break;
                case IMalefactor malefactor:
                    linesMalefactors[numberLine].Remove(malefactor);
                    break;
                case IStrike strike:
                    linesStrikes[numberLine].Remove(strike);
                    break;
                case Gem gem:
                    gems.Remove(gem);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}