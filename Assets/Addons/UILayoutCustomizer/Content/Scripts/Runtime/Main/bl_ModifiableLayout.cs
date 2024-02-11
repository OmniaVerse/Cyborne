using System;
using UnityEngine;

namespace MFPS.Addon.LayoutCustomizer
{
    public class bl_ModifiableLayout : bl_LayoutBase
    {
        [HideInInspector] public string layoutKey = "";

        public bool allowModifySize = true;
        public Vector2 allowedSizeRange = new Vector2(0.5f, 1.7f);
        public bool allowModifyOpacity = true;
        public Vector2 allowedOpacity = new Vector2(0.02f, 1);

        private bl_LayoutMark mark;

        public bl_LayoutMark LayoutMark
        {
            get => mark;
            set => mark = value;
        }

        private LayoutRectData originalRectData;

        /// <summary>
        ///
        /// </summary>
        private void Awake()
        {
            ThisRectData = new LayoutRectData(ThisRectTransform);
            originalRectData = new LayoutRectData(ThisRectTransform);

            bl_LayoutCustomizer.AddLayoutMarkInstance(this);
            if (bl_LayoutCustomizer.Instance != null && bl_LayoutCustomizer.Instance.IsSetupLoaded)
            {
                Init();
            }
        }

        /// <summary>
        /// Called at the start of the scene or when the Canvas that contains the bl_LayoutCustomizer.cs is awake
        /// this is called after the saved data is loaded.
        /// </summary>
        public void Init()
        {
            LayoutRectData data;
            //if there's data saved for this layout
            if (bl_LayoutCustomizer.LoadLayoutDataFor(this, out data))
            {
                ThisRectData = data;
                ThisRectData.Apply(ThisRectTransform);
                if (allowModifyOpacity) CanvasAlpha.alpha = data.Opacity;
            }
        }

        /// <summary>
        /// Called when the customizer is invoked/active
        /// What this does is create a mark UI for this layout.
        /// </summary>
        public void Prepare()
        {
            if (mark != null || bl_LayoutCustomizer.Instance == null) return;

            var go = Instantiate(bl_LayoutCustomizer.Instance.markUITemplate.gameObject);
            go.transform.SetParent(bl_LayoutCustomizer.Instance.marksPanel, false);
            mark = go.GetComponent<bl_LayoutMark>();
            mark.Init(this);
            go.SetActive(false);
        }

        /// <summary>
        /// Get the current rectTransform position
        /// </summary>
        public void FetchRectTransform()
        {
            mark?.FethRect();
            if (allowModifyOpacity) ThisRectData.Opacity = CanvasAlpha.alpha;
        }

        /// <summary>
        /// Make the UI mark centered in this layout
        /// </summary>
        public void PositioneMark()
        {
            mark?.CalculateLayoutMark();
        }

        /// <summary>
        ///
        /// </summary>
        public void ResetToOriginal()
        {
            originalRectData.Apply(ThisRectTransform);
            mark?.CalculateLayoutMark();
            if (allowModifyOpacity) CanvasAlpha.alpha = 1;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="factor"></param>
        public void ModifySizeByFactor(float factor)
        {
            var currentSize = originalRectData.SizeDelta;
            currentSize *= factor;

            var diff = currentSize - ThisRectTransform.sizeDelta;
            bl_LayoutCustomizerUtility.SetRectTransformPivot(ThisRectTransform, Vector3.one * 0.5f);
            ThisRectTransform.sizeDelta = currentSize;
            float factorRatio = ParentCanvas.scaleFactor / mark.ParentCanvas.scaleFactor;
            mark.ThisRectTransform.sizeDelta += (diff * factorRatio);
            bl_LayoutCustomizerUtility.SetRectTransformPivot(ThisRectTransform, originalRectData.Pivot);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="active"></param>
        public void SetActiveMark(bool active)
        {
            if (LayoutMark != null)
            {
                LayoutMark.gameObject.SetActive(active);
                if (active) LayoutMark.CalculateLayoutMark();
            }
        }

        /// <summary>
        ///
        /// </summary>
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(layoutKey))
                layoutKey = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public float GetSizeFactor() => ThisRectTransform.sizeDelta.x / originalRectData.SizeDelta.x;

        private CanvasGroup m_canvasAlpha;

        public CanvasGroup CanvasAlpha
        {
            get
            {
                if (m_canvasAlpha == null) m_canvasAlpha = GetComponent<CanvasGroup>();
                if (m_canvasAlpha == null)
                {
                    m_canvasAlpha = gameObject.AddComponent<CanvasGroup>();
                    m_canvasAlpha.alpha = allowedOpacity.y;
                }
                return m_canvasAlpha;
            }
        }
    }
}