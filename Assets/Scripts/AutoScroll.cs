using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
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
        scrollPosition.y = transform.position.y;
        scrollPosition.x -= Time.deltaTime * speed * GameManager.Instance.GetIncreasementSpeed();
        transform.position = scrollPosition;
    }
}
