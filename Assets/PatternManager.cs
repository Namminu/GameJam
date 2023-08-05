using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = System.Numerics.Quaternion;
using Random = UnityEngine.Random;

public class PatternManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset[] datas;
    private Alldata[] patterns;
    
    private Vector2[] spawnPos = new Vector2[5];
    
    [SerializeField] private GameObject[] prefabs;

    [SerializeField] private float spawnDelay;
    [SerializeField] private float patternDelay;

    private int currentIndex;

    private int patternType;
    
    private float spaceY;
    private Vector2 offset;

    private void Awake()
    {
        patterns = new Alldata[datas.Length];
        for(int i = 0; i < datas.Length; i++) patterns[i] = JsonUtility.FromJson<Alldata>(datas[i].text);

        Debug.Log(patterns[0].pattern);
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        spaceY = col.size.y / 5;
        offset = new Vector2(transform.position.x, transform.position.y - col.size.y / 2);
        
        for (int i = 0; i < 5; i++)
        {
            spawnPos[i] = offset + Vector2.up * spaceY * (i + 1);
        }
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }
    
    IEnumerator Spawn()
    {
        currentIndex = Random.Range(0, patterns.Length);
        WaitForSeconds spawnDelayWaitForSeconds = new WaitForSeconds(spawnDelay);
        foreach (var line in patterns[currentIndex].pattern)
        {
            SpawnLine(line.lineA, 0);
            SpawnLine(line.lineB, 1);
            SpawnLine(line.lineC, 2);
            SpawnLine(line.lineD, 3);
            SpawnLine(line.lineE, 4);
            yield return spawnDelayWaitForSeconds;
        }
        yield return new WaitForSeconds(patternDelay);
        StartCoroutine(Spawn());
    }

    private void SpawnLine(string type, int i)
    {
        GameObject go;
        
        switch (type)
        {
            case "a":
                go = prefabs[0];
                break;
            case "b":
                go = prefabs[1];
                break;
            default:
                return;
        }

        Instantiate(go, spawnPos[i], UnityEngine.Quaternion.Euler(new Vector3(0, 0, 90)));
    }
}


[Serializable]
public class Alldata
{
    public SingleLine[] pattern;
}

[Serializable]
public class SingleLine
{
    public string lineA;
    public string lineB;
    public string lineC;
    public string lineD;
    public string lineE;
}