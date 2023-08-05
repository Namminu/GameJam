using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFish : MonoBehaviour
{
    [SerializeField] private float speed;
    
    void Update()
    {
        transform.position += Vector3.left * (speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
