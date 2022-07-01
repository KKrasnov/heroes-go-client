using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UnitsListPreviewElement : BaseUIElement
    {
        [SerializeField]
        private GameObject _unitInfoPrefab;

        [SerializeField]
        private RectTransform _listContainer;

        List<UnitPreviewElement> _unitsInstances = new List<UnitPreviewElement>();

        public void Apply(List<UnitData> units, Action<Guid> OnUnitSelected = null)
        {
            Clear();
            units.Sort(UnitData.CompareByRating);

            foreach (var unit in units)
            {
                GameObject unitInfoObject = GameObject.Instantiate(_unitInfoPrefab, _listContainer);

                unitInfoObject.transform.SetParent(_listContainer);
                unitInfoObject.transform.localPosition = Vector3.zero;
                unitInfoObject.transform.localScale = Vector3.one;
                unitInfoObject.transform.localEulerAngles = Vector3.zero;

                UnitPreviewElement unitPreview = unitInfoObject.GetComponent<UnitPreviewElement>();

                _unitsInstances.Add(unitPreview);

                unitPreview.OnSelected += OnUnitSelected;
                unitPreview.Apply(unit);
            }
        }

        private void Clear()
        {
            for(int i = _unitsInstances.Count - 1; i >= 0; i--)
            {
                Destroy(_unitsInstances[i].gameObject);
            }
            _unitsInstances.Clear();
        }
    }
}