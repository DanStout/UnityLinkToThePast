using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    public GameObject contextClue;

    public void ShowClue(bool shown = true)
    {
        contextClue.SetActive(shown);
    }
}
