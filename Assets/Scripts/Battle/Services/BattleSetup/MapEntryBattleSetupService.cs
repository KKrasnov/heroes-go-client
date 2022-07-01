using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI;
using Zenject;

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
        _fastBattleService = ProjectContext.Instance.Container.Resolve<IFastBattleService>();
        _manualBattleService = ProjectContext.Instance.Container.Resolve<IManualBattleService>();
    }

    public void SetupBattle(GameMapEntryData entry)
    {
        _cachedEntry = entry;
        _cachedPlayerArmy = ProjectContext.Instance.Container.Resolve<IPlayerDataService>().GetArmyData();
        ProjectContext.Instance.Container.Resolve<IUIManager>().OpenWindow(WindowType.FightLobby, new FightLobbyWindowData()
        {
            AllyArmy = _cachedPlayerArmy,
            EnemyArmy = entry.Garrison,
            OnFastBattleSelected = OnFastBattleSelected,
            OnManualBattleSelected = OnManualBattleSelected
        });
    }

    private void OnFastBattleSelected()
    {
        ProjectContext.Instance.Container.Resolve<IGameMapService>().RegisterEntryBattle(_cachedEntry.EntryId);
        _fastBattleService.StartBattle(new BattleSetupData(_cachedPlayerArmy, _cachedEntry.Garrison), OnBattleEnded);
    }

    private void OnManualBattleSelected()
    {
        ProjectContext.Instance.Container.Resolve<IGameMapService>().RegisterEntryBattle(_cachedEntry.EntryId);
        _fastBattleService.StartBattle(new BattleSetupData(_cachedPlayerArmy, _cachedEntry.Garrison), OnBattleEnded);
    }

    private void OnBattleEnded(BattleResultData result)
    {
        _cachedResult = result;
        _cachedReward = ProjectContext.Instance.Container.Resolve<IGameMapService>().CommitEntryBattleResult(_cachedEntry.EntryId, _cachedResult);
        ProjectContext.Instance.Container.Resolve<IUIManager>().OpenWindow(WindowType.BattleResult, new BattleResultWindowData(_cachedResult, OnBattleResultViewed));
        Debug.Log(result.Ally.SurvivedArmy.ArmyForceRating);
    }

    private void OnBattleResultViewed()
    {
        ProjectContext.Instance.Container.Resolve<IUIManager>().OpenWindow(WindowType.ClaimReward, new ClaimRewardWindowData(_cachedReward, OnRewardClaimed));
    }

    private void OnRewardClaimed()
    {
        ProjectContext.Instance.Container.Resolve<IUIManager>().CloseWindow(WindowType.FightLobby);
        ProjectContext.Instance.Container.Resolve<IUIManager>().CloseWindow(WindowType.GameMapEntryDialog);
    }
}
