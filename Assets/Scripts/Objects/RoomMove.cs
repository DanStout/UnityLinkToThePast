using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector3 playerChange;
    public string placeName;
    public GameObject text;

    public Transform newCameraMin;
    public Transform newCameraMax;

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
            cam.min = newCameraMin;
            cam.max = newCameraMax;
            other.transform.position += playerChange;

            if (text != null && !string.IsNullOrWhiteSpace(placeName))
            {
                StartCoroutine(placeNameCo());
            }
        }
    }

    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
