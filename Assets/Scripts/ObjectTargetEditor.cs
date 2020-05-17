using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectTargetBehaviour))]
public class ObjectTargetEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


       // ObjectTargetBehaviour ob_trgt = (ObjectTargetBehaviour)target;

        /*EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Toggle("X", ob_trgt.freeze_x);
        EditorGUILayout.Toggle("Y", ob_trgt.freeze_y);
        EditorGUILayout.Toggle("Z", ob_trgt.freeze_z);
        EditorGUILayout.EndHorizontal();*/
    }
}
