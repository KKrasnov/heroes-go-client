using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UnitsListWindowView : BaseWindowView<UnitsListWindowData>
    {
        public override WindowType WindowType
        {
            get
            {
                return WindowType.UnitsList;
            }
        }

        [SerializeField]
        private UnitsListPreviewElement _unitsList;

        public event Action<Guid> OnUnitSelectedEvent;

        public override void UpdateView(UnitsListWindowData data)
        {
            base.UpdateView(data);
            _unitsList.Apply(data.Units, OnUnitSelected);
        }

        private void OnUnitSelected(Guid instanceId)
        {
            if (OnUnitSelectedEvent != null)
                OnUnitSelectedEvent(instanceId);
        }
    }
}