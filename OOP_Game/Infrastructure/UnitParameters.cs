using OOP_Game.Units;
using System;
using System.Collections.Generic;
using System.Windows;

namespace OOP_Game.Infrastructure
{
    public class UnitParameters
    {
        private readonly Dictionary<string, int> parametres = new Dictionary<string, int>();

        public UnitParameters SetHealth(int value)
        {
            parametres.Add("Health", value);
            return this;
        }

        public UnitParameters SetDamage(int value)
        {
            parametres.Add("Damage", value);
            return this;
        }

        public UnitParameters SetReload(int value)
        {
            parametres.Add("Reload", value);
            return this;
        }

        public int? Health
        {
            get
            {
                if (parametres.ContainsKey("Health"))
                    return parametres["Health"];
                else
                    return null;
            }
        }

        public int? Damage
        {
            get
            {
                if (parametres.ContainsKey("Damage"))
                    return parametres["Damage"];
                else
                    return null;
            }
        }

        public int? Reload
        {
            get
            {
                if (parametres.ContainsKey("Reload"))
                    return parametres["Reload"];
                else
                    return null;
            }
        }
    }
}
