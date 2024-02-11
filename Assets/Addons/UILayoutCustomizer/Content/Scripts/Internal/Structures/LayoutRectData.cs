using System;
using System.Collections.Generic;
using UnityEngine;

namespace MFPS.Addon.LayoutCustomizer
{
    [Serializable]
    public class LayoutRectData 
    {
        public Vector2 AnchorPosition;
        public Vector2 SizeDelta;
        public Vector2 AnchorMin;
        public Vector2 AnchorMax;
        public Vector2 Pivot;
        public Vector3 Rotation;
        public Vector3 Scale;
        public float Opacity = 1;

        public LayoutRectData()
        {
            Scale = Vector3.one;
        }

        public LayoutRectData(RectTransform rect)
        {
            Fetch(rect);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        public void Fetch(RectTransform rect)
        {
            if (rect == null) return;

            this.AnchorPosition = rect.anchoredPosition;
            this.SizeDelta = rect.sizeDelta;
            this.AnchorMin = rect.anchorMin;
            this.AnchorMax = rect.anchorMax;
            this.Pivot = rect.pivot;
            this.Rotation = rect.eulerAngles;
            this.Scale = rect.localScale;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        public void Apply(RectTransform rect)
        {
            if (rect == null) return;

            rect.anchorMin = this.AnchorMin;
            rect.anchorMax = this.AnchorMax;
            rect.pivot = this.Pivot;
            rect.eulerAngles = this.Rotation;
            rect.localScale = this.Scale;
            rect.anchoredPosition = this.AnchorPosition;
            rect.sizeDelta = this.SizeDelta;
        }
    }
}