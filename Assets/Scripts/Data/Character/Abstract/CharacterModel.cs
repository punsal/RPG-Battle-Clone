using UnityEngine;

namespace Data.Character.Abstract
{
    public abstract class CharacterModel : ScriptableObject
    {
        public new string name;
        [Header("Character Attributes")]
        [SerializeField] protected float health;
        [SerializeField] protected float attackPower;

        [Header("Visuals")]
        [SerializeField] protected Sprite sprite;

        public float Health => health;
        public float AttackPower => attackPower;
        
        public Sprite Sprite => sprite;
    }
}