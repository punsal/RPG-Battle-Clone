using Character.Input_Handling.Abstract;
using Data.Character;
using UI.Popup_UI.Information;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Hero_Item_UI.Controller
{
    public class HeroItemUIController : InputController, IPointerDownHandler, IPointerUpHandler
    {
#pragma warning disable 649
        
        [Header("View")]
        [SerializeField] private Image imageBackground;
        [SerializeField] private Image imageIcon;

        [Header("Pop Up")]
        [SerializeField] private InfoPopupUIController infoPopupUIControllerPrefab;
        
#pragma warning restore 649
        
        private HeroModel heroModel;

        public bool IsSelected => heroModel.IsSelected;

        public void Construct(HeroModel model)
        {
            heroModel = model;
            gameObject.name = heroModel.name;
            
            imageBackground.color = heroModel.IsSelected ? Color.green : Color.black;
            imageIcon.sprite = heroModel.Sprite;

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            StartInput();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            EndInput();
        }

        protected override void OnClick()
        {
            imageBackground.color = heroModel.Select() ? Color.green : Color.black;
        }

        protected override void OnHold()
        {
            var infoPopup = Instantiate(infoPopupUIControllerPrefab);
            infoPopup.Construct(heroModel);
        }
    }
}
