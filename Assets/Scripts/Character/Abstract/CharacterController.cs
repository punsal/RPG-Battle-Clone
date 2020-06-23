using Character.Graphics;
using Character.Interface;
using Data.Character.Abstract;
using UI.Health_Bar;
using UI.Popup_UI.Fading;
using UnityEngine;
using Utility.System.Object_Pooler_System;

namespace Character.Abstract
{
    public abstract class CharacterController : MonoBehaviour, ITakeDamage, IAttack
    {
        [Header("Graphics")]
        [SerializeField] protected CharacterGraphicsController characterGraphicsController;

        [Header("UI")]
        [SerializeField] protected HealthBarController healthBarController;
        [SerializeField] protected string fadingPopupUIControllerTag = "FadingPopup";

        protected FadingPopupUIController GetFadingPopup()
        {
            var fadingObject = ObjectPooler.SharedInstance.GetPooledObject(fadingPopupUIControllerTag);
            fadingObject.SetActive(true);
            return fadingObject.GetComponent<FadingPopupUIController>();
        }

        public abstract void Construct(CharacterModel model, bool isLoad = false);
        
        public abstract void TakeDamage(float attackPower);

        public abstract void Attack();
    }
}