using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MFPS.Addon.LayoutCustomizer
{
    [CustomEditor(typeof(bl_ModifiableLayout))]
    public class bl_ModifiableLayoutEditor : Editor
    {
        bl_ModifiableLayout script;

        /// <summary>
        /// 
        /// </summary>
        private void OnEnable()
        {
            script = (bl_ModifiableLayout)target;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginVertical("box");
            script.allowModifySize = EditorGUILayout.ToggleLeft("Allow Modify Size", script.allowModifySize, EditorStyles.toolbarButton);
            GUILayout.Space(4);
            if (script.allowModifySize)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Size Range", GUILayout.Width(100));
                GUILayout.Space(4);
                script.allowedSizeRange.x = EditorGUILayout.FloatField(script.allowedSizeRange.x, GUILayout.Width(40));
                EditorGUILayout.MinMaxSlider(ref script.allowedSizeRange.x, ref script.allowedSizeRange.y, 0.1f, 2.25f);
                script.allowedSizeRange.y = EditorGUILayout.FloatField(script.allowedSizeRange.y, GUILayout.Width(40));
                GUILayout.Space(4);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("box");
            script.allowModifyOpacity = EditorGUILayout.ToggleLeft("Allow Modify Opacity", script.allowModifyOpacity, EditorStyles.toolbarButton);
            GUILayout.Space(4);
            if (script.allowModifyOpacity)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Opacity Range", GUILayout.Width(100));
                GUILayout.Space(4);
                script.allowedOpacity.x = EditorGUILayout.FloatField(script.allowedOpacity.x, GUILayout.Width(40));
                EditorGUILayout.MinMaxSlider(ref script.allowedOpacity.x, ref script.allowedOpacity.y, 0.1f, 2.25f);
                script.allowedOpacity.y = EditorGUILayout.FloatField(script.allowedOpacity.y, GUILayout.Width(40));
                GUILayout.Space(4);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(target);
            }
        }
    }
}