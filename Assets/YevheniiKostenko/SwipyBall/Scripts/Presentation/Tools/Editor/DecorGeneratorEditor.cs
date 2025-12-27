#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Presentation.Tools
{
    [CustomEditor(typeof(DecorGenerator))]
    public class DecorGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            DecorGenerator decorGenerator = (DecorGenerator)target;
            if (GUILayout.Button("Generate Decors"))
            {
                decorGenerator.GenerateDecors();
            }
        }
    }
}
#endif