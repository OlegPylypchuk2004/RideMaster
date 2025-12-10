using UI.PopupSystem.Popups;
using UnityEngine;
using Zenject;

namespace GameplayScene
{
    public class GameplaySceneUIInstaller : MonoInstaller
    {
        [SerializeField] private LevelCompletedPopup _levelCompletedPopupPrefab;

        public override void InstallBindings()
        {
            Container.Bind<LevelCompletedPopup>()
                .FromComponentInNewPrefab(_levelCompletedPopupPrefab)
                .AsSingle();
        }
    }
}