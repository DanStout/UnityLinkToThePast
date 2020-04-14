using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    public Inventory inventory;
    public Item contents;
    public Signal raiseItem;
    public GameObject dialogBox;
    public BoolValue isOpen;

    Text text;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        text = dialogBox.GetComponentInChildren<Text>();

        if (isOpen.runtimeValue)
        {
            anim.SetBool("opened", true);
        }
    }

    protected override bool ShowContextClue()
    {
        return !isOpen.runtimeValue;
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (isOpen.runtimeValue)
            {
                dialogBox.SetActive(false);

                if (inventory.currentItem != null)
                {
                    inventory.currentItem = null;
                    raiseItem.Raise();
                }
            }
            else
            {
                dialogBox.SetActive(true);
                text.text = contents.description;
                inventory.AddItem(contents);
                inventory.currentItem = contents;
                raiseItem.Raise();
                isOpen.runtimeValue = true;
                anim.SetBool("opened", true);
                UpdateActiveContextClues(false);
            }
        }
    }

    protected override void OnPlayerLeave()
    {
        dialogBox.SetActive(false);
    }
}
