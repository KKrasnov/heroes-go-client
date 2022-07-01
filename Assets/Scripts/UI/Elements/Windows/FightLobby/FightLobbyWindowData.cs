using System;

namespace Assets.Scripts.UI
{
    public class FightLobbyWindowData
    {
        public ArmyData AllyArmy;
        public ArmyData EnemyArmy;

        public Action OnFastBattleSelected;
        public Action OnManualBattleSelected;
    }
}