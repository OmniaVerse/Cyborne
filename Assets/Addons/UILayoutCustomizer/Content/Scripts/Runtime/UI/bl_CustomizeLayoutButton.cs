using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFPS.Addon.LayoutCustomizer
{
    public class bl_CustomizeLayoutButton : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        public void OnClick()
        {
            bl_LayoutCustomizer.Instance.ActiveCustomizer();
        }
    }
}