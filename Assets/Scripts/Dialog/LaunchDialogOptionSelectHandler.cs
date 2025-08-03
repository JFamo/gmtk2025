using UnityEngine;

namespace Dialog
{
    public class LaunchDialogOptionSelectHandler : IDialogOptionSelectHandler
    {
        public void HandleOptionSelected(DialogOptionContext context)
        {
            Debug.Log("Launching follow-up dialog");
            DialogPanelController.GetInstance().LaunchDialog(context.GetFollowUpDialog());
        }
    }
}