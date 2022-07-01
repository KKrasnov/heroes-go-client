using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ClaimRewardWindowController : BaseWindowController<ClaimRewardWindowView, ClaimRewardWindowData>
    {
        public override void Dispose()
        {
            base.Dispose();
            if (_windowData.OnViewed != null)
                _windowData.OnViewed();
        }
    }
}