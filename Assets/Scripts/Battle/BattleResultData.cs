using System.Collections;
using System.Collections.Generic;

public class BattleResultData
{
    public bool IsVictory;
    public ResultArmyData Ally;
    public ResultArmyData Enemy;
}

public class ResultArmyData
{
    public ArmyData SurvivedArmy;
    public List<UnitData> DeadUnits = new List<UnitData>();
}
