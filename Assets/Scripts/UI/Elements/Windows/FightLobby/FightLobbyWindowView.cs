using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class FightLobbyWindowView : BaseWindowView<FightLobbyWindowData>
    {
        public override WindowType WindowType
        {
            get
            {
                return WindowType.FightLobby;
            }
        }

        [SerializeField]
        private Text _allyForceRatingLbl;

        [SerializeField]
        private Text _enemyForceRatingLbl;

        public event Action OnAutoBattleSelected;
        public event Action OnBattleSelected;

        public override void UpdateView(FightLobbyWindowData data)
        {
            base.UpdateView(data);
            _allyForceRatingLbl.text = data.AllyArmy.ArmyForceRating.ToString();
            _enemyForceRatingLbl.text = data.EnemyArmy.ArmyForceRating.ToString();
        }

        public void OnAutoBattleBtnClicked()
        {
            if(OnAutoBattleSelected != null)
            {
                OnAutoBattleSelected();
            }
        }

        public void OnBattleBtnClicked()
        {
            if (OnBattleSelected != null)
                OnBattleSelected();
        }
    }
}