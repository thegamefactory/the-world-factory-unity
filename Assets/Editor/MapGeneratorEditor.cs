using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGenerator = this.target as MapGenerator;
        this.DrawDefaultInspector();

        if ((this.DrawDefaultInspector() && mapGenerator.AutoUpdate)
            || GUILayout.Button("Generate"))
        {
            mapGenerator.Generate();
        }
    }
}
