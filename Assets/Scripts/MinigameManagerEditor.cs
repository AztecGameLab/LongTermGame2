using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MinigameManager))]
public class MinigameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MinigameManager myTarget = (MinigameManager)target;
        if (GUILayout.Button("skip scene"))
        {
            myTarget.nextScene();
        }

    }
}
