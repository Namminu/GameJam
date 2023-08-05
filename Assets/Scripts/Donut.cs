using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 60;
    
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.CompareTag("Obstacle")) return;
        if(other.CompareTag("Player")) Destroy(gameObject);
    }
}
