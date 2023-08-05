using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll : IObstacle
{
    Vector2 scrollPosition;
    public float speed;

    private void Start()
    {
        scrollPosition = transform.position;
    }

    void Update()
    {
        scrollPosition.y = transform.position.y;
        scrollPosition.x -= Time.deltaTime * speed * GameManager.Instance.GetIncreasementSpeed();
        transform.position = scrollPosition;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}


public abstract class IObstacle : MonoBehaviour
{
    public float damage;
}