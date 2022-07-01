using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class UnitInfoWindowController : BaseWindowController<UnitInfoWindowView, UnitInfoWindowControllerData>
    {
        public override void ApplyData(object data)
        {
            base.ApplyData(data);

            UnitConfiguration unitConfig;

            IUnitsConfigurationService configService = ProjectContext.Instance.Container.Resolve<IUnitsConfigurationService>();
            
            switch (_windowData.Occupancy)
            {
                case UnitInfoWindowControllerData.UnitOccupancy.Draft:
                case UnitInfoWindowControllerData.UnitOccupancy.Squad:
                    unitConfig = configService.GetUnitConfiguration(ProjectContext.Instance.Container.Resolve<IPlayerDataService>().GetUnitData(_windowData.ID).ID);
                    break;
                case UnitInfoWindowControllerData.UnitOccupancy.None:
                default:
                    unitConfig = configService.GetUnitConfiguration(_windowData.ID);
                    break;
            }

            UnitInfoWindowViewData viewData = new UnitInfoWindowViewData();
            viewData.UnitConfig = unitConfig;
            viewData.UnitOccupancy = _windowData.Occupancy;

            _view.OnUnitMovedToDraftEvent += OnUnitMovedToDraftHandler;
            _view.OnUnitDismissedEvent += OnUnitDismissedHandler;
            _view.UpdateView(viewData);
        }

        private void OnUnitMovedToDraftHandler()
        {
            ProjectContext.Instance.Container.Resolve<UIManager>().CloseWindow(this);
            if (_windowData.OnViewed != null)
                _windowData.OnViewed(true);
        }

        private void OnUnitDismissedHandler()
        {
            ProjectContext.Instance.Container.Resolve<UIManager>().CloseWindow(this);
            if (_windowData.OnViewed != null)
                _windowData.OnViewed(true);
        }

        protected override void OnCloseWindowClickHandler()
        {
            base.OnCloseWindowClickHandler();
            if(_windowData.OnViewed != null)
                _windowData.OnViewed(false);
        }
    }
}