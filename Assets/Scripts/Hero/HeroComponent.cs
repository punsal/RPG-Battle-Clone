using UnityEngine;

namespace Hero
{
    public class HeroComponent : MonoBehaviour
    {
        [SerializeField] private float holdDuration = 3f;
        private float timer;
        private bool isPressed;
        
        private Hero hero;

        // ReSharper disable once ParameterHidesMember
        public void Construct(Hero hero)
        {
            this.hero = hero;
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
                hero.Attack();
            }
            else
            {
                Debug.Log($"Show Attributes of {hero.HeroAttribute.Name}");
            }
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}