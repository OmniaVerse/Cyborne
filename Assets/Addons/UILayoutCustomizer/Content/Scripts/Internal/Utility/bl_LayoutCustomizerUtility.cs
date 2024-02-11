using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFPS.Addon.LayoutCustomizer
{
    public static class bl_LayoutCustomizerUtility
    {
        /// <summary>
        /// 
        /// </summary>
        public static void CopyRectProperties(RectTransform source, RectTransform target)
        {
            target.pivot = source.pivot;
            target.anchorMin = source.anchorMin;
            target.anchorMax = source.anchorMax;
            target.anchoredPosition = source.anchoredPosition;
            target.rotation = source.rotation;
            target.sizeDelta = source.sizeDelta;
        }

        public static void SetRectTransformPivot(RectTransform rectTransform, Vector2 pivot)
        {
            if (rectTransform == null) return;

            Vector2 size = rectTransform.rect.size;
            Vector2 deltaPivot = rectTransform.pivot - pivot;
            Vector3 deltaPosition = new Vector3(deltaPivot.x * size.x, deltaPivot.y * size.y);
            rectTransform.pivot = pivot;
            rectTransform.localPosition -= deltaPosition;
        }
    }
}