using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeFish : MonoBehaviour
{
    [SerializeField] private float twistTime;

    [SerializeField]
    private AnimationCurve curve;
    [SerializeField] private float speed;

    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;

    private float timer;
    private bool toggle;

    private void OnEnable()
    {
        timer = (transform.position.y - minHeight) / (maxHeight - minHeight) * twistTime;
        if (timer > twistTime) toggle = true;
        else if (timer < 0) toggle = false;
    }

    void Update()
    {
        transform.position += Vector3.left * (speed * Time.deltaTime);
        
        if (timer > twistTime) toggle = true;
        else if (timer < 0) toggle = false;

        timer += toggle ? -Time.deltaTime : Time.deltaTime;
        
        Vector3 pos = transform.position;
        pos.y = minHeight + curve.Evaluate(timer / twistTime) * (maxHeight - minHeight);

        transform.position = pos;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
