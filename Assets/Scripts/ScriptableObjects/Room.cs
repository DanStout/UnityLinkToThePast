using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Room : ScriptableObject
{
    public string displayName;

    public Vector2 cameraMin;

    public Vector2 cameraMax;
}
