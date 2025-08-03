using Quests;
using UnityEngine;

namespace Dialog
{
    public class RemoveDrinkOptionSelectHandler : IDialogOptionSelectHandler
    {
        public void HandleOptionSelected(DialogOptionContext context)
        {
            PlayerStateController.GetInstance().RemoveDrink();
        }
    }
}