using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using OOP_Game.Units;

namespace OOP_Game.GameLogic
{
    public class Map
    {
        private readonly List<HashSet<IGameObject>> linesMalefactors;
        private readonly List<HashSet<IGameObject>> linesHeroes;
        private readonly List<HashSet<IGameObject>> linesStrikes;
        private readonly HashSet<IGameObject> gems;
        private readonly HashSet<IGameObject> gemManufacturers;
        public IEnumerable<IHero> Heroes => linesHeroes.SelectMany(line => line).Cast<IHero>().ToList();
        public IEnumerable<IMalefactor> Malefactors => linesMalefactors.SelectMany(line => line).Cast<IMalefactor>().ToList();
        public IEnumerable<IStrike> Strikes => linesStrikes.SelectMany(strike => strike).Cast<IStrike>().ToList();
        public IEnumerable<IGemManufacturer> GemManufacturers => gemManufacturers.Cast<IGemManufacturer>().ToList();
        public IEnumerable<Gem> Gems => gems.Cast<Gem>().ToList();
        public int Height { get; }
        public int Width { get; }

        public Map(int height, int width)
        {
            Height = height;
            Width = width;
            linesMalefactors = CreateLines<IGameObject>(Height);
            linesHeroes = CreateLines<IGameObject>(Height);
            linesStrikes = CreateLines<IGameObject>(Height);
            gemManufacturers = new HashSet<IGameObject> {new GemFactory()};
            gems = new HashSet<IGameObject>();
        }

        private static List<HashSet<T>> CreateLines<T>(int count)
        {
            return Enumerable.Range(0, count)
                .Select(i => new HashSet<T>())
                .ToList();
        }
        
        private void ChangeMap(Action<HashSet<IGameObject>, IGameObject> changer, IGameObject gameObject)
        {
            var numberLine = (int)gameObject.Position.Y;
            switch (gameObject)
            {
                case IGemManufacturer gemManufacturer:
                    changer(linesHeroes[numberLine],(IHero)gemManufacturer);
                    changer(gemManufacturers, gemManufacturer);
                    break;
                case IHero hero:
                    changer(linesHeroes[numberLine], hero);
                    break;
                case IMalefactor malefactor:
                    changer(linesMalefactors[numberLine] ,malefactor);
                    break;
                case IStrike strike:
                    changer(linesStrikes[numberLine], strike);
                    break;
                case Gem gem:
                    changer(gems, gem);
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public bool Contains(Vector vector)
        {
            return vector.X >= 0
                   && vector.X < Width
                   && vector.Y >= 0
                   && vector.Y < Height;
        }

        public IEnumerable<IHero> GetHeroesFromLine(int numberLine)
        {
            return linesHeroes[numberLine].Cast<IHero>().ToList();
        }
       
        public IEnumerable<IMalefactor> GetMalefactorFromLine(int numberLine)
        {
            return linesMalefactors[numberLine].Cast<IMalefactor>().ToList();
        }

        public IEnumerable<IGameObject> GetGameObjects()
        {
            foreach (var hero in Heroes)
                yield return hero;
            foreach (var malefactor in Malefactors)
                yield return malefactor;
            foreach (var strike in Strikes)
                yield return strike;
            foreach (var gem in Gems)
                yield return gem;
        }

        public void Add(IGameObject gameObject)
        {
            ChangeMap((set, o) => set.Add(o), gameObject);
        }
        
        public void Delete(IGameObject gameObject)
        {
            ChangeMap((set, o) => set.Remove(o), gameObject);
        }
    }
}