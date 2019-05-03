using System;
using System.Linq;
using System.Collections.Generic;
using OOP_Game.Units;

namespace OOP_Game.GameLogic
{
    public class Map
    {
        private List<HashSet<IMalefactor>> linesMalefactors;
        private List<HashSet<IHero>> linesHeroes; 
        private int height;
        private int width;

        public Map(int height, int width)
        {
            this.height = height;
            this.width = width;
            linesMalefactors = CreateLines<IMalefactor>(this.height);
            linesHeroes = CreateLines<IHero>(this.height);
        }

        private static List<HashSet<T>> CreateLines<T>(int count)
        {
            return Enumerable.Range(0, count)
                .Select(i => new HashSet<T>())
                .ToList();
        }

        public IEnumerable<IGameObject> ForEachGameObject()
        {
            foreach (var hero in ForEachHeroes())
                yield return hero;
            foreach (var malefactor in ForEachMalefactors())
                yield return malefactor;
        }

        public IEnumerable<IHero> ForEachHeroes()
        {
            return linesHeroes.SelectMany(line => line);
        }

        public IEnumerable<IMalefactor> ForEachMalefactors()
        {
            return linesMalefactors.SelectMany(line => line);
        }

        public void Add(IGameObject gameObject)
        {
            var numberLine = gameObject.Position.Y;
            switch (gameObject)
            {
                case IHero hero:
                    linesHeroes[numberLine].Add(hero);
                    break;
                case IMalefactor malefactor:
                    linesMalefactors[numberLine].Add(malefactor);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        
        public void Delete(IGameObject gameObject)
        {
            var numberLine = gameObject.Position.Y;
            switch (gameObject)
            {
                case IHero hero:
                    linesHeroes[numberLine].Remove(hero);
                    break;
                case IMalefactor malefactor:
                    linesMalefactors[numberLine].Remove(malefactor);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}