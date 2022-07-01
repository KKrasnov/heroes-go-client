using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class HeroesListWindowItem : BaseUIElement
    {
        [SerializeField]
        private Text _heroNameLbl;

        [SerializeField]
        private Text _heroHPLbl;

        [SerializeField]
        private Image _heroPreviewImg;

        public event Action<HeroData> OnSelected;

        private HeroData _cachedHero;

        public void ApplyModel(HeroData hero)
        {
            _cachedHero = hero;

            _heroNameLbl.text = hero.NameKey;
            _heroHPLbl.text = string.Format("HP: {0}/{1}", hero.CurrentHP, hero.MaxHP);

            _heroPreviewImg.sprite = Resources.Load<Sprite>(hero.PreviewSpritePath);
        }

        public void Select()
        {
            if (OnSelected != null)
                OnSelected(_cachedHero);
        }
    }
}