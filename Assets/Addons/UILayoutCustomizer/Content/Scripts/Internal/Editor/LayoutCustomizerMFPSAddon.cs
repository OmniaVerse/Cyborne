using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace MFPS.Addon.LayoutCustomizer
{
    public class LayoutCustomizerMFPSAddon
    {

        [MenuItem("MFPS/Addons/Layout Customizer/Integrate")]
        private static void Instegrate()
        {
            bool integrate = false;
            GameObject prefab = AssetDatabase.LoadAssetAtPath("Assets/Addons/UILayoutCustomizer/Content/Prefabs/Main/Layout Customizer.prefab", typeof(GameObject)) as GameObject;
            if (prefab != null)
            {
                GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                if (bl_UIReferences.Instance != null)
                {
                    instance.transform.SetParent(bl_UIReferences.Instance.transform, false);
                    instance.transform.SetAsLastSibling();
                }
                SetupDefaultWrappers();
                Selection.activeGameObject = instance;
                EditorGUIUtility.PingObject(instance);
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                EditorUtility.SetDirty(instance);
                integrate = true;

            }
            else { Debug.LogWarning("Couldn't found the layout customizer prefab!"); }

            prefab = AssetDatabase.LoadAssetAtPath("Assets/Addons/UILayoutCustomizer/Content/Prefabs/Instances/Modify Layout Setting.prefab", typeof(GameObject)) as GameObject;
            if (prefab != null)
            {
                if (bl_UIReferences.Instance.addonReferences[2].GetComponentInChildren<bl_CustomizeLayoutButton>(true) == null)
                {
                    GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                    if (bl_UIReferences.Instance != null)
                    {
                        instance.transform.SetParent(bl_UIReferences.Instance.addonReferences[2].transform, false);
                        int i = bl_UIReferences.Instance.addonReferences[2].transform.childCount;
                        instance.transform.SetSiblingIndex(i - 2);
                    }

                    EditorUtility.SetDirty(instance);
                }
                integrate = true;
            }
            else { Debug.LogWarning("Couldn't found the layout customizer setting prefab!"); }

            if (integrate) Debug.Log("<color=green>Layout Customizer Integrated in this map.</color>");
        }

        public static void SetupDefaultWrappers()
        {
            var all = new List<bl_ModifiableLayoutWrapper>();

            all.AddRange(bl_UIReferences.Instance.transform.GetComponentsInChildren<bl_ModifiableLayoutWrapper>(true));
#if MFPSM
            if (bl_TouchHelper.Instance != null)
            {
                all.AddRange(bl_TouchHelper.Instance.transform.GetComponentsInChildren<bl_ModifiableLayoutWrapper>(true));
            }
#endif

            for (int i = 0; i < all.Count; i++)
            {
                var script = all[i].GetComponent<bl_ModifiableLayout>();
                if (script != null) continue;

                script = all[i].gameObject.AddComponent<bl_ModifiableLayout>();
                script.allowedOpacity = all[i].allowedOpacity;
                script.allowModifySize = all[i].allowModifySize;
                script.allowModifyOpacity = all[i].allowModifyOpacity;
                script.allowedSizeRange = all[i].allowedSizeRange;
                EditorUtility.SetDirty(script);
            }
        }

        /*[MenuItem("MFPS/Addons/Layout Customizer/Remove Integration")]
        public static void RemoveIntegrationWrappers()
        {
            var all = new List<bl_ModifiableLayout>();

            all.AddRange(bl_UIReferences.Instance.transform.GetComponentsInChildren<bl_ModifiableLayout>(true));
#if MFPSM
            if (bl_TouchHelper.Instance != null)
            {
                all.AddRange(bl_TouchHelper.Instance.transform.GetComponentsInChildren<bl_ModifiableLayout>(true));
            }
#endif

            for (int i = 0; i < all.Count; i++)
            {
                var go = all[i].gameObject;
                Object.DestroyImmediate(all[i]);
                EditorUtility.SetDirty(go);
            }
        }*/

        [MenuItem("MFPS/Addons/Layout Customizer/Integrate", true)]
        private static bool InstegrateValidate()
        {
            var km = GameObject.FindObjectOfType<bl_LayoutCustomizer>();
            bl_GameManager gm = GameObject.FindObjectOfType<bl_GameManager>();
            return (km == null && gm != null);
        }
    }
}