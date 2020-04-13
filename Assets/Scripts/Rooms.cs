using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    [Header("Rooms in this scene")]
    public List<Room> rooms = new List<Room>();

    void OnDrawGizmosSelected()
    {
        foreach (var room in rooms)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(room.cameraMin, 0.3f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(room.cameraMax, 0.3f);
        }
    }
}
