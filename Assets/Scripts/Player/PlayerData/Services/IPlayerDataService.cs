using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IPlayerDataService
{
    FractionType GetFraction();

    int GetGoldAmount();
    //int GetCrystalsAmount();

    ArmyData GetArmyData();
    HeroData GetHeroData(Guid id);
    UnitData GetUnitData(Guid instanceId);
    UnitData[] GetUnitsDraft();
}
