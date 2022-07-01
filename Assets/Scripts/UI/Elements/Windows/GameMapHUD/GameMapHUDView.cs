using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GameMapHUDView : BaseWindowView
    {
        public event Action OnOpenArmyEvent;

        public override WindowType WindowType
        {
            get
            {
                return WindowType.GameMapHUD;
            }
        }

        public void OnArmyBtnClick()
        {
            if (OnOpenArmyEvent != null)
                OnOpenArmyEvent();
        }
    }
}