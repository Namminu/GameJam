using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlip : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject, 0.417f);
    }
}