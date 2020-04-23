using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Room : MonoBehaviour
{
    public string displayName;

    public Vector2 CameraMin => roomCollider.bounds.min;
    public Vector2 CameraMax => roomCollider.bounds.max;

    protected Enemy[] enemies;
    private MonoBehaviour[] allMonos;
    private Collider2D roomCollider;

    void Awake()
    {
        roomCollider = GetComponent<Collider2D>();
        enemies = GetComponentsInChildren<Enemy>();
        var pots = GetComponentsInChildren<Pot>();

        allMonos = enemies
            .Concat<MonoBehaviour>(pots)
            .ToArray();
    }

    protected virtual void OnPlayerExit()
    {

    }

    protected virtual void OnPlayerEnter()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || other.isTrigger) return;

        foreach (var mono in allMonos)
        {
            mono.gameObject.SetActive(true);
        }

        OnPlayerEnter();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || other.isTrigger) return;

        foreach (var mono in allMonos)
        {
            mono.gameObject.SetActive(false);
        }

        OnPlayerExit();
    }
}
