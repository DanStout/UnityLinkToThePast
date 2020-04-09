using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 topRight;
    public Vector2 bottomLeft;

    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    void LateUpdate()
    {
        var targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        targetPos.x = Mathf.Clamp(targetPos.x, bottomLeft.x, topRight.x);
        targetPos.y = Mathf.Clamp(targetPos.y, bottomLeft.y, topRight.y);

        if (transform.position != targetPos)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }
}
