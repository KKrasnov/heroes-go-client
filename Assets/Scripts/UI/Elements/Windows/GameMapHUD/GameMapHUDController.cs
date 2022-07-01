using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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
            ProjectContext.Instance.Container.Resolve<UIManager>().OpenWindow(WindowType.ArmyList);
        }
    }
}
