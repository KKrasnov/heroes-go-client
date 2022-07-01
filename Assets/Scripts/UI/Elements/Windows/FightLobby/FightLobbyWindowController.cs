using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class FightLobbyWindowController : BaseWindowController<FightLobbyWindowView, FightLobbyWindowData>
    {
        public override void Initialize(BaseWindowView view)
        {
            base.Initialize(view);
            _view.OnAutoBattleSelected += StartAutoBattle;
            _view.OnBattleSelected += StartBattle;
        }

        private void StartBattle()
        {
            _windowData.OnManualBattleSelected();
        }

        private void StartAutoBattle()
        {
            _windowData.OnFastBattleSelected();
        }

        public override void ApplyData(object data)
        {
            base.ApplyData(data);
            _view.UpdateView(_windowData);
        }
    }
}