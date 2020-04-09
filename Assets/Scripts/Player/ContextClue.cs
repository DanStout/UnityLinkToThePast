using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    public GameObject contextClue;

    public void ToggleClue()
    {
        contextClue.SetActive(!contextClue.activeSelf);
    }
}
