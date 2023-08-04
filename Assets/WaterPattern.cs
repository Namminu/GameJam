using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPattern : MonoBehaviour
{
    
}

[Serializable]
public class SingleLine
{
    [Range(0, 4)]
    public int count;
}

[Serializable]
public class SinglePattern
{
    public int lineCount;
}
