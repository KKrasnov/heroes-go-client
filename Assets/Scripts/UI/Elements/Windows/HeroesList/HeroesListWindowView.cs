using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class HeroesListWindowView : BaseWindowView
    {
        public override WindowType WindowType
        {
            get
            {
                return WindowType.HeroesList;
            }
        }

        [SerializeField]
        private GameObject _heroInfoPrefab;

        [SerializeField]
        private RectTransform _listContainer;

        public event Action<HeroData> OnHeroSelectedEvent;

        public void ApplyView(ArmyData army)
        {
            foreach(var hero in army.Heroes)
            {
                GameObject heroInfoObject = GameObject.Instantiate(_heroInfoPrefab, _listContainer);

                heroInfoObject.transform.SetParent(_listContainer);
                heroInfoObject.transform.localPosition = Vector3.zero;
                heroInfoObject.transform.localScale = Vector3.one;
                heroInfoObject.transform.localEulerAngles = Vector3.zero;

                HeroesListWindowItem heroInfo = heroInfoObject.GetComponent<HeroesListWindowItem>();

                heroInfo.OnSelected += OnHeroSelected;

                heroInfo.ApplyModel(hero);
            }
        }

        private void OnHeroSelected(HeroData hero)
        {
            if (OnHeroSelectedEvent != null)
                OnHeroSelectedEvent(hero);
        }
    }
}