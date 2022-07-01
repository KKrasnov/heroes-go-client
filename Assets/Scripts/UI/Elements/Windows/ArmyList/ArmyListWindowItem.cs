using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ArmyListWindowItem : BaseUIElement
    {
        private const int UNITS_PREVIEW_LIMIT = 5;

        [SerializeField]
        private HeroPreviewElement _heroPreview;

        [SerializeField]
        private GameObject _unitPreviewPrefab;

        [SerializeField]
        private Transform _listContainer;

        [SerializeField]
        private Text _squadRatingLbl;

        [SerializeField]
        private Text _squadCountLbl;

        public event Action<HeroData> OnSelected;

        private HeroData _cachedHero;

        public void ApplyData(HeroData hero)
        {
            _cachedHero = hero;

            _heroPreview.Apply(_cachedHero);

            _squadRatingLbl.text = hero.TotalForceRating.ToString();

            _squadCountLbl.text = string.Format("{0}/{1}", hero.Squad.Length, hero.SquadLimit);

            List<UnitData> units = new List<UnitData>(_cachedHero.Squad);
            units.Sort(UnitData.CompareByRating);

            for(int i = 0; i < UNITS_PREVIEW_LIMIT && i < units.Count; i++)
            {
                GameObject unitPreviewObject = GameObject.Instantiate(_unitPreviewPrefab);

                unitPreviewObject.transform.SetParent(_listContainer);
                unitPreviewObject.transform.localPosition = Vector3.zero;
                unitPreviewObject.transform.localScale = Vector3.one;
                unitPreviewObject.transform.localRotation = Quaternion.identity;

                UnitPreviewElement unitPreview = unitPreviewObject.GetComponent<UnitPreviewElement>();
                unitPreview.Apply(units[i]);
            }
        }

        public void Select()
        {
            if (OnSelected != null)
                OnSelected(_cachedHero);
        }
    }
}