using UnityEngine;
using UnityEditor;

namespace AngryKoala.RunnerControls
{
    [CustomEditor(typeof(RunnerLevel))]
    public class RunnerLevelEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            RunnerLevel runnerLevel = (RunnerLevel)target;

            DrawDefaultInspector();

            if(GUILayout.Button("Place Path Sections"))
            {
                runnerLevel.PlacePathSections();
            }
        }
    }
}
