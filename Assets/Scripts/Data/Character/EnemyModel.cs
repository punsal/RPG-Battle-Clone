using System.Collections.Generic;
using System.Linq;
using Data.Character.Abstract;
using UnityEngine;

namespace Data.Character
{
    [CreateAssetMenu(fileName = "Enemy Model", menuName = "Model/Enemy Model", order = 0)]
    public class EnemyModel : CharacterModel
    {
        [Header("Battle Attributes")]
        [SerializeField] private float currentHealth;
        public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

        [SerializeField] private List<HeroModel> heroes;

        public void Construct(List<HeroModel> heroModels)
        {
            heroes = heroModels;

            var totalAttackPower = heroes.Sum(model => model.AttackPower);
            var totalHealth = heroes.Sum(model => model.Health);

            var averageAttackPower = totalAttackPower / heroes.Count;
            var averageHealth = totalHealth / heroes.Count;

            health = averageHealth * Random.Range(1.25f, 2f);
            attackPower = averageAttackPower * Random.Range(1f, 2.5f);
        }
    }
}