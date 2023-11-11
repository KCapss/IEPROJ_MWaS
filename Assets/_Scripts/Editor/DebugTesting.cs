using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TurnIndicator))]


public class DebugTesting : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TurnIndicator t = (TurnIndicator)target;
        if (GUILayout.Button("Restart Animation"))
        {
            t.Restart();
        }
    }

}
