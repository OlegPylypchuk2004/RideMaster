using UI.PopupSystem.Popups;
using UnityEngine;
using Zenject;

namespace MenuScene
{
    public class MenuSceneUIInstaller : MonoInstaller
    {
        [SerializeField] private SettingsPopup _settingsPopupPrefab;

        public override void InstallBindings()
        {
            Container.Bind<SettingsPopup>()
                .FromComponentInNewPrefab(_settingsPopupPrefab)
                .AsSingle();
        }
    }
}