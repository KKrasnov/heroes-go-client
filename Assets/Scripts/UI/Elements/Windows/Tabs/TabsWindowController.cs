using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class TabsWindowController<TView> : BaseWindowController<TView> where TView : TabsWindowView
    {
        public override void Initialize(BaseWindowView view)
        {
            base.Initialize(view);
        }
    }

    public class TabsWindowController : BaseWindowController<TabsWindowView>
    {

    }
}