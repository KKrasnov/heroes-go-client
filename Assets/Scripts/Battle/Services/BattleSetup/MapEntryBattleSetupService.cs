using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI;

public class MapEntryBattleSetupService : IMapEntryBattleSetupService
{
    private IFastBattleService _fastBattleService;
    private IManualBattleService _manualBattleService;

    private ArmyData _cachedPlayerArmy;
    private GameMapEntryData _cachedEntry;

    private BattleResultData _cachedResult;
    private RewardData _cachedReward;

    public MapEntryBattleSetupService()
    {
        _fastBattleService = CompositionRoot.Container.Resolve<IFastBattleService>();
        _manualBattleService = CompositionRoot.Container.Resolve<IManualBattleService>();
    }

    public void SetupBattle(GameMapEntryData entry)
    {
        _cachedEntry = entry;
        _cachedPlayerArmy = CompositionRoot.Container.Resolve<IPlayerDataService>().GetArmyData();
        CompositionRoot.Container.Resolve<UIManager>().OpenWindow(WindowType.FightLobby, new FightLobbyWindowData()
        {
            AllyArmy = _cachedPlayerArmy,
            EnemyArmy = entry.Garrison,
            OnFastBattleSelected = OnFastBattleSelected,
            OnManualBattleSelected = OnManualBattleSelected
        });
    }

    private void OnFastBattleSelected()
    {
        CompositionRoot.Container.Resolve<IGameMapService>().RegisterEntryBattle(_cachedEntry.EntryId);
        _fastBattleService.StartBattle(new BattleSetupData(_cachedPlayerArmy, _cachedEntry.Garrison), OnBattleEnded);
    }

    private void OnManualBattleSelected()
    {
        CompositionRoot.Container.Resolve<IGameMapService>().RegisterEntryBattle(_cachedEntry.EntryId);
        _fastBattleService.StartBattle(new BattleSetupData(_cachedPlayerArmy, _cachedEntry.Garrison), OnBattleEnded);
    }

    private void OnBattleEnded(BattleResultData result)
    {
        _cachedResult = result;
        _cachedReward = CompositionRoot.Container.Resolve<IGameMapService>().CommitEntryBattleResult(_cachedEntry.EntryId, _cachedResult);
        CompositionRoot.Container.Resolve<UIManager>().OpenWindow(WindowType.BattleResult, new BattleResultWindowData(_cachedResult, OnBattleResultViewed));
        Debug.Log(result.Ally.SurvivedArmy.ArmyForceRating);
    }

    private void OnBattleResultViewed()
    {
        CompositionRoot.Container.Resolve<UIManager>().OpenWindow(WindowType.ClaimReward, new ClaimRewardWindowData(_cachedReward, OnRewardClaimed));
    }

    private void OnRewardClaimed()
    {
        CompositionRoot.Container.Resolve<UIManager>().CloseWindow(WindowType.FightLobby);
        CompositionRoot.Container.Resolve<UIManager>().CloseWindow(WindowType.GameMapEntryDialog);
    }
}
