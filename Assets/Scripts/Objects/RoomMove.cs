using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector3 playerChange;
    public GameObject text;
    public Room newRoom;

    private CameraMovement cam;
    private Text placeText;

    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
        if (text != null)
        {
            placeText = text.GetComponent<Text>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            cam.EnterRoom(newRoom);
            other.transform.position += playerChange;

            if (text != null && !string.IsNullOrWhiteSpace(newRoom.displayName))
            {
                StartCoroutine(placeNameCo());
            }
        }
    }

    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = newRoom.displayName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
