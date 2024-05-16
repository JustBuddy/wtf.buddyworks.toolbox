using UnityEngine;
using UnityEditor;

namespace BUDDYWORKS.ToolBox
{
    public class SpawnToolboxPrefab : MonoBehaviour
    {
        public static string prefabGUID = "0e07d7ed7da2cc1449f6cc1be73f35fe";
        // Dependency check
        public static string liltoonShader = "lilToon";

        [MenuItem("BUDDYWORKS/Toolbox/Spawn Prefab...", false, 0)]
        public static void SpawnToolBoxPrefab()
        {
            CheckShaderPresence(liltoonShader);
            SpawnPrefab(prefabGUID);
        }

        private static void SpawnPrefab(string guid)
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(guid);

            if (string.IsNullOrEmpty(prefabPath))
            {
                Debug.LogError("[Toolbox] Prefab with GUID " + guid + " not found.");
                return;
            }

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            GameObject selectedObject = Selection.activeGameObject;

            if (prefab == null)
            {
                Debug.LogError("[Toolbox] Failed to load prefab with GUID " + guid + " at path " + prefabPath);
                return;
            }

            GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            if (selectedObject != null)
            {
                instantiatedPrefab.transform.parent = selectedObject.transform;
            }

            if (instantiatedPrefab != null)
            {
                EditorGUIUtility.PingObject(instantiatedPrefab);
            }
            else
            {
                Debug.LogError("[Toolbox] Failed to instantiate prefab with GUID " + guid);
            }
        }

        private static void CheckShaderPresence(string shaderName)
        {
            // Attempt to find the shader by its name
            Shader shader = Shader.Find(shaderName);

            // Check if the shader was found
            if (shader != null)
            {
                return;
            }
            else
            {
                bool result = EditorUtility.DisplayDialog("Shader missing!", "Some features need \"" + shaderName + "\", please import it before usage.", "Download shader", "Got it!");
                if (result)
                {
                    Application.OpenURL("https://github.com/lilxyzw/lilToon/releases");
                }
                Debug.LogWarning("[Toolbox] Shader \"" + shaderName + "\" is NOT present in the project.");
            }
        }
    }
}
