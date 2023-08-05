using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    Vector2 scrollPosition;
    public float speed;

    private void Start()
    {
        scrollPosition = transform.position;
    }

    void Update()
    {
        scrollPosition.x -= Time.deltaTime * speed * GameManager.Instance.GetIncreasementSpeed();
        transform.position = scrollPosition;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
