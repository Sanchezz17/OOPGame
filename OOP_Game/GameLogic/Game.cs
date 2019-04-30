using System.Collections.Generic;
using OOP_Game.Units;

namespace OOP_Game.GameLogic
{
    public class Game
    {
        private int CurrentLevelNumber;
        private readonly List<Level> Levels;
        public Level CurrentLevel  => Levels[CurrentLevelNumber];

        public Game(List<Level> levels)
        {
            CurrentLevelNumber = 0;
            Levels = levels;
        }

        public void MakeGameIteration()
        {
            foreach (var gameObject in CurrentLevel.Map.ForEachGameObject())
            {
                if (gameObject is IMovable movable && gameObject.State == State.Moves) 
                    movable.Move();
                /*
                 * и другие взаимодействия с объектами
                 * 
                 * Надо обсудить как мы будем понимать, что объект должен атаковать
                 * т.е. как должен "зомби" понимать, что он стоит на одной клетке с "растением"
                 * или аналогично с "снарядом" и "зомби"
                 * вопрос скорее в том, как мы будем хранить всё, если всё в двумерном массиве, то ок.
                 */
            }
        }
    }
}