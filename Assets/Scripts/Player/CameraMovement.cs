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

    void Awake()
    {
        cam = GetComponent<Camera>();
        anim = GetComponent<Animator>();

        ySize = cam.orthographicSize;
        xSize = ySize * cam.aspect;
    }

    void Start()
    {
        min = initialRoom.CameraMin;
        max = initialRoom.CameraMax;

        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
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
        min = room.CameraMin;
        max = room.CameraMax;
    }

    public void ScreenKick()
    {
        anim.SetTrigger("kick");
    }
}
