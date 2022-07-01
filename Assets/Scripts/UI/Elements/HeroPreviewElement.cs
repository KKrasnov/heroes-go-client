using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class HeroPreviewElement : BaseUIElement
    {
        [SerializeField]
        private Image _previewImg;

        [SerializeField]
        private Text _previewText;

        [SerializeField]
        private Text _heroHPLbl;

        public void Apply(HeroData hero)
        {
            if(_previewImg)
                _previewImg.sprite = Resources.Load<Sprite>(hero.PreviewSpritePath);
            if(_previewText)
                _previewText.text = hero.NameKey;
            if(_heroHPLbl)
                _heroHPLbl.text = string.Format("HP: {0}/{1}", hero.CurrentHP, hero.MaxHP);
        }
    }
}