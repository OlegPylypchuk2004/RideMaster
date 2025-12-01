using UnityEngine;
using Zenject;

namespace WalletSystem
{
    public class WalletSystemInstaller : MonoInstaller
    {
        [SerializeField] private CurrencyConfig[] _currencyConfigs;

        public override void InstallBindings()
        {
            Container.Bind<CurrencyWallet>()
                .AsSingle()
                .WithArguments(_currencyConfigs);
        }
    }
}