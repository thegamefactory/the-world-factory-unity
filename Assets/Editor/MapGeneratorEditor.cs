using UnityEditor;
using UnityEngine;
using TWF;

[CustomEditor (typeof (MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGenerator = target as MapGenerator;
        DrawDefaultInspector();

        if ((DrawDefaultInspector() && mapGenerator.autoUpdate)
            || GUILayout.Button("Generate"))
        {
            mapGenerator.Generate(Root.GetInstance<GameService>());
        }
    }
}
