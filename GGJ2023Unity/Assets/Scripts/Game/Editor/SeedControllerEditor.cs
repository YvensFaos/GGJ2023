using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    [CustomEditor(typeof(SeedController))]
    public class SeedControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var controller = (SeedController)target;
            GUILayout.Label($"Growth Step: {controller.GrowthStep}");
        }
    }
}