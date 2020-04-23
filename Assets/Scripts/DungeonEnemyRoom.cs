using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{
    public Door[] doors;

    void Start()
    {
        doors = GetComponentsInChildren<Door>(includeInactive: true);
        SetDoorsActive(false);
    }

    private void SetDoorsActive(bool active)
    {
        foreach(var door in doors)
        {
            door.gameObject.SetActive(active);
        }
    }

    protected override void OnPlayerEnter()
    {
        SetDoorsActive(true);
    }

    public void CheckEnemies()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                return;
            }
        }

        SetDoorsActive(false);
    }
}
