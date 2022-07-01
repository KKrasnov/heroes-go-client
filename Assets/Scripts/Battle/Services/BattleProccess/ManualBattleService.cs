using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualBattleService : IManualBattleService {
    
    public void StartBattle(BattleSetupData battleSetup, Action<BattleResultData> onBattleEnded)
    {
        throw new NotImplementedException();
    }
}
