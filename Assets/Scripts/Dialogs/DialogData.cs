using System;
using System.Collections.Generic;

public class DialogData
{
    public Guid ID;

    public Dictionary<string, DialogSpeechData> Speeches;

    public DialogData()
    {
        Speeches = new Dictionary<string, DialogSpeechData>();
    }

    public DialogData(DialogSpeechData[] speeches) : this()
    {
        GenerateSpeechesDictionary(speeches);
    }

    private void GenerateSpeechesDictionary(DialogSpeechData[] speeches)
    {
        foreach(var speech in speeches)
        {
            Speeches.Add(speech.SpeechKey, speech);
        }
    }
}

public class DialogSpeechData
{
    public string SpeechKey;

    public string TextKey;

    public DialogOptionData[] Options;
}

public class DialogOptionData
{
    public enum DialogOptionStyle
    {
        Neutral,
        Negative,
        Positive
    }

    public DialogOptionStyle Style = DialogOptionStyle.Neutral;

    public string AnswerKey;

    public DialogAction[] Consequences;
}
