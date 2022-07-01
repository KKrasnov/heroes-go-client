using System;
using System.Collections.Generic;

namespace Assets.Scripts.UI
{
    public class WindowsMap 
    {
        private Dictionary<WindowType, WindowTypeInfo> _windowsMap = new Dictionary<WindowType, WindowTypeInfo>();

        public WindowsMap()
        {
            FillMap();
        }

        private void FillMap()
        {
            _windowsMap.Add(WindowType.GameMapHUD, new WindowTypeInfo("Prefabs/UI/Windows/GameMapHUD/GameMapHUD" , typeof(GameMapHUDController)));
            _windowsMap.Add(WindowType.HeroesList, new WindowTypeInfo("Prefabs/UI/Windows/HeroesList/HeroesList", typeof(HeroesListWindowController)));
            _windowsMap.Add(WindowType.ArmyList, new WindowTypeInfo("Prefabs/UI/Windows/ArmyList/ArmyList", typeof(ArmyListWindowController)));
            _windowsMap.Add(WindowType.UnitsList, new WindowTypeInfo("Prefab/UI/Windows/UnitsList/UnitsList", typeof(UnitsListWindowController)));
            _windowsMap.Add(WindowType.HeroInfo, new WindowTypeInfo("Prefabs/UI/Windows/HeroInfo/HeroInfo", typeof(HeroInfoController)));
            _windowsMap.Add(WindowType.UnitInfo, new WindowTypeInfo("Prefabs/UI/Windows/UnitInfo/UnitInfo", typeof(UnitInfoWindowController)));
            _windowsMap.Add(WindowType.SquadInfo, new WindowTypeInfo("Prefabs/UI/Windows/SquadInfo/SquadInfo", typeof(SquadInfoWindowController)));
            _windowsMap.Add(WindowType.GameMapEntryDialog, new WindowTypeInfo("Prefabs/UI/Windows/GameMapEntryDialog/GameMapEntryDialog", typeof(GameMapEntryDialogController)));
            _windowsMap.Add(WindowType.FightLobby, new WindowTypeInfo("Prefabs/UI/Windows/FightLobby/FightLobby", typeof(FightLobbyWindowController)));
            _windowsMap.Add(WindowType.BattleResult, new WindowTypeInfo("Prefabs/UI/Windows/BattleResult/BattleResult", typeof(BattleResultWindowController)));
            _windowsMap.Add(WindowType.ClaimReward, new WindowTypeInfo("Prefabs/UI/Windows/ClaimReward/ClaimReward", typeof(ClaimRewardWindowController)));
        }

        public WindowTypeInfo GetWindowInfo(WindowType windowType)
        {
            return _windowsMap[windowType];
        }
    }

    public class WindowTypeInfo
    {
        public string ResourcePath;
        public Type ControllerType;

        public WindowTypeInfo(string resourcePath, Type controllerType)
        {
            ResourcePath = resourcePath;
            ControllerType = controllerType;
        }
    }
}