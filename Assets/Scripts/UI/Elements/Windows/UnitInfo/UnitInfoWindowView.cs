using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UnitInfoWindowView : BaseWindowView<UnitInfoWindowViewData>
    {
        [SerializeField]
        private Button _moveToDraftBtn;

        [SerializeField]
        private Button _dismissBtn;
        
        public override WindowType WindowType
        {
            get
            {
                return WindowType.UnitInfo;
            }
        }

        public event Action OnUnitMovedToDraftEvent;
        public event Action OnUnitDismissedEvent;

        public override void UpdateView(UnitInfoWindowViewData data)
        {
            base.UpdateView(data);

            switch(data.UnitOccupancy)
            {
                case UnitInfoWindowControllerData.UnitOccupancy.Draft:
                    _dismissBtn.gameObject.SetActive(true);
                    break;
                case UnitInfoWindowControllerData.UnitOccupancy.Squad:
                    _moveToDraftBtn.gameObject.SetActive(true);
                    break;
                case UnitInfoWindowControllerData.UnitOccupancy.None:
                default:
                    break;
            }
        }
        
        public void Dismiss()
        {
            if (OnUnitDismissedEvent != null)
                OnUnitDismissedEvent();
        }

        public void MoveToDraft()
        {
            if (OnUnitMovedToDraftEvent != null)
                OnUnitMovedToDraftEvent();
        }
    }
}