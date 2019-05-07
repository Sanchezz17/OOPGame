using System.Collections.Generic;
using System.Linq;
using OOP_Game.Units;
using System.Windows;

namespace OOP_Game.GameLogic
{
    public class Game
    {
        public Vector p; 
        public int CurrentLevelNumber { get; private set; }
        private readonly List<Level> Levels;
        public Level CurrentLevel => Levels[CurrentLevelNumber];
        public bool GameIsOver { get; private set; }
        public Game(List<Level> levels)
        {
            CurrentLevelNumber = 0;
            Levels = levels;
        }


        public void MakeGameIteration()
        {
            var objectsForAdd = new HashSet<IGameObject>();
            foreach (var gameObject in CurrentLevel.Map.ForEachGameObject())
            {
                if (gameObject is IMovable movable && gameObject.State == State.Moves)
                {
                    movable.Move();
                    if (gameObject is IMalefactor && gameObject.Position.X <= 0)
                        GameIsOver = true;
                }
            }

            for (var i = 0; i < CurrentLevel.Map.Height; i++)
            {
                var malefactors = (HashSet<IMalefactor>) CurrentLevel.Map.GetMalefactorFromLine(i);
                foreach (var malefactor in malefactors)
                {
                    malefactor.State = State.Moves;
                }
                foreach (var hero in CurrentLevel.Map.GetHeroesFromLine(i))
                {
                    if (malefactors.Count != 0)
                        objectsForAdd.Add(hero.Attack());
                    foreach (var malefactor in malefactors)
                    {
                        if ((malefactor.Position - hero.Position).Length < 1)
                        {
                            malefactor.State = State.Attacks;
                            hero.Trigger(malefactor.Attack());
                        }
                        else
                        {
                            malefactor.State = State.Moves;
                        }
                    }
                }

                foreach (var strike in CurrentLevel.Map.ForEachStrikes())
                {
                    foreach (var malefactor in malefactors)
                    {
                        if ((malefactor.Position - strike.Position).Length < .1)
                        {
                            malefactor.Trigger(strike);
                        }
                    }
                }
                
            }
            
            foreach (var gameObject in objectsForAdd)
            {
                CurrentLevel.Map.Add(gameObject);
            }

            var objectsForDelete = new HashSet<IGameObject>();
            foreach (var gameObject in CurrentLevel.Map.ForEachGameObject())
            {
                if (gameObject.IsDead || gameObject.Position.X > 10)
                    objectsForDelete.Add(gameObject);
            }
            foreach (var gameObject in objectsForDelete)
            {
                CurrentLevel.Map.Delete(gameObject);
            }
            
        }
    }
}