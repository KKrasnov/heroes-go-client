using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class TabsWindowView : BaseWindowView
    {
        public override WindowType WindowType
        {
            get
            {
                return WindowType.TabsMenu;
            }
        }

        [SerializeField]
        protected TabElement[] _tabs;

        protected override void Start()
        {
            for (int i = 0; i < _tabs.Length; i++)
            {
                TabElement tab = _tabs[i];
                tab.Index = i;
                tab.OnTabSelected += OnTabSelectedHandler;
                tab.IsSelected = i == 0;
            }
        }

        protected virtual void OnTabSelectedHandler(int index)
        {
            for(int i = 0; i < _tabs.Length; i++)
            {
                TabElement tab = _tabs[i];
                tab.IsSelected = index == i;
            }
        }
    }
}