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
                EditorUtility.SetDirty(gate.Gates[0].GateText);
                EditorUtility.SetDirty(gate.Gates[1].GateText);
            }

            if(GUILayout.Button("Swap Gates"))
            {
                gate.SwapGates();
                EditorUtility.SetDirty(gate);
                EditorUtility.SetDirty(gate.Gates[0].GateText);
                EditorUtility.SetDirty(gate.Gates[1].GateText);
            }
        }
    }
}
