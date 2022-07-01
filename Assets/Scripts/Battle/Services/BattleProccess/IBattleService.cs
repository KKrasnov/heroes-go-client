using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleService
{
    void StartBattle(BattleSetupData battleSetup, Action<BattleResultData> onBattleEnded);
}
