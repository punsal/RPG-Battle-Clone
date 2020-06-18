using System.Collections.Generic;
using UnityEngine;

namespace Hero
{
    public class Enemy
    {
        private readonly GameObject gameObject;
        private readonly EnemyComponent enemyComponent;
        
        public EnemyAttribute EnemyAttribute { get; private set; }

        private readonly List<Hero> heroes;
        
        public Enemy(List<Hero> heroes)
        {
            this.heroes = heroes;
            
            var heroAttributes = new List<HeroAttribute>();
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var hero in this.heroes)
            {
                heroAttributes.Add(hero.HeroAttribute);
            }
            
            EnemyAttribute = new EnemyAttribute("Enemy", heroAttributes);
            
            gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gameObject.transform.localScale = Vector3.one * 3f;
            gameObject.name = EnemyAttribute.Name;

            enemyComponent = gameObject.AddComponent<EnemyComponent>();
            enemyComponent.Construct(this);
        }

        public void Move(Vector3 position)
        {
            var transform = gameObject.transform;
            transform.position = position;
        }

        public void TakeDamage(int attackPower)
        {
            var currentAttribute = EnemyAttribute;
            currentAttribute.Health -= attackPower;
            EnemyAttribute = currentAttribute;
            if (EnemyAttribute.Health <= 0)
            {
                Die();    
            }
        }
        
        public void Attack()
        {
            var random = new System.Random();
            
            var isSelectedAHero = false;
            var selectedHero = heroes[0];
            while (!isSelectedAHero)
            {
                selectedHero = heroes[random.Next(0, heroes.Count)];
                if (selectedHero.HeroAttribute.Health <= 0)
                {
                    heroes.Remove(selectedHero);
                    continue;
                }

                isSelectedAHero = true;
            }
            
            selectedHero.TakeDamage(EnemyAttribute.AttackPower);
            
            Debug.Log($"{EnemyAttribute.Name} attacked to {selectedHero.HeroAttribute.Name}.");
        }

        private void Die()
        {
            SetActive(false);
        }
        
        public void SetActive(bool isActive) => gameObject.SetActive(isActive);
        public bool IsActive => gameObject.activeSelf;
    }
}