using Hero;
using IoC_Container.Component.Abstract;
using State;
using UnityEngine;

namespace IoC_Container.Component
{
    public class SceneInjector : Injector
    {
        [SerializeField] private GameManager gameManager;
    
        public override void OnInject()
        {
            //Cache GameStateModel
            Container.Bind<GameStateModel>(new GameStateModel());
            
            //Cache GameManager
            Container.Bind<GameManager>(gameManager);
            
            //Cache HeroManager
            Container.Bind<HeroManager>(new HeroManager());
        }
    }
}