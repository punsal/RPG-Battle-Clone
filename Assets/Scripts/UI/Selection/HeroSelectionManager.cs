using Hero;
using UnityEngine;

namespace UI.Selection
{
    public class HeroSelectionManager : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private HeroSelectionItem heroSelectionItemPrefab;

        [Header("UI Elements")]
        [SerializeField] private GameObject buttonBattle;
        
        private HeroManager heroManager;
        
        // ReSharper disable once ParameterHidesMember
        public void Construct(HeroManager heroManager)
        {
            this.heroManager = heroManager;

            foreach (var hero in heroManager.Heroes)
            {
                var heroSelectionItem = Instantiate(heroSelectionItemPrefab, transform);
                heroSelectionItem.Construct(hero);
            }
        }

        private void Update()
        {
            if (heroManager?.SelectedHeroes == null) return;
            buttonBattle.SetActive(heroManager.SelectedHeroes.Count >= 3);            
        }
    }
}
