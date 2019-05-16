using Domain.Units;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;

namespace Domain.Units
{
    public class Characteristic
    {
        public string Name { get; private set; }
        public int Value { get; private set; }
        public int UpgradePrice { get; private set; }
        private int baseUpgradePrice = 50;

        public Characteristic(string name, int value)
        {
            Name = name;
            Value = value;
            UpgradePrice = baseUpgradePrice;
        }

        public void Upgrade(int upgradeValue)
        {
            Value += upgradeValue;
            UpgradePrice += 25;
        }

        public override bool Equals(object obj)
        {
            var characteristic = obj as Characteristic;
            return characteristic is null ? false : characteristic.Name == Name;

        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    public class UnitParameters
    {
        public readonly HashSet<Characteristic> characteristics = new HashSet<Characteristic>();

        public UnitParameters SetHealth(int value)
        {
            characteristics.Add(new Characteristic("Health", value));
            return this;
        }

        public UnitParameters SetDamage(int value)
        {
            characteristics.Add(new Characteristic("Damage", value));
            return this;
        }

        public UnitParameters SetReload(int value)
        {
            characteristics.Add(new Characteristic("Reload", value));
            return this;
        }

        public int Health
        {
            get
            {
                return characteristics
                    .Where(c => c.Name == "Health")
                    .Select(c => c.Value)
                    .First();
            }
        }

        public int Damage
        {
            get
            {
                return characteristics
                    .Where(c => c.Name == "Damage")
                    .Select(c => c.Value)
                    .First();
            }
        }

        public int Reload
        {
            get
            {
                return characteristics
                   .Where(c => c.Name == "Reload")
                   .Select(c => c.Value)
                   .First();
            }
        }
    }
}
