#define MOCK
using UnityDI;
using Assets.Scripts.UI;
using UnityEngine;

public static class CompositionRoot 
{
    public static Container Container
    {
        get
        {
            if (_container == null)
                _container = InitContainer();
            return _container;
        }
    }
    private static Container _container;

    private static Container InitContainer()
    {
        UnityDI.Container container = new Container();

        container.RegisterType<IMapEntryBattleSetupService, MapEntryBattleSetupService>();
        container.RegisterType<IFastBattleService, AutomaticBattleService>();
        container.RegisterType<IManualBattleService, ManualBattleService>();

        container.RegisterInstance<UIManager>(new UIManager());
        container.RegisterInstance<IGameMapManager>(GameObject.FindObjectOfType<GameMapManager>());

        IPlayerDataService playerDataService;
        IGameMapService gameMapService;

#if MOCK
        playerDataService = new MockPlayerDataService();
        gameMapService = new MockGameMapService(playerDataService);
        container.RegisterInstance<IUnitsConfigurationService>(new MockUnitsConfigurationService());
        container.RegisterInstance<IPlayerLocationService>(new PlayerLocationService());
        container.RegisterInstance<IGameMapEntriesViewHelper>(GameObject.FindObjectOfType<GameMapEntriesViewHelper>());
        container.RegisterInstance<ILocalizationService>(new MockLocalizationService());
        container.RegisterInstance<IDialogService>(new MockDialogService());
#else
        //nothing to say yet
#endif

        container.RegisterInstance<IPlayerDataService>(playerDataService);
        container.RegisterInstance<IGameMapService>(gameMapService);

        return container;
    }
}
