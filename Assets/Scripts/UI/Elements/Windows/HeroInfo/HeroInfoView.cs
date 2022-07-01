using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class HeroInfoView : BaseWindowView
    {
        public override WindowType WindowType
        {
            get
            {
                return WindowType.HeroInfo;
            }
        }

        public void ApplyView(HeroData heroData)
        {

        }
    }
}