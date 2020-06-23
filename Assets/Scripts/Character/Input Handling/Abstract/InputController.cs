using UnityEngine;

namespace Character.Input_Handling.Abstract
{
    public abstract class InputController : MonoBehaviour
    {
        [Header("Input Handing")]
        [SerializeField] private float holdDuration = 3f;
        private float timer;
        private bool isSelected;

        private void Update()
        {
            HandleInput();
        }

        protected void StartInput()
        {
            timer = 0f;
            isSelected = true;
        }

        private void HandleInput()
        {
            if (isSelected)
            {
                timer += Time.deltaTime;
            }
        }

        protected void EndInput()
        {
            isSelected = false;
            if (timer < holdDuration)
            {
                OnClick();
            }
            else
            {
                OnHold();
            }
        }

        protected abstract void OnClick();
        protected abstract void OnHold();
    }
}