using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Utils;
using Zenject;

namespace Assets.Scripts.UI
{
    public class TabElement : BaseUIElement
    {
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                _tabView.gameObject.SetActive(_isSelected);
                _activeSelectionObject.SetActive(_isSelected);
                _inactiveSelectionObject.SetActive(!_isSelected);
                if (_isSelected)
                    _tabView.TabSelected();
            }
        }

        private bool _isSelected = false;

        [SerializeField]
        private GameObject _activeSelectionObject;

        [SerializeField]
        private GameObject _inactiveSelectionObject;

        [SerializeField]
        private Button _selectButton;

        [SerializeField]
        private BaseTabWindowView _tabView;

        [HideInInspector]
        public int Index;

        public event UnityAction<int> OnTabSelected;

        protected override void Awake()
        {
            base.Awake();
            ProjectContext.Instance.Container.Resolve<UIManager>().RegisterActiveWindow(_tabView);
            _selectButton.onClick.AddListener(OnSelectButtonClick);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void OnSelectButtonClick()
        {
            if(OnTabSelected != null)
                OnTabSelected(Index);
        }
    }
}