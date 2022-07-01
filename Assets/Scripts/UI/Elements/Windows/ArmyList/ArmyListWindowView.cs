using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ArmyListWindowView : BaseWindowView<ArmyListWindowData>
    {
        public override WindowType WindowType
        {
            get
            {
                return WindowType.ArmyList;
            }
        }

        [SerializeField]
        private GameObject _squadInfoPrefab;

        [SerializeField]
        private RectTransform _listContainer;

        public event Action<HeroData> OnSquadSelectedEvent;

        public override void UpdateView(ArmyListWindowData data)
        {
            base.UpdateView(data);

            List<HeroData> heroes = new List<HeroData>(data.Army.Heroes);
            heroes.Sort(HeroData.CompareByTotalRating);

            foreach (var hero in heroes)
            {
                GameObject squadInfoObject = GameObject.Instantiate(_squadInfoPrefab, _listContainer);

                squadInfoObject.transform.SetParent(_listContainer);
                squadInfoObject.transform.localPosition = Vector3.zero;
                squadInfoObject.transform.localScale = Vector3.one;
                squadInfoObject.transform.localEulerAngles = Vector3.zero;

                ArmyListWindowItem squadInfo = squadInfoObject.GetComponent<ArmyListWindowItem>();

                squadInfo.OnSelected += OnSquadSelected;

                squadInfo.ApplyData(hero);
            }
        }

        private void OnSquadSelected(HeroData hero)
        {
            if (OnSquadSelectedEvent != null)
                OnSquadSelectedEvent(hero);
        }
    }
}