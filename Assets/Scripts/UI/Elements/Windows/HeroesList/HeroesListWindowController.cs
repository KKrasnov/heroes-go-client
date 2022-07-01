using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class HeroesListWindowController : BaseWindowController<HeroesListWindowView>
    {
        private ArmyData _army;

        public override void Initialize(BaseWindowView view)
        {
            base.Initialize(view);
            _view.OnHeroSelectedEvent += _view_OnHeroSelectedHandler;

            _army = ProjectContext.Instance.Container.Resolve<IPlayerDataService>().GetArmyData();
            _view.ApplyView(_army);
        }

        private void _view_OnHeroSelectedHandler(HeroData obj)
        {
            throw new System.NotImplementedException();
        }
    }
}