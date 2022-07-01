using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public abstract class BaseWindowView : BaseUIElement
    {
        public abstract WindowType WindowType
        {
            get;
        }

        protected UnityEvent _CloseWindowEvent = new UnityEvent();
        
        public event UnityAction OnCloseWindow
        {
            add
            {
                _CloseWindowEvent.AddListener(value);
            }
            remove
            {
                _CloseWindowEvent.RemoveListener(value);
            }
        }

        protected override void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
        }

        public virtual void Close()
        {
            OnCloseButtonClick();
        }

        protected override void OnDestroy()
        {
            _CloseWindowEvent.RemoveAllListeners();
        }

        protected virtual void OnCloseButtonClick()
        {
            _CloseWindowEvent.Invoke();
        }
    }

    public abstract class BaseWindowView<TData> : BaseWindowView
    {
        public virtual void UpdateView(TData data)
        {

        }
    }
}