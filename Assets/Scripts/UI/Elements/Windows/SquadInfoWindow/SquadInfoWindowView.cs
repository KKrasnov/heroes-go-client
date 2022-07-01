using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class SquadInfoWindowView : BaseWindowView<SquadInfoWindowData>
    {
        public override WindowType WindowType
        {
            get
            {
                return WindowType.SquadInfo;
            }
        }

        [SerializeField]
        private HeroPreviewElement _heroPreview;

        [SerializeField]
        private UnitsListPreviewElement _unitsList;

        public event Action<Guid> OnUnitSelectedEvent;

        public override void UpdateView(SquadInfoWindowData data)
        {
            base.UpdateView(data);

            _heroPreview.Apply(data.Hero);

            _unitsList.Apply(new List<UnitData>(data.Hero.Squad), OnUnitSelected);
        }

        private void OnUnitSelected(Guid instanceId)
        {
            if (OnUnitSelectedEvent != null)
                OnUnitSelectedEvent(instanceId);
        }
    }
}