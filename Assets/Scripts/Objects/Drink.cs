using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : Collectible
{
    protected override void HandleCollected() {
        PlayerStateController.GetInstance().AddDrink();
    }
}