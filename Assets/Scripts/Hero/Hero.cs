using UnityEngine;

namespace Hero
{
    public class Hero
    {
        private readonly GameObject gameObject;
        public HeroAttribute HeroAttribute { get; private set; }

        public bool IsAvailable { get; set; }

        private readonly HeroComponent heroComponent;
        
        private Enemy enemy;
        
        private static int Id = 1;

        public Hero()
        {
            HeroAttribute = new HeroAttribute($"Hero {Id++}");
            IsAvailable = false;
            
            gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gameObject.name = HeroAttribute.Name;
            
            heroComponent = gameObject.AddComponent<HeroComponent>();
            heroComponent.Construct(this);
        }

        public void Move(Vector3 position)
        {
            var transform = gameObject.transform;
            transform.position = position;
        }

        // ReSharper disable once ParameterHidesMember
        public void PrepareForBattle(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public void TakeDamage(int attackPower)
        {
            var currentAttribute = HeroAttribute;
            currentAttribute.Health -= attackPower;
            HeroAttribute = currentAttribute;
            if (HeroAttribute.Health <= 0)
            {
                Die();
            }
        }
        
        public void Attack()
        {
            enemy.TakeDamage(HeroAttribute.AttackPower);
            Debug.Log($"{HeroAttribute.Name} attacked.");
        }

        private void Die()
        {
            SetActive(false);
        }
        
        public void SetActive(bool isActive) => gameObject.SetActive(isActive);
        public bool IsActive => gameObject.activeSelf;
    }
}
