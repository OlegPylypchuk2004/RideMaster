using LevelSystem;
using LocationSystem;
using UnityEngine;
using Zenject;

namespace SessionSystem
{
    public class SessionSystemInstaller : MonoInstaller
    {
        [SerializeField] private LocationConfig _defaultLocationConfig;
        [SerializeField] private LevelConfig _defaultLevelConfig;

        public override void InstallBindings()
        {
            Container.Bind<SessionData>()
                .AsSingle()
                .WithArguments(_defaultLocationConfig, _defaultLevelConfig);
        }
    }
}