using System;
using System.Collections.Generic;
using UnityEngine;

namespace MFPS.Addon.LayoutCustomizer
{
    [Serializable]
    public class LayoutSetupData
    {
        public List<LayoutData> Layouts = new List<LayoutData>();

        /// <summary>
        /// Get the saved data for a specific data
        /// </summary>
        /// <param name="layoutKey"></param>
        /// <returns></returns>
        public LayoutRectData GetDataFor(string layoutKey)
        {
            if (Layouts == null || Layouts.Count <= 0) return null;

            var index = Layouts.FindIndex(x => x.Key == layoutKey);
            if (index == -1) return null;

            return Layouts[index].Data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layout"></param>
        public void AddData(bl_ModifiableLayout layout)
        {
            var data = new LayoutData();
            data.Key = layout.layoutKey;
            data.Data = layout.ThisRectData;

            if(Layouts.Exists(x => x.Key == data.Key))
            {
                Debug.LogWarning("There is a layout with the same key.", layout);
                return;
            }
            Layouts.Add(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public static void SaveFor(string layoutKey, LayoutSetupData data)
        {
            var key = $"{Application.productName}.layoutsetup.{layoutKey}";
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, json);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layoutKey"></param>
        /// <returns></returns>
        public static LayoutSetupData LoadSetup(string layoutKey)
        {
            var key = $"{Application.productName}.layoutsetup.{layoutKey}";
            if (!PlayerPrefs.HasKey(key)) return null;

            var json = PlayerPrefs.GetString(key);
            return LoadSetupFromJson(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static LayoutSetupData LoadSetupFromJson(string json)
        {
            var data = JsonUtility.FromJson<LayoutSetupData>(json);
            return data;
        }

        [Serializable]
        public class LayoutData
        {
            public string Key;
            public LayoutRectData Data;
        }
    }
}