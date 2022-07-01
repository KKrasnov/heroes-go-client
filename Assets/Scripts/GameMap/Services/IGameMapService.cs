using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameMapService
{
    List<GameMapEntryData> GetNearbyStaticEntries(Vector2 playerGeoPos);

    List<GameMapEntryData> GetNearbyDynamicEntries(Vector2 playerGeoPos);

    GameMapEntryData GetEntryData(Guid entryId);


    /// <summary>
    /// Registring battle on entry by current user, if somebody already battles on entry, return false.
    /// </summary>
    /// <param name="entryId"></param>
    /// <returns></returns>
    bool RegisterEntryBattle(Guid entryId);

    /// <summary>
    /// Method, which calls on battle ending, need to claim reward and send to backend result info.
    /// </summary>
    /// <param name="entryId"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    RewardData CommitEntryBattleResult(Guid entryId, BattleResultData result);
}
