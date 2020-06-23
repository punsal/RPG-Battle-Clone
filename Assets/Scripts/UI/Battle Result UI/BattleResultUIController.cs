using Data.Game;
using TMPro;
using UnityEngine;

namespace UI.Battle_Result_UI
{
    public class BattleResultUIController : MonoBehaviour
    {
#pragma warning disable 649
        
        [SerializeField] private TextMeshProUGUI textResult;
        [SerializeField] private GameData gameData;
        
#pragma warning restore 649
        
        private void OnEnable()
        {
            textResult.text = gameData.battleResult;
        }
    }
}
