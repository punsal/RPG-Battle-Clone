using System.Collections.Generic;

namespace Hero
{
    public struct EnemyAttribute
    {
        public readonly string Name;
        public int Health;
        public int AttackPower;

        public EnemyAttribute(string name, List<HeroAttribute> heroAttribute)
        {
            var random = new System.Random();
            Name = name;
            Health = heroAttribute.GetAverageAttackPower() * random.Next(8, 12);
            AttackPower = heroAttribute.GetTotalHealth() / random.Next(8, 12);
        }
    }
}