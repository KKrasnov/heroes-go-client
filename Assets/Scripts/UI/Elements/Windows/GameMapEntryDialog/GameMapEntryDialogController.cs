using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class GameMapEntryDialogController : BaseWindowController<GameMapEntryDialogView, Guid>, IDialogController
    {
        private GameMapEntryData _entryData;

        public override void Initialize(BaseWindowView view)
        {
            base.Initialize(view);
            _view.OnOptionSelectedEvent += OnOptionSelectedHandler;
        }

        public override void ApplyData(object data)
        {
            base.ApplyData(data);
            _entryData = ProjectContext.Instance.Container.Resolve<IGameMapService>().GetEntryData(_windowData);
            _view.UpdateView(_entryData);
            ShowSpeech("main");
        }

        private void OnOptionSelectedHandler(DialogOptionData optionData)
        {
            foreach(DialogAction action in optionData.Consequences)
            {
                action.Do(this);
            }
        }

        public override void Dispose()
        {
            ProjectContext.Instance.Container.Resolve<IGameMapManager>().RefreshMapEntry(_windowData);
            base.Dispose();
        }

        protected override void OnCloseWindowClickHandler()
        {
            base.OnCloseWindowClickHandler();
        }

        #region IDialogController impl

        public void ShowSpeech(string speechKey)
        {
            _view.ShowSpeech(speechKey);
        }

        public void StartFight()
        {
            ProjectContext.Instance.Container.Resolve<IMapEntryBattleSetupService>().SetupBattle(_entryData);
        }

        public void Leave()
        {
            _view.Close();
        }
        #endregion
    }
}