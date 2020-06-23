using System.Collections.Generic;
using System.Globalization;
using Data.Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup_UI.Information
{
    public class InfoPopupUIController : MonoBehaviour
    {
        #pragma warning disable 649
        
        [Header("Information Texts")]
        [SerializeField] private TextMeshProUGUI textName;
        [SerializeField] private TextMeshProUGUI textLevel;
        [SerializeField] private TextMeshProUGUI textAttackPower;
        [SerializeField] private TextMeshProUGUI textExperience;

        [Header("Close")]
        [SerializeField] private List<Button> closeButtons;
        
        #pragma warning restore 649
        
        public void Construct(HeroModel heroModel)
        {
            textName.text = heroModel.name;
            textLevel.text = heroModel.Level.ToString();
            textAttackPower.text = Mathf.CeilToInt(heroModel.AttackPower).ToString(CultureInfo.CurrentCulture);
            textExperience.text = heroModel.Experience.ToString();

            foreach (var closeButton in closeButtons)
            {
                closeButton.onClick.AddListener(Close);
            }
        }

        private void Close()
        {
            Destroy(gameObject);
        }
    }
}
