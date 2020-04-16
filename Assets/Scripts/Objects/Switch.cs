using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public BoolValue pressed;

    public Sprite pressedSprite;

    public GameObject door;

    private SpriteRenderer spriteRend;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();

        if (pressed.runtimeValue)
        {
            door.SetActive(false);
            PressSwitch();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pressed.runtimeValue = true;
            PressSwitch();
        }
    }

    private void PressSwitch()
    {
        door.SetActive(false);
        spriteRend.sprite = pressedSprite;
    }
}
