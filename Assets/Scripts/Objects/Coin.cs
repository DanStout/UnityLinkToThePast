using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Powerup
{
    public Inventory inventory;

    protected override void OnPickedUp()
    {
        inventory.coins++;
    }
}
