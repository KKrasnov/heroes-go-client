using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class DialogOptionElement : BaseUIElement
    {
        [SerializeField]
        private Image _btnImg;

        [SerializeField]
        private Text _optionTextLbl;

        private Action<DialogOptionData> _OnSelect;

        private DialogOptionData _cachedData;

        public void Apply(DialogOptionData data, Action<DialogOptionData> onSelect)
        {
            _cachedData = data;
            _OnSelect += onSelect;
            _optionTextLbl.text = CompositionRoot.Container.Resolve<ILocalizationService>().GetLocalizedText(_cachedData.AnswerKey);
        }

        public void Select()
        {
            if (_OnSelect != null)
                _OnSelect(_cachedData);
        }
    }
}