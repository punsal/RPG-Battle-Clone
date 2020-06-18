using System.Collections.Generic;

namespace Hero
{
    public static class HeroAttributeExtension
    {
        public static int GetTotalHealth(this IEnumerable<HeroAttribute> heroAttributes)
        {
            var totalHealth = 0;
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var heroAttribute in heroAttributes)
            {
                totalHealth += heroAttribute.Health;
            }

            return totalHealth;
        }

        public static int GetAverageAttackPower(this IEnumerable<HeroAttribute> heroAttributes)
        {
            var totalAttackPower = 0;
            var heroCount = 0;
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var heroAttribute in heroAttributes)
            {
                totalAttackPower += heroAttribute.AttackPower;
                heroCount++;
            }

            if (heroCount < 1)
            {
                return 0;
            }

            var averageAttackPower = totalAttackPower / heroCount;
            return averageAttackPower;
        }
    }
}