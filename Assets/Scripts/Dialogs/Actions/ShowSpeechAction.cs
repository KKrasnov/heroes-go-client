public class ShowSpeechAction : DialogAction
{
    private string _speechKey;

    public ShowSpeechAction(string key)
    {
        _speechKey = key;
    }

    public override void Do(IDialogController controller)
    {
        controller.ShowSpeech(_speechKey);
    }
}