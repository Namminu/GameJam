using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private AlwaysSpawn[] alwaysThigns;

    private void Start()
    {
        for (int i = 0; i < alwaysThigns.Length; i++) StartCoroutine(DelaySpawn(i));
    }

    private IEnumerator DelaySpawn(int i)
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            Instantiate(alwaysThigns[i].prefab, alwaysThigns[i].spawnPos,
                alwaysThigns[i].prefab.transform.rotation);
            yield return new WaitForSeconds(alwaysThigns[i].SpawnDelay);
        }
    }
    
}


[Serializable]
public class AlwaysSpawn
{
    public GameObject prefab;
    public Vector2 spawnPos;
    [SerializeField]
    private float minSpawnDelay;
    [SerializeField]
    private float maxSpawnDelay;

    public float SpawnDelay => Random.Range(minSpawnDelay, maxSpawnDelay);

}