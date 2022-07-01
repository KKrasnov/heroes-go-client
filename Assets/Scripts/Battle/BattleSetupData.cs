using System;

public class BattleSetupData
{
    public ArmyData AllyArmy;
    public ArmyData EnemyArmy;
    public BattleSetupData()
    {

    }

    public BattleSetupData(ArmyData ally, ArmyData enemy)
    {
        AllyArmy = ally;
        EnemyArmy = enemy;
    }
}
