using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Units;
using Domain.Units.Heroes;

namespace Domain.GameLogic
{
    public class Player
    {
        public int Coins { get; set; }
        public IDescribe[] Heroes { get; }
        public Player(IDescribe[] describesHeroes)
        {
            Coins = 100;
            Heroes = describesHeroes;
        }

        public IDescribe GetHeroParameters(Type heroType)
        {
            return Heroes
                    .First(c => c.Type == heroType);
        }
    }
}