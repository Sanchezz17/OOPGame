using System.Collections.Generic;
using System.Linq;
using Domain.Units;

namespace Domain.GameLogic
{
    public class Game
    {
        public int CurrentLevelNumber { get; private set; }
        private readonly List<Level> Levels;
        public Level CurrentLevel => Levels[CurrentLevelNumber];
        public bool IsOver { get; private set; }
        public bool IsWin { get; private set; }
        public Player Player { get; }
        public Game(List<Level> levels, Player player)
        {
            CurrentLevelNumber = 0;
            Levels = levels;
            Player = player;
        }
        
        public void MakeGameIteration()
        {
            MakeWave();
            MakeGem();
            ResetStates();
            MakeAttacks();
            CheckStrikes();
            UpdateGameObjects();
            MakeMove();
            IsOver = CheckGameOver();
        }

        private void MakeWave()
        {
            foreach (var wave in CurrentLevel.Waves)
            {
                if (wave.IsReadyToStart() && !wave.IsPassed)
                {
                    foreach (var malefactors in wave.Malefactors)
                        CurrentLevel.Map.Add(malefactors);
                    wave.IsPassed = true;
                    CurrentLevel.PassedWaves++;
                }
            }
        }

        private void MakeGem()
        {
            foreach (var gemManufacturer in CurrentLevel.Map.GemManufacturers)
            {
                if (gemManufacturer.IsAvailableGem())
                    CurrentLevel.Map.Add(gemManufacturer.GetGem());
            }
        }

        private bool CheckGameOver()
        {
            return CurrentLevel.Map.Malefactors
                .Any(malefactor => malefactor.Position.X < -1);
        }

        private void MakeAttacks()
        {
            var objectsForAdd = new HashSet<IGameObject>();
            for (var i = 0; i < CurrentLevel.Map.Height; i++)
            {
                var malefactors = CurrentLevel.Map.GetMalefactorFromLine(i);
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
                    var isAttackAvailable = attackingHero.IsAttackAvailable();
                    var needAttack = malefactors.Any(malefactor => malefactor.Position.X > hero.Position.X);
                    if (needAttack)
                    {
                        hero.State = State.Attacks;
                        if (isAttackAvailable)
                            objectsForAdd.Add(attackingHero.Attack());
                    }
                    foreach (var gameObject in objectsForAdd)
                        CurrentLevel.Map.Add(gameObject);
                }
            }
        }

        private void CheckStrikes()
        {
            for (var i = 0; i < CurrentLevel.Map.Height; i++)
            {
                var malefactors = CurrentLevel.Map.GetMalefactorFromLine(i);
                foreach (var strike in CurrentLevel.Map.Strikes)
                {
                    foreach (var malefactor in malefactors)
                        if ((malefactor.Position - strike.Position).Length < 0.3)
                            malefactor.Trigger(strike);
                }
            }
        }

        private void MakeMove()
        {
            foreach (var gameObject in CurrentLevel.Map.GetGameObjects())
                if (gameObject is IMovable movable && gameObject.State == State.Moves)
                    movable.Move();
        }

        private void UpdateGameObjects()
        {
            var objectsForDelete = new HashSet<IGameObject>();
            foreach (var gameObject in CurrentLevel.Map.GetGameObjects())
            {
                if (gameObject.IsDead || !CurrentLevel.Map.Contains(gameObject.Position) && !(gameObject is IMalefactor))
                    objectsForDelete.Add(gameObject);
            }
            foreach (var gameObject in objectsForDelete)
            {
                if (gameObject is BaseMalefactor malefactor)
                    Player.Coins += malefactor.Cost;
                CurrentLevel.Map.Delete(gameObject);
            }
        }

        private void ResetStates()
        {
            foreach (var hero in CurrentLevel.Map.Heroes)
                hero.State = hero is IGemManufacturer ? State.Produce : State.Idle;
            foreach (var malefactor in CurrentLevel.Map.Malefactors)
                malefactor.State = State.Moves;
        }

        public void ToNextLevel()
        {
            if (CurrentLevelNumber == Levels.Count - 1)
            {
                IsWin = true;
                return;
            }
            CurrentLevelNumber++;
        }
    }
}