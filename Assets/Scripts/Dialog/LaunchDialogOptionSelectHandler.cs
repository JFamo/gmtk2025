namespace Dialog
{
    public class LaunchDialogOptionSelectHandler : IDialogOptionSelectHandler
    {
        public void HandleOptionSelected(DialogOptionContext context)
        {
            DialogPanelController.GetInstance().LaunchDialog(context.GetFollowUpDialog());
        }
    }
}