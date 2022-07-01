using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIContext : MonoBehaviour
    {
        private void Start()
        {
            RegisterActiveWindows();
        }

        private void RegisterActiveWindows()
        {
            UIManager uiManager = CompositionRoot.Container.Resolve<UIManager>();
            BaseWindowView[] activeWindows = FindObjectsOfType<BaseWindowView>();
            foreach (BaseWindowView view in activeWindows)
            {
                uiManager.RegisterActiveWindow(view);
            }
        }
    }
}