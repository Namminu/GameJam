using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


#if UNITY_EDITOR
[CustomEditor(typeof(SinglePattern))]
public class SinglePatternEditor : Editor
{
    private SinglePattern value;
    private void OnEnable()
    {
        value = (SinglePattern)target;
    }

    public override void OnInspectorGUI()
    {

        SinglePattern singlePattern = (SinglePattern)target;
        target.
    }
}
#endif