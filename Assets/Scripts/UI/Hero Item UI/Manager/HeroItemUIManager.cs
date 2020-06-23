using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Character;
using UI.Hero_Item_UI.Controller;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Hero_Item_UI.Manager
{
    public class HeroItemUIManager : MonoBehaviour
    {
#pragma warning disable 649
        
        [FormerlySerializedAs("heroItemControllerPrefab")] [SerializeField] private HeroItemUIController heroItemUIControllerPrefab;
        [SerializeField] private GameObject buttonBattle;
        
#pragma warning restore 649

        private List<HeroItemUIController> heroItemControllers;

        public void Construct(IEnumerable<HeroModel> heroModels)
        {
            if (heroItemControllers != null && heroItemControllers.Count > 0)
            {
                while (heroItemControllers.Count > 0)
                {
                    var temp = heroItemControllers[0];
                    heroItemControllers.RemoveAt(0);
                    Destroy(temp.gameObject);
                }
            }
            heroItemControllers = new List<HeroItemUIController>();
            
            foreach (var heroModel in heroModels)
            {
                var tempController = Instantiate(heroItemUIControllerPrefab, transform);
                tempController.Construct(heroModel);
                heroItemControllers.Add(tempController);
            }
        }

        private void Update()
        {
            var count = heroItemControllers
                .Sum(heroItemController => heroItemController.IsSelected ? 1 : 0);
            buttonBattle.SetActive(count >= 3);
        }
    }
}
