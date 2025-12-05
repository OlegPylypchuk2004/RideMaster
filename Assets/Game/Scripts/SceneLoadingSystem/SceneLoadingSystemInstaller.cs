using UnityEngine;
using Zenject;

namespace SceneLoadingSystem
{
    public class SceneLoadingSystemInstaller : MonoInstaller
    {
        [SerializeField, Min(0f)] private float _loadDelay;

        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>()
                .AsSingle()
                .WithArguments(_loadDelay);
        }
    }
}