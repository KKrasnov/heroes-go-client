using Assets.Scripts.UI;
using Zenject;

/// <summary>
/// ProjectContext scope
/// </summary>
public class ServicesInstaller : Installer<ServicesInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IMapEntryBattleSetupService>().To<MapEntryBattleSetupService>().AsSingle();
        Container.Bind<IFastBattleService>().To<AutomaticBattleService>().AsSingle();
        Container.Bind<IManualBattleService>().To<ManualBattleService>().AsSingle();
        Container.Bind<UIManager>().ToSelf().AsSingle();
        Container.Bind<IPlayerDataService>().To<MockPlayerDataService>().AsSingle();
        Container.Bind<IGameMapService>().To<MockGameMapService>().AsSingle();
        Container.Bind<IUnitsConfigurationService>().To<MockUnitsConfigurationService>().AsSingle();
        Container.Bind<IPlayerLocationService>().To<PlayerLocationService>().AsSingle();
        Container.Bind<ILocalizationService>().To<MockLocalizationService>().AsSingle();
        Container.Bind<IDialogService>().To<MockDialogService>().AsSingle();
    }
}