using TMPro;
using UnityEngine;

namespace UI.Popup_UI.Fading
{
    public class FadingPopupUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textValue;

        private void OnValidate()
        {
            if (textValue == null)
            {
                textValue = GetComponentInChildren<TextMeshProUGUI>();
            }
        }

        public void Construct(float value, Vector3 position)
        {
            textValue.color = value < 0f ? Color.red : Color.green;
            textValue.text = value.ToString("F1");
            
            transform.position = position;
        }

        public void Construct(string text, Vector3 position, bool isNegative = false)
        {
            textValue.color = isNegative ? Color.green : Color.red;
            textValue.text = text;
            
            transform.position = position;
        }

        public void Complete()
        {
            gameObject.SetActive(false);
        }
    }
}
