using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private AlwaysSpawn[] alwaysThigns;

    private void Update()
    {
        for (int i = 0; i < alwaysThigns.Length; i++)
        {
            alwaysThigns[i].spawnTimer += Time.deltaTime;
            if (alwaysThigns[i].spawnDelay < alwaysThigns[i].spawnTimer)
            {
                alwaysThigns[i].spawnTimer = 0;
                Instantiate(alwaysThigns[i].prefab, alwaysThigns[i].spawnPos,
                    alwaysThigns[i].prefab.transform.rotation);
            }
        }
    }
}


[Serializable]
public class AlwaysSpawn
{
    public GameObject prefab;
    public Vector2 spawnPos;
    public float spawnDelay;
    [NonSerialized]
    public float spawnTimer;
}