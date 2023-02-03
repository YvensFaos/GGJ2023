using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Utils.Editor
{
    public class SceneNavigationWindow : EditorWindow
    {
        private Vector2 _scrollPos;

        [MenuItem("Window/Scene Navigation")]
        public static void ShowWindow()
        {
            GetWindow<SceneNavigationWindow>("Scene Navigation");
        }

        private void OnGUI()
        {
            var labelStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold
            };
            
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

            EditorGUILayout.LabelField("Build Scenes:", labelStyle);
            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (!scene.enabled)
                {
                    continue;
                }

                if (GUILayout.Button($"Open [{scene.path.Split('/').Last()}]"))
                {
                    EditorSceneManager.OpenScene(scene.path);
                }
            }

            EditorGUILayout.EndScrollView();
        }
    }
}