using System.Collections.Generic;
using System.Linq;
using OOP_Game.Units;

namespace OOP_Game.GameLogic
{
    public class Game
    {
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
            ResetStates();
            MakeAttacks();
            CheckStrikes();
            UpdateGameObjects();
            MakeMove();
            GameIsOver = CheckGameOver();
        }

        private bool CheckGameOver()
        {
            return CurrentLevel.Map.ForEachMalefactors()
                .Any(malefactor => malefactor.Position.X < -1);
        }

        private void MakeAttacks()
        {
            var objectsForAdd = new HashSet<IGameObject>();
            for (var i = 0; i < CurrentLevel.Map.Height; i++)
            {
                var malefactors = (HashSet<IMalefactor>) CurrentLevel.Map.GetMalefactorFromLine(i);
                foreach (var hero in CurrentLevel.Map.GetHeroesFromLine(i))
                {
                    foreach (var malefactor in malefactors)
                    {
                        if (malefactor.Position.X > hero.Position.X)
                        {
                            if (hero.IsAttackAvailable())
                                objectsForAdd.Add(hero.Attack());
                            hero.State = State.Attacks;
                        }
                    }
                    
                    foreach (var malefactor in malefactors)
                    {
                        if ((malefactor.Position - hero.Position).Length < 0.8)
                        {
                            if (malefactor.IsAttackAvailable())
                                hero.Trigger(malefactor.Attack());
                            malefactor.State = State.Attacks;
                        }
                    }
                }
            }
            foreach (var gameObject in objectsForAdd)
                CurrentLevel.Map.Add(gameObject);
        }

        private void CheckStrikes()
        {
            for (var i = 0; i < CurrentLevel.Map.Height; i++)
            {
                var malefactors = (HashSet<IMalefactor>) CurrentLevel.Map.GetMalefactorFromLine(i);
                foreach (var strike in CurrentLevel.Map.ForEachStrikes())
                {
                    foreach (var malefactor in malefactors)
                        if ((malefactor.Position - strike.Position).Length < 0.3)
                            malefactor.Trigger(strike);
                }              
            }
        }

        private void MakeMove()
        {
            foreach (var gameObject in CurrentLevel.Map.ForEachGameObject())
                if (gameObject is IMovable movable && gameObject.State == State.Moves)
                    movable.Move();
        }

        private void UpdateGameObjects()
        {
            var objectsForDelete = new HashSet<IGameObject>();
            foreach (var gameObject in CurrentLevel.Map.ForEachGameObject())
            {
                if (gameObject.IsDead || gameObject.Position.X > 10)
                    objectsForDelete.Add(gameObject);
            }
            foreach (var gameObject in objectsForDelete)
                CurrentLevel.Map.Delete(gameObject);
        }

        private void ResetStates()
        {
            foreach (var hero in CurrentLevel.Map.ForEachHeroes())
                hero.State = State.Idle;
            foreach (var malefactor in CurrentLevel.Map.ForEachMalefactors())
                malefactor.State = State.Moves;
        }
    }
}