using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPiece : Powerup
{
    public FloatValue playerHealth;
    public float healthValue;
    public FloatValue heartContainers;

    protected override void OnPickedUp()
    {
        playerHealth.runtimeValue += healthValue;
        if (playerHealth.runtimeValue >= heartContainers.initialValue * 2f)
        {
            playerHealth.runtimeValue = heartContainers.initialValue * 2f;
        }
    }

}
