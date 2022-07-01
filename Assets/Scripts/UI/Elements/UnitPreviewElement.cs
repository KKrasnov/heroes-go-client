using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UnitPreviewElement : BaseUIElement
    {
        [SerializeField]
        private Image _previewImg;

        [SerializeField]
        private Text _previewText;

        [SerializeField]
        private Text _rarityLbl;

        public event Action<Guid> OnSelected;

        private UnitData _cachedUnit;

        public void Apply(UnitData unit)
        {
            _previewImg.sprite = Resources.Load<Sprite>(unit.PreviewSpritePath);
            _previewText.text = unit.NameKey;
            _rarityLbl.text = string.Format("{0}*", unit.Rarity.ToString());
        }

        public void Select()
        {
            if (OnSelected != null)
                OnSelected(_cachedUnit.InstanceID);
        }
    }
}