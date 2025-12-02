using UnityEngine;
using Zenject;

namespace VehicleSystem
{
    public class VehicleSystemInstaller : MonoInstaller
    {
        [SerializeField] private Vehicle _vehiclePrefab;

        public override void InstallBindings()
        {
            Container.Bind<Vehicle>()
                .FromComponentInNewPrefab(_vehiclePrefab)
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<VehicleAssembler>()
                .AsSingle()
                .NonLazy();
        }
    }
}