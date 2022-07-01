using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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
            IUIManager uiManager = ProjectContext.Instance.Container.Resolve<IUIManager>();
            BaseWindowView[] activeWindows = FindObjectsOfType<BaseWindowView>();
            foreach (BaseWindowView view in activeWindows)
            {
                uiManager.RegisterActiveWindow(view);
            }
        }
    }
}