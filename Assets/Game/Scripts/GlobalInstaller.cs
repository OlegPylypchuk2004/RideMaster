using VehicleSystem.Building;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PartsGridData>()
            .AsSingle();
    }
}