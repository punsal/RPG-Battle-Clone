using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Selection
{
    public class HeroSelectionItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("UI Elements")]
        [SerializeField] private Image panelItem;
        [SerializeField] private Image imageBackground;
        [SerializeField] private Image imageIcon;

        [Header("Behaviour")] 
        [SerializeField] private float holdDuration = 3f;
        
        private Hero.Hero hero;
        private bool isSelected;

        private bool isClicked;
        [SerializeField] private float timer = 0f;

        public delegate void Selected(Hero.Hero hero);
        public static event Selected OnSelected;
        
        // ReSharper disable once ParameterHidesMember
        public void Construct(Hero.Hero hero)
        {
            this.hero = hero;

            panelItem.raycastTarget = hero.IsAvailable;
            imageBackground.enabled = isSelected = false;
            imageIcon.enabled = hero.IsAvailable;
        }

        private void Update()
        {
            if (isClicked)
            {
                timer += Time.deltaTime;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isClicked = true;
            timer = 0f;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isClicked = false;
            if (timer < holdDuration)
            {
                imageBackground.enabled = isSelected = !isSelected;
                OnSelected?.Invoke(hero);
            }
            else
            {
                Debug.Log($"Show Attributes of {hero.HeroAttribute.Name}");
            }
        }
    }
}
