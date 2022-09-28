using UnityEngine;
using UnityEditor;

namespace AngryKoala.Interaction
{
    [CustomEditor(typeof(GateController))]
    public class GateControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GateController gate = (GateController)target;

            base.OnInspectorGUI();

            if(GUILayout.Button("Adjust Gate Texts"))
            {
                gate.AdjustGateTexts();
                EditorUtility.SetDirty(gate);
            }

            if(GUILayout.Button("Swap Gates"))
            {
                gate.SwapGates();
                EditorUtility.SetDirty(gate);
            }
        }
    }
}
