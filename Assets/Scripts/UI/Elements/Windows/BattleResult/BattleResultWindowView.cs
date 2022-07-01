using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class BattleResultWindowView : BaseWindowView<BattleResultWindowData>
    {
        public override WindowType WindowType
        {
            get
            {
                return WindowType.BattleResult;
            }
        }
    }
}