using UnityEngine;

namespace IoC_Container.Component.Abstract
{
    public abstract class Injector : MonoBehaviour
    {
        private void Awake()
        {
            OnInject();
        }

        public abstract void OnInject();
    }
}