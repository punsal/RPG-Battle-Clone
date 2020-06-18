using System.Collections.Generic;
using UI.Selection;

namespace Hero
{
    public class HeroManager
    {
        public List<Hero> Heroes { get; }

        public List<Hero> SelectedHeroes { get; private set; }
        
        public HeroManager()
        {
            Heroes = new List<Hero>();
            for (var i = 0; i < 10; i++)
            {
                Heroes.Add(new Hero());
            }

            SelectedHeroes = new List<Hero>();
            
            HeroSelectionItem.OnSelected += OnSelectedHandler;
        }

        ~HeroManager()
        {
            HeroSelectionItem.OnSelected -= OnSelectedHandler;
        }

        private void OnSelectedHandler(Hero hero)
        {
            if (SelectedHeroes == null) SelectedHeroes = new List<Hero>();

            if (!SelectedHeroes.Remove(hero)) SelectedHeroes.Add(hero);
        }
    }
}