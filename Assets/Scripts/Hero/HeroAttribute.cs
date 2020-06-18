namespace Hero
{
    public struct HeroAttribute
    {
        public readonly string Name;
        public int Health;
        public int AttackPower;
        public int Experience;
        public int Level;

        public HeroAttribute(string name)
        {
            var random = new System.Random();
            Name = name;
            Health = random.Next(80, 120);
            AttackPower = random.Next(5, 15);
            Experience = 0;
            Level = 1;
        }
    }
}