using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class FloatingTextAddon
{
    [MenuItem("MFPS/Addons/Floating Text/Integrate")]
    private static void Instegrate()
    {
        var km = GameObject.FindObjectOfType<bl_FloatingTextManager>();
        if (km == null)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath("Assets/Addons/FloatingText/Content/Prefabs/Main/Floating Text Manager.prefab", typeof(GameObject)) as GameObject;
            if (prefab != null)
            {
                GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                instance.transform.SetAsLastSibling();
                Selection.activeGameObject = instance;
                EditorGUIUtility.PingObject(instance);
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                EditorUtility.SetDirty(instance);
                Debug.Log("<color=green>Floating Text Integrated in this map.</color>");
            }
            else { Debug.LogWarning("Couldn't found the floating text prefab!"); }
        }
    }

    [MenuItem("MFPS/Addons/Floating Text/Integrate", true)]
    private static bool InstegrateValidate()
    {
        var km = GameObject.FindObjectOfType<bl_FloatingTextManager>();
        var gm = GameObject.FindObjectOfType<bl_GameManager>();
        return (km == null && gm != null);
    }
}