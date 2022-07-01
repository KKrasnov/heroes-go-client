using System;
using System.Collections.Generic;

public class MockDialogService : IDialogService
{
    private Dictionary<Guid, DialogData> _dialogs = new Dictionary<Guid, DialogData>();

    public MockDialogService()
    {
        AddNewDialog(new DialogData(
            new DialogSpeechData[]
            {
                new DialogSpeechData()
                {
                    SpeechKey = "main",
                    TextKey = "dlg_orc_robbers_main",
                    Options = new DialogOptionData[]
                    {
                        new DialogOptionData()
                        {
                            AnswerKey = "dlg_fight",
                            Consequences = new DialogAction[]
                            {
                                new FightAction()
                            }
                        },
                        new DialogOptionData()
                        {
                            AnswerKey = "dlg_what_do_you_want",
                            Consequences = new DialogAction[]
                            {
                                new ShowSpeechAction("what")
                            }
                        }
                    }
                },
                new DialogSpeechData()
                {
                    SpeechKey = "what",
                    TextKey = "dlg_orc_robbers_what_want",
                    Options = new DialogOptionData[]
                    {
                        new DialogOptionData()
                        {
                            AnswerKey = "dlg_then_lets_fight",
                            Consequences = new DialogAction[]
                            {
                                new FightAction()
                            }
                        },
                        new DialogOptionData()
                        {
                            AnswerKey = "dlg_back",
                            Consequences = new DialogAction[]
                            {
                                new ShowSpeechAction("main")
                            }
                        },
                        new DialogOptionData()
                        {
                            AnswerKey = "dlg_leave",
                            Consequences = new DialogAction[]
                            {
                                new LeaveAction()
                            }
                        }
                    }
                }
            }
            )
        {
            ID = new Guid("00000000000000000000000000000001")
        });
    }

    public DialogData GetDialogData(Guid dialogId)
    {
        return _dialogs[dialogId];
    }

    private void AddNewDialog(DialogData data)
    {
        _dialogs.Add(data.ID, data);
    }
}
