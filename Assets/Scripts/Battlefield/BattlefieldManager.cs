using System.Collections.Generic;
using Hero;
using UnityEngine;

namespace Battlefield
{
    public class BattlefieldManager : MonoBehaviour
    {
        [SerializeField] private Transform heroPositionsTransform;
        private List<Vector3> heroPositions;

        [SerializeField] private Transform enemyPositionTransform;
        private Vector3 enemyPosition;

        private void Awake()
        {
            heroPositions = new List<Vector3>();
            
            var heroPositionTransforms = heroPositionsTransform.GetComponentsInChildren<Transform>();
            for (var i = 1; i < heroPositionTransforms.Length; i++)
            {
                var tempHeroPositionTransform = heroPositionTransforms[i];
                heroPositions.Add(tempHeroPositionTransform.position);
            }

            enemyPosition = enemyPositionTransform.position;
        }

        public void PutHeroesInBattlefield(List<Hero.Hero> heroes)
        {
            for (var i = 0; i < heroes.Count; i++)
            {
                var hero = heroes[i];
                hero.Move(heroPositions[i]);
            }
        }

        public void PutEnemyInBattlefield(Enemy enemy)
        {
            enemy.Move(enemyPosition);
        }
    }
}
