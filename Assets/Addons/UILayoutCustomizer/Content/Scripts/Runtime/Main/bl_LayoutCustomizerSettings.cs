using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "LayoutCustomizerSettings", menuName = "Lovatto/Layout Customizer/Settinga")]
public class bl_LayoutCustomizerSettings : ScriptableObject
{
    public float markSizeMargin = 10;
    public Color markSelectedColor = Color.green;

    private static bl_LayoutCustomizerSettings m_Data;
    public static bl_LayoutCustomizerSettings Instance
    {
        get
        {
            if (m_Data == null)
            {
                m_Data = Resources.Load("LayoutCustomizerSettings", typeof(bl_LayoutCustomizerSettings)) as bl_LayoutCustomizerSettings;
            }
            return m_Data;
        }
    }
}