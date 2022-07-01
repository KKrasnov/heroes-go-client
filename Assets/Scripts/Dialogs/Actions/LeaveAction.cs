public class LeaveAction : DialogAction
{
    public override void Do(IDialogController controller)
    {
        controller.Leave();
    }
}