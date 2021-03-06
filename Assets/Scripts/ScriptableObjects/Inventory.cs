﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;
    public int coins;

    void OnEnable()
    {
        coins = 0;
        numberOfKeys = 0;
    }

    public void AddItem(Item item)
    {
        if (item.isKey)
        {
            numberOfKeys++;
        }
        else
        {
            items.Add(item);
        }
    }
}
