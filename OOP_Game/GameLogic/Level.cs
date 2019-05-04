namespace OOP_Game.GameLogic
{
    public class Level
    {
        public Map Map { get; }
        public int Score { get; }

        public Level(Map map)
        {
            Score = 0;
            Map = map;
        }

    }
}