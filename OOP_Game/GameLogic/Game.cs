using System.Collections.Generic;
using System.Linq;
using OOP_Game.Units;

namespace OOP_Game.GameLogic
{
    public class Game
    {
        public bool Started { get; private set; }
        public int CurrentLevelNumber { get; private set; }
        private readonly List<Level> Levels;
        public Level CurrentLevel => Levels[CurrentLevelNumber];
        public bool GameIsOver { get; private set; }
        private GemFactory gemFactory;
        public Game(List<Level> levels)
        {
            Started = false;
            CurrentLevelNumber = 0;
            Levels = levels;
            gemFactory = new GemFactory();
        }

        public void Start() => Started = true;

        public void Pause() => Started = false;
        
        public void MakeGameIteration()
        {
            MakeGem();
            ResetStates();
            MakeAttacks();
            CheckStrikes();
            UpdateGameObjects();
            MakeMove();
            GameIsOver = CheckGameOver();
        }

        public void MakeGem()
        {
            foreach (var gemManufacturer in CurrentLevel.Map.GemManufacturers())
            {
                if (gemManufacturer.IsAvailable())
                    CurrentLevel.Map.Add(gemManufacturer.GetGem());
            }
        }

        private bool CheckGameOver()
        {
            return CurrentLevel.Map.Malefactors()
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
                        if ((malefactor.Position - hero.Position).Length < 0.8)
                        {
                            if (malefactor.IsAttackAvailable())
                                hero.Trigger(malefactor.Attack());
                            malefactor.State = State.Attacks;
                        }
                    }
                    
                    if (!(hero is IAttacking))
                        continue;
                    var attackingHero = hero as IAttacking;
                    foreach (var malefactor in malefactors)
                    {
                        if (malefactor.Position.X > hero.Position.X)
                        {
                            if (attackingHero.IsAttackAvailable())
                                objectsForAdd.Add(attackingHero.Attack());
                            hero.State = State.Attacks;
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
                foreach (var strike in CurrentLevel.Map.Strikes())
                {
                    foreach (var malefactor in malefactors)
                        if ((malefactor.Position - strike.Position).Length < 0.3)
                            malefactor.Trigger(strike);
                }              
            }
        }

        private void MakeMove()
        {
            foreach (var gameObject in CurrentLevel.Map.GameObjects())
                if (gameObject is IMovable movable && gameObject.State == State.Moves)
                    movable.Move();
        }

        private void UpdateGameObjects()
        {
            var objectsForDelete = new HashSet<IGameObject>();
            foreach (var gameObject in CurrentLevel.Map.GameObjects())
            {
                if (gameObject.IsDead || (!CurrentLevel.Map.Contains(gameObject.Position) && !(gameObject is IMalefactor)))
                    objectsForDelete.Add(gameObject);
            }
            foreach (var gameObject in objectsForDelete)
                CurrentLevel.Map.Delete(gameObject);
        }

        private void ResetStates()
        {
            foreach (var hero in CurrentLevel.Map.Heroes())
                hero.State = hero is IGemManufacturer ? State.Produce : State.Idle;
            foreach (var malefactor in CurrentLevel.Map.Malefactors())
                malefactor.State = State.Moves;
        }
    }
}