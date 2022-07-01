using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GameMapHUDController : BaseWindowController<GameMapHUDView>
    {
        public override void Initialize(BaseWindowView view)
        {
            base.Initialize(view);

            _view.OnOpenArmyEvent += OpenArmyClickHandler;
        }

        private void OpenArmyClickHandler()
        {
            CompositionRoot.Container.Resolve<UIManager>().OpenWindow(WindowType.ArmyList);
        }
    }
}
