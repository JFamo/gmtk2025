namespace Dialog
{
    public class AddHealthOptionSelectHandler : IDialogOptionSelectHandler
    {
        public void HandleOptionSelected(DialogOptionContext context)
        {
            PlayerStateController.GetInstance().MaxHealth();
        }
    }
}