using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UnitsListWindowController : BaseWindowController<UnitsListWindowView>
    {
        public override void Initialize(BaseWindowView view)
        {
            base.Initialize(view);

            UnitsListWindowData data = new UnitsListWindowData();
            data.Units = new List<UnitData>(CompositionRoot.Container.Resolve<IPlayerDataService>().GetUnitsDraft());

            _view.OnUnitSelectedEvent += OnUnitSelectedHandler;
            _view.UpdateView(data);
        }
        
        private void OnUnitSelectedHandler(Guid instanceId)
        {
            UnitInfoWindowControllerData windowData = new UnitInfoWindowControllerData();
            windowData.ID = instanceId;
            windowData.Occupancy = UnitInfoWindowControllerData.UnitOccupancy.Draft;
            CompositionRoot.Container.Resolve<UIManager>().OpenWindow(WindowType.UnitInfo, windowData);
        }
    }
}