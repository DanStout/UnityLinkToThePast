using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    Key,
    Enemy,
    Button
}

public class Door : Interactable
{
    [Header("Door Variables")]
    public DoorType DoorType;

    public Inventory inventory;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inRange && (DoorType == DoorType.Key && inventory.numberOfKeys > 0))
        {
            inventory.numberOfKeys--;
            UpdateActiveContextClues(false);
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
