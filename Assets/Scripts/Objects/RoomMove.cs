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
    public Tilemap destinationTilemap;

    private Vector2 cameraMin;
    private Vector2 cameraMax;

    private CameraMovement cam;
    private Text placeText;

    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
        placeText = text.GetComponent<Text>();

        if (destinationTilemap != null)
        {
            destinationTilemap.CompressBounds();
            var bnd = destinationTilemap.cellBounds;

            var ySize = Camera.main.orthographicSize;
            var xSize = ySize * Camera.main.aspect;

            cameraMin = new Vector2(bnd.xMin + xSize, bnd.yMin + ySize);
            cameraMax = new Vector2(bnd.xMax - xSize, bnd.yMax - ySize);
        }
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            cam.bottomLeft = cameraMin;
            cam.topRight = cameraMax;
            other.transform.position += playerChange;

            if (!string.IsNullOrWhiteSpace(placeName))
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
