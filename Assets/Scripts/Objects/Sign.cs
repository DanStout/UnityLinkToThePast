using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    public GameObject dialogBox;
    public string dialog;

    private Text text;

    void Start()
    {
        text = dialogBox.GetComponentInChildren<Text>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inRange)
        {
            if (dialogBox.activeSelf)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                text.text = dialog;
            }
        }
    }

    protected override void OnPlayerLeave()
    {
        dialogBox.SetActive(false);
    }
}
