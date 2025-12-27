using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace VehicleSystem
{
    public class VehicleSystemInstaller : MonoInstaller
    {
        [SerializeField] private Vehicle _vehiclePrefab;
        [SerializeField, MinValue(0f)] private float _minVehicleLinearVelocity;

        public override void InstallBindings()
        {
            Container.Bind<Vehicle>()
                .FromComponentInNewPrefab(_vehiclePrefab)
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<VehicleAssembler>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<VehicleStopChecker>()
                .AsSingle()
                .WithArguments(_minVehicleLinearVelocity);
        }
    }
}