using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFPS.Addon.LayoutCustomizer
{
    public class bl_LayoutCustomizerMFPS : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        public void OnCustomizerClose()
        {
            if (bl_LayoutCustomizer.Instance != null)
            {
                bl_GameInput.InputFocus = MFPSInputFocus.Player;
                bl_UIReferences.Instance.addonReferences[0].GetComponent<Canvas>().enabled = true;
#if MFPSM
                if (bl_TouchHelper.Instance != null)
                {
                    var mc = bl_TouchHelper.Instance.GetComponentInParent<Canvas>();
                    if (mc != null)
                    {
                        mc.enabled = false;
                    }
                }
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnCustomizerOpen()
        {
            bl_UtilityHelper.LockCursor(false);
            if (bl_LayoutCustomizer.Instance != null)
            {
                bl_GameInput.InputFocus = MFPSInputFocus.Interface;
                bl_UIReferences.Instance.addonReferences[0].GetComponent<Canvas>().enabled = false;
#if MFPSM
                if (bl_TouchHelper.Instance != null)
                {
                    var mc = bl_TouchHelper.Instance.GetComponentInParent<Canvas>();
                    if (mc != null)
                    {
                        mc.enabled = true;
                    }
                }
#endif
            }
        }
    }
}