using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public abstract class BaseWindowController<TView> : IWindowController where TView : BaseWindowView
    { 
        public BaseWindowView View
        {
            get
            {
                return _view;
            }
        }

        protected TView _view;

        public virtual void Initialize(BaseWindowView view)
        {
            _view = (TView)view;
            _view.OnCloseWindow += OnCloseWindowClickHandler;
        }

        public virtual void ApplyData(object data)
        {

        }

        public virtual void Dispose()
        {
            if (_view)
                GameObject.Destroy(_view.gameObject);
        }

        protected virtual void OnCloseWindowClickHandler()
        {
            CompositionRoot.Container.Resolve<UIManager>().CloseWindow(this);
        }
    }

    public abstract class BaseWindowController<TView, TData> : BaseWindowController<TView> where TView : BaseWindowView
    {
        protected TData _windowData;

        public override void ApplyData(object data)
        {
            _windowData = (TData)data;
        }
    }

    public abstract class BaseWindowController : BaseWindowController<BaseWindowView>
    {

    }
}