using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.UI
{
    public abstract class BaseTabWindowView : BaseWindowView
    {
        protected UnityEvent _TabOpenedEvent = new UnityEvent();

        public event UnityAction OnTabOpened
        {
            add
            {
                _TabOpenedEvent.AddListener(value);
            }
            remove
            {
                _TabOpenedEvent.RemoveListener(value);
            }
        }

        public void TabSelected()
        {
            _TabOpenedEvent.Invoke();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _TabOpenedEvent.RemoveAllListeners();
        }
    }
}
