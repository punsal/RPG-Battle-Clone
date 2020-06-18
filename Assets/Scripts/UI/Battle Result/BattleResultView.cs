using IoC_Container;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle_Result
{
    public class BattleResultView : MonoBehaviour
    {
        [SerializeField] private Text textResult;
        
        private GameManager gameManager;

        private void Awake()
        {
            gameManager = Container.Get<GameManager>();
        }

        private void OnEnable()
        {
            if (gameManager == null)
            {
                gameManager = Container.Get<GameManager>();
            }

            textResult.text = gameManager.whoWon;
        }
    }
}