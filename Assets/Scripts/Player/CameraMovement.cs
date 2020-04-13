using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Room initialRoom;

    private Animator anim;
    private Camera cam;
    private float xSize;
    private float ySize;
    private Vector2 min;
    private Vector2 max;

    void Start()
    {
        min = initialRoom.cameraMin;
        max = initialRoom.cameraMax;

        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        cam = GetComponent<Camera>();
        anim = GetComponent<Animator>();

        ySize = cam.orthographicSize;
        xSize = ySize * cam.aspect;
    }

    void LateUpdate()
    {
        var x = Mathf.Clamp(target.position.x, min.x + xSize, max.x - xSize);
        var y = Mathf.Clamp(target.position.y, min.y + ySize, max.y - ySize);

        var targetPos = new Vector3(x, y, transform.position.z);

        if (transform.position != targetPos)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }

    public void EnterRoom(Room room)
    {
        min = room.cameraMin;
        max = room.cameraMax;
    }

    public void ScreenKick()
    {
        anim.SetTrigger("kick");
    }
}
