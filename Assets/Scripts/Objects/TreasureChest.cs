using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    public Inventory inventory;
    public Item contents;
    public bool isOpen;
    public Signal raiseItem;
    public GameObject dialogBox;

    private Text text;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        text = dialogBox.GetComponentInChildren<Text>();
    }

    protected override bool ShowContextChanged()
    {
        return !isOpen;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inRange)
        {
            if (isOpen)
            {
                if (inventory.currentItem != null)
                {
                    dialogBox.SetActive(false);
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
                isOpen = true;
                anim.SetBool("opened", true);
                contextChanged.Raise();
            }
        }
    }
}
