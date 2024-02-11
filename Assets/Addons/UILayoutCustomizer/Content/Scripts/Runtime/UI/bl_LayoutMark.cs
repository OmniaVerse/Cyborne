using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MFPS.Addon.LayoutCustomizer
{
    public class bl_LayoutMark : bl_LayoutBase
    {
        #region Public members
        [SerializeField] private RectTransform m_layoutTarget = null;
        public RectTransform TargetModifier
        {
            get => m_layoutTarget;
            set => m_layoutTarget = value;
        }
        [SerializeField] private List<GameObject> selectedUI = null;
        [SerializeField] private Graphic[] coloredUI;
        #endregion

        #region Public properties
        public bl_ModifiableLayout LayoutEntity { get; set; }
        public Canvas TargetCanvas => LayoutEntity.ParentCanvas;
        public LayoutRectData TargetRectData
        {
            get => LayoutEntity.ThisRectData;
            set => LayoutEntity.ThisRectData = value;
        }
        #endregion

        private float scaleDifference = 1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Init(bl_ModifiableLayout entity)
        {
            if (entity == null || bl_LayoutCustomizer.Instance == null) return;

            LayoutEntity = entity;
            TargetModifier = (RectTransform)entity.transform;
            gameObject.name = $"{TargetModifier.name} [Mark]";
            scaleDifference = TargetCanvas.scaleFactor / bl_LayoutCustomizer.Instance.ParentCanvas.scaleFactor;
        }

        /// <summary>
        /// Move the layout mark and the target layout
        /// </summary>
        public void Move(Vector2 position)
        {
            var oldPos = ThisRectTransform.anchoredPosition;
            if (LayoutEntity.ParentCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {             
                ThisRectTransform.anchoredPosition += position / ParentCanvas.scaleFactor;
                var diff = ThisRectTransform.anchoredPosition - oldPos;
                TargetModifier.anchoredPosition += (diff / scaleDifference);
            }
            else
            {
                ThisRectTransform.anchoredPosition += position;
                TargetModifier.anchoredPosition += position;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Selected()
        {
            bl_LayoutCustomizer.Instance?.OnMarkSelected(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public void FethRect()
        {
            ThisRectData.Fetch(ThisRectTransform);
            TargetRectData.Fetch(TargetModifier);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ApplyRect()
        {
            ThisRectData.Apply(ThisRectTransform);
            TargetRectData.Apply(TargetModifier);
        }

        /// <summary>
        /// Position the layout mark in the target layout
        /// </summary>
        public void CalculateLayoutMark()
        {
            var oldParent = ThisRectTransform.parent;
            ThisRectTransform.SetParent(TargetModifier.parent, false);
            bl_LayoutCustomizerUtility.CopyRectProperties(TargetModifier, ThisRectTransform);
            bl_LayoutCustomizerUtility.SetRectTransformPivot(ThisRectTransform, Vector2.one * 0.5f);
            ThisRectTransform.sizeDelta += Vector2.one * bl_LayoutCustomizerSettings.Instance.markSizeMargin;

            if (LayoutEntity.ParentCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                ThisRectTransform.SetParent(oldParent, true);

                if (ThisRectTransform.localScale != Vector3.one)
                {
                    ThisRectTransform.sizeDelta *= ThisRectTransform.localScale.x;
                    ThisRectTransform.localScale = Vector3.one;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        public void ChangeColor(Color color)
        {
            foreach (var item in coloredUI)
            {
                if (item == null) continue;
                item.CrossFadeColor(color, 0.1f, true, true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="active"></param>
        public void SetActiveSectionUI(bool active)
        {
            selectedUI.ForEach(x => { if (x != null) x.SetActive(active); });
            ChangeColor(active ? bl_LayoutCustomizerSettings.Instance.markSelectedColor : Color.white);
        }
      
    }
}