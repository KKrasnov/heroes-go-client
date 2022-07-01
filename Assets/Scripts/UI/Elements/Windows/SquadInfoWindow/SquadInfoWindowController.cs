using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class SquadInfoWindowController : BaseWindowController<SquadInfoWindowView, Guid>
    {
        public override void ApplyData(object data)
        {
            base.ApplyData(data);

            SquadInfoWindowData viewData = new SquadInfoWindowData();
            viewData.Hero = ProjectContext.Instance.Container.Resolve<IPlayerDataService>().GetHeroData(_windowData);

            _view.OnUnitSelectedEvent += OnUnitSelectedHandler;
            _view.UpdateView(viewData);
        }

        private void OnUnitSelectedHandler(Guid instanceId)
        {
            UnitInfoWindowControllerData windowData = new UnitInfoWindowControllerData();
            windowData.ID = instanceId;
            windowData.Occupancy = UnitInfoWindowControllerData.UnitOccupancy.Squad;
            windowData.OnViewed = OnUnitInfoViewed;
            ProjectContext.Instance.Container.Resolve<UIManager>().OpenWindow(WindowType.UnitInfo, windowData);
        }

        private void OnUnitInfoViewed(bool needToUpdate)
        {
            if(needToUpdate)
            {
                SquadInfoWindowData viewData = new SquadInfoWindowData();
                viewData.Hero = ProjectContext.Instance.Container.Resolve<IPlayerDataService>().GetHeroData(_windowData);
                _view.UpdateView(viewData);
            }
        }
    }
}