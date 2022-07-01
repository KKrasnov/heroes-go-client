using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utils;

namespace Assets.Scripts.UI
{
    public class UIManager
    {
        private WindowsMap _windowsMap = new WindowsMap();
        private List<ValuePair<WindowType, IWindowController>> _activeWindows = new List<ValuePair<WindowType, IWindowController>>();

        private Canvas _UICanvas;

        public Canvas UICanvas
        {
            get { return _UICanvas; }
        }

        private const string UI_CANVAS_TAG = "UICanvas";

        public void OpenWindow(WindowType windowType)
        {
            OpenWindow(windowType, Vector3.zero, Vector3.zero);
        }

        public void OpenWindow(WindowType windowType, object data)
        {
            OpenWindow(windowType, Vector3.zero, Vector3.zero, data: data);
        }

        public void OpenWindow(WindowType windowType, Vector3 position)
        {
            OpenWindow(windowType, position, Vector3.zero);
        }

        public void OpenWindow(WindowType windowType, Vector3 position, object data)
        {
            OpenWindow(windowType, position, Vector3.zero, data: data);
        }

        public void OpenWindow(WindowType windowType, Vector3 position, Vector3 eulerAngles, bool UseDefaultCanvas = true, object data = null)
        {
            WindowTypeInfo windowInfo = _windowsMap.GetWindowInfo(windowType);

            GameObject windowPrefab = Resources.Load<GameObject>(windowInfo.ResourcePath);
            GameObject windowObject = GameObject.Instantiate(windowPrefab);
            windowObject.name = windowPrefab.name;
            windowObject.transform.position = position;
            windowObject.transform.eulerAngles = eulerAngles;
            BaseWindowView windowView = windowObject.GetComponent<BaseWindowView>();

            IWindowController controller = InitializeController(windowInfo, windowView, data);

            _activeWindows.Add(new ValuePair<WindowType, IWindowController>(windowType, controller));

            if (!_UICanvas || !_UICanvas.gameObject)
            {
                _UICanvas = null;
                GameObject canvasObject = GameObject.FindWithTag(UI_CANVAS_TAG);

                if (canvasObject)
                    _UICanvas = canvasObject.GetComponent<Canvas>();
            }

            if (_UICanvas && UseDefaultCanvas)
            {
                windowObject.GetComponent<RectTransform>().transform.SetParent(_UICanvas.transform, false);
            }
        }

        public void RegisterActiveWindow(BaseWindowView view)
        {
            if (IsWindowAlreadyRegistered(view)) return;
            WindowTypeInfo windowInfo = _windowsMap.GetWindowInfo(view.WindowType);

            if (windowInfo == null) return;

            IWindowController controller = InitializeController(windowInfo, view);

            _activeWindows.Add(new ValuePair<WindowType, IWindowController>(view.WindowType, controller));
        }

        public void CloseWindow(WindowType window)
        {
            ValuePair<WindowType, IWindowController> pairEntry = _activeWindows.Find(item => item.First == window);

            if (pairEntry == null)
                return;

            IWindowController controller = pairEntry.Second;

            controller.Dispose();
            DestroyView(controller.View);

            _activeWindows.Remove(pairEntry);
        }

        public void CloseWindow(IWindowController controller)
        {
            ValuePair<WindowType, IWindowController> pairEntry = _activeWindows.Find(item => item.Second == controller);

            controller.Dispose();
            DestroyView(controller.View);

            _activeWindows.Remove(pairEntry);
        }

        public void CloseAllWindows()
        {
            for (int i = _activeWindows.Count - 1; i >= 0; i--)
            {
                ValuePair<WindowType, IWindowController> pairEntry = _activeWindows[i];
                IWindowController controller = pairEntry.Second;
                controller.Dispose();
                DestroyView(controller.View);

                _activeWindows.Remove(pairEntry);
            }
        }

        private bool IsWindowAlreadyRegistered(BaseWindowView view)
        {
            foreach (var pairEntry in _activeWindows)
            {
                if (pairEntry.Second.View == view)
                    return true;
            }
            return false;
        }

        private IWindowController InitializeController(WindowTypeInfo windowInfo, BaseWindowView view, object data = null)
        {
            IWindowController controller = (IWindowController)Activator.CreateInstance(windowInfo.ControllerType);
            
            controller.Initialize(view);

            if (data != null)
                controller.ApplyData(data);

            return controller;
        }

        private void DestroyView(BaseWindowView view)
        {
            if (view)
                GameObject.Destroy(view.gameObject);
        }
    }
}