using UnityEngine;

namespace Assets.Scripts.UI
{
    public interface IUIManager
    {
        void OpenWindow(WindowType windowType);
        void OpenWindow(WindowType windowType, object data);
        void OpenWindow(WindowType windowType, Vector3 position);
        void OpenWindow(WindowType windowType, Vector3 position, object data);

        void OpenWindow(WindowType windowType, Vector3 position, Vector3 eulerAngles, bool UseDefaultCanvas = true,
            object data = null);

        void RegisterActiveWindow(BaseWindowView view);
        void CloseWindow(WindowType window);
        void CloseWindow(IWindowController controller);
        void CloseAllWindows();
        

    }
}