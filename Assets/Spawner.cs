using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    

    [SerializeField] private AlwaysSpawn[] alwaysThigns;
    private bool[] isStops;


    private void Awake()
    {
        isStops = new bool[alwaysThigns.Length];
        if (Instance == null) Instance = this;
        else if(Instance != this) Destroy(gameObject);
    }
    
    private void Start()
    {
        for (int i = 0; i < alwaysThigns.Length; i++) StartCoroutine(DelaySpawn(i));
        
    }

    private IEnumerator DelaySpawn(int i)
    {
        yield return new WaitForSeconds(2f);
        Random.InitState(Mathf.CeilToInt(Time.deltaTime * 1000));
        while (true)
        {
            yield return new WaitUntil(() => !isStops[i]);
            Instantiate(alwaysThigns[i].prefab, alwaysThigns[i].spawnPos,
                alwaysThigns[i].prefab.transform.rotation);
            yield return new WaitForSeconds(alwaysThigns[i].SpawnDelay);
        }
    }


    public void WaterSpawnStop()
    {
        for (int i = 0; i < 4; i++)
        {
            isStops[i + 2] = true;
        }
    }

    public void ReStartWaterSpawn()
    {
        for (int i = 0; i < isStops.Length; i++)
        {
            isStops[i] = false;
        }
    }
}




[Serializable]
public class AlwaysSpawn
{
    public GameObject prefab;


    [SerializeField]
    private float minSpawnY;
    
    [SerializeField]
    private float maxSpawnY;
    public Vector2 spawnPos => new Vector2(15, Random.Range(minSpawnY, maxSpawnY));
    [SerializeField]
    private float minSpawnDelay;
    [SerializeField]
    private float maxSpawnDelay;

    public float SpawnDelay => Random.Range(minSpawnDelay, maxSpawnDelay);
}