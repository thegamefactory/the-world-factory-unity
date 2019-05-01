using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGenerator = target as MapGenerator;
        DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
        {
            mapGenerator.Generate();
        }
    }
}
