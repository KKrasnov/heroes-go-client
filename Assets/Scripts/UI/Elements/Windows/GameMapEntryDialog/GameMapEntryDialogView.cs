using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GameMapEntryDialogView : BaseWindowView<GameMapEntryData>
    {
        public override WindowType WindowType
        {
            get
            {
                return WindowType.GameMapEntryDialog;
            }
        }

        [SerializeField]
        private HeroPreviewElement _heroPreview;

        [SerializeField]
        private Text _entryTitleLbl;

        [SerializeField]
        private Text _dialogTextLbl;

        [SerializeField]
        private Transform _optionsContainer;

        [SerializeField]
        private GameObject _optionPrefab;

        private GameMapEntryData _cachedData;

        private DialogData _cachedDialog;

        private List<DialogOptionElement> _dialogOptionsObjects = new List<DialogOptionElement>();

        public event Action<DialogOptionData> OnOptionSelectedEvent;

        public override void UpdateView(GameMapEntryData data)
        {
            base.UpdateView(data);
            _cachedData = data;
            _cachedDialog = CompositionRoot.Container.Resolve<IDialogService>().GetDialogData(_cachedData.EntryDialogId);

            ILocalizationService localizationService = CompositionRoot.Container.Resolve<ILocalizationService>();

            _entryTitleLbl.text = localizationService.GetLocalizedText(_cachedData.EntryNameKey);
            _heroPreview.Apply(_cachedData.Garrison.Heroes[0]);
        }

        public void ShowSpeech(string speechKey)
        {
            ClearDialogOptions();
            DialogSpeechData speech = _cachedDialog.Speeches[speechKey];

            _dialogTextLbl.text = CompositionRoot.Container.Resolve<ILocalizationService>().GetLocalizedText(speech.TextKey);

            foreach (DialogOptionData option in speech.Options)
            {
                CreateDialogOption(option);
            }
        }

        private void CreateDialogOption(DialogOptionData data)
        {
            GameObject optionObject = GameObject.Instantiate(_optionPrefab);
            optionObject.transform.SetParent(_optionsContainer);
            optionObject.transform.localPosition = Vector3.zero;
            optionObject.transform.localEulerAngles = Vector3.zero;
            optionObject.transform.localScale = Vector3.one;

            DialogOptionElement component = optionObject.GetComponent<DialogOptionElement>();
            component.Apply(data, OnOptionSelected);
            _dialogOptionsObjects.Add(component);
        }

        private void ClearDialogOptions()
        {
            foreach(var item in _dialogOptionsObjects)
            { 
                GameObject.Destroy(item.gameObject);
            }
            _dialogOptionsObjects.Clear();
        }

        private void OnOptionSelected(DialogOptionData data)
        {
            if (OnOptionSelectedEvent != null)
                OnOptionSelectedEvent(data);
        }
    }
}