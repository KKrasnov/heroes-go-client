using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public abstract class BaseTabWindowController<TView> : BaseWindowController<TView> where TView : BaseTabWindowView
    {
        public override void Initialize(BaseWindowView view)
        {
            base.Initialize(view);
            _view.OnTabOpened += OnTabOpenedHandler;
        }

        protected abstract void OnTabOpenedHandler();
    }

    public abstract class BaseTabWindowController : BaseTabWindowController<BaseTabWindowView>
    {
    }
}