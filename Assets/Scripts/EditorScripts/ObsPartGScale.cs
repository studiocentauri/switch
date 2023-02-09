using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObsPartHelperMono))]
public class ObsPartGScale : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ObsPartHelperMono myScript = (ObsPartHelperMono)target;
        if(GUILayout.Button("Buttons"))
        {
            myScript.Organize();
        }
    }
}
