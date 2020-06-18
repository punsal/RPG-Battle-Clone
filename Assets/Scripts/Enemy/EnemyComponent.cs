using UnityEngine;

namespace Hero
{
    public class EnemyComponent : MonoBehaviour
    {
        [SerializeField] private float holdDuration = 3f;
        private float timer;
        private bool isPressed;
        
        private Enemy enemy;
        
        // ReSharper disable once ParameterHidesMember
        public void Construct(Enemy enemy)
        {
            this.enemy = enemy;
        }
        
        private void Update()
        {
            if (isPressed)
            {
                timer += Time.deltaTime;
            }    
        }

        private void OnMouseDown()
        {
            timer = 0f;
            isPressed = true;
        }

        private void OnMouseUp()
        {
            isPressed = false;
            if (timer < holdDuration)
            {
                enemy.Attack();
            }
            else
            {
                Debug.Log($"Show Attributes of {enemy.EnemyAttribute.Name}");
            }
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}