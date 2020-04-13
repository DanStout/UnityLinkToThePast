using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Transform min;
    public Transform max;

    private Animator anim;
    private Camera cam;
    private float xSize;
    private float ySize;

    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        cam = GetComponent<Camera>();
        anim = GetComponent<Animator>();

        ySize = cam.orthographicSize;
        xSize = ySize * cam.aspect;
    }

    void LateUpdate()
    {
        var minP = min.position;
        var maxP = max.position;

        var x = Mathf.Clamp(target.position.x, minP.x + xSize, maxP.x - xSize);
        var y = Mathf.Clamp(target.position.y, minP.y + ySize, maxP.y - ySize);

        var targetPos = new Vector3(x, y, transform.position.z);

        if (transform.position != targetPos)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }

    public void ScreenKick()
    {
        anim.SetTrigger("kick");
    }
}
