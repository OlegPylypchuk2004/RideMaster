using Zenject;

namespace VehicleSystem
{
    public class VehicleSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<VehicleAssembler>()
                .AsSingle()
                .NonLazy();
        }
    }
}