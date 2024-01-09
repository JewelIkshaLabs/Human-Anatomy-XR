using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PartLabeller))]
public class PartLabellerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PartLabeller partLabeller = (PartLabeller)target;

        if (GUILayout.Button("Nomenclate Parts"))
        {
            partLabeller.Nomenclate(partLabeller._model);
        }
    }
}
