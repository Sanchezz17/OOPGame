using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using OOP_Game.Units;

namespace OOP_Game.GameLogic
{
    public class Map
    {
        private List<HashSet<IMalefactor>> linesMalefactors;
        private List<HashSet<IHero>> linesHeroes;
        private List<HashSet<IStrike>> linesStrikes;
        public int Height { get; }
        public int Width { get; }

        public Map(int height, int width)
        {
            Height = height;
            Width = width;
            linesMalefactors = CreateLines<IMalefactor>(Height);
            linesHeroes = CreateLines<IHero>(Height);
            linesStrikes = CreateLines<IStrike>(Height);
        }

        private static List<HashSet<T>> CreateLines<T>(int count)
        {
            return Enumerable.Range(0, count)
                .Select(i => new HashSet<T>())
                .ToList();
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
                default:
                    throw new ArgumentException();
            }
        }
    }
}