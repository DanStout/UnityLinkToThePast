using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinTextManager : MonoBehaviour
{
    public Inventory inventory;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        UpdateCoinCount();
    }

    public void UpdateCoinCount()
    {
        text.text = inventory.coins.ToString();
    }
}
