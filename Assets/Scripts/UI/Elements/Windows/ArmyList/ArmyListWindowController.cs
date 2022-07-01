using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ArmyListWindowController : BaseWindowController<ArmyListWindowView>
    {
        private ArmyData _army;

        public override void Initialize(BaseWindowView view)
        {
            base.Initialize(view);

            _view.OnSquadSelectedEvent += OnSquadSelected;

            _army = CompositionRoot.Container.Resolve<IPlayerDataService>().GetArmyData();

            _view.UpdateView(new ArmyListWindowData()
            {
                Army = _army
            });

        }

        private void OnSquadSelected(HeroData hero)
        {
            CompositionRoot.Container.Resolve<UIManager>().OpenWindow(WindowType.SquadInfo, hero.ID);
        }
    }
}