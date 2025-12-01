using LevelSystem;
using UnityEngine;
using Zenject;

namespace SessionSystem
{
    public class SessionSystemInstaller : MonoInstaller
    {
        [SerializeField] private LevelConfig _defaultLevelConfig;

        public override void InstallBindings()
        {
            Container.Bind<SessionData>()
                .AsSingle()
                .WithArguments(_defaultLevelConfig);
        }
    }
}