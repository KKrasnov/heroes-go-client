using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ClaimRewardWindowView : BaseWindowView<ClaimRewardWindowData>
    {
        public override WindowType WindowType
        {
            get
            {
                return WindowType.ClaimReward;
            }
        }
    }
}
