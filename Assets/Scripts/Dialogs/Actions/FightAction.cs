public class FightAction : DialogAction
{
    public override void Do(IDialogController controller)
    {
        controller.StartFight();
    }
}