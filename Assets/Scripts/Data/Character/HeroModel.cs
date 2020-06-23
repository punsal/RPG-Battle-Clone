using System;
using Data.Character.Abstract;
using Data.Character.Upgrade;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data.Character
{
    [Serializable]
    public class PersistentHeroModel
    {
        
    }
    
    [CreateAssetMenu(fileName = "Hero Model", menuName = "Model/Hero Model", order = 0)]
    public class HeroModel : CharacterModel
    {
        [Header("Hero Attributes")]
        [SerializeField] protected int experience;
        public int Experience => experience;
        
        [SerializeField] protected bool isAvailable;

        public bool IsAvailable
        {
            get => isAvailable; 
            set => isAvailable = value;
        }
        
        [SerializeField] protected bool isSelected;

        public bool IsSelected
        {
            get => isSelected; 
            set => isSelected = value;
        }
        
        [Header("Battle Attributes")]
        [SerializeField] protected float currentHealth;

        public float CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = value;
        }

        [Header("Upgrade")]
        [SerializeField] protected UpgradeData upgradeData;
        public UpgradeData UpgradeData => upgradeData;

        private static int Id = 1;

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private void Awake()
        {
            isAvailable = Id < 3;
            name = $"Hero {Id++}";
            health = Random.Range(80f, 120f);
            attackPower = Random.Range(5f, 15f);
            experience = 0;
        }

        public void ResetModel()
        {
            health = Random.Range(80f, 120f);
            attackPower = Random.Range(5f, 15f);
            experience = 0;
        }

        public int Level => 1 + experience / 5;

        public void AddExperience(int value = 1)
        {
            var currentLevel = Level;
            experience++;
            var nextLevel = Level;

            var result = upgradeData;
            if (currentLevel >= nextLevel)
            {
                result.experience = value;
                result.health = 0f;
                result.attackPower = 0f;
                upgradeData = result;
                return;
            }

            var attackPowerUpgrade = attackPower * 0.1f;
            var healthUpgrade = health * 0.1f;
            
            attackPower += attackPowerUpgrade;
            health += healthUpgrade;

            result.experience = value;
            result.attackPower = attackPowerUpgrade;
            result.health = healthUpgrade;

            upgradeData = result;
        }

        public bool Select()
        {
            isSelected = !isSelected;
            return isSelected;
        }
    }
}