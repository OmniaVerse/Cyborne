using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MFPS.Addon.LayoutCustomizer
{
    public class bl_LayoutPropertiesWindow : MonoBehaviour
    {
        public float buttonMoveSteps = 2;

        public GameObject content;
        public GameObject propsWindow;
        public Slider opacitySlider;
        public Slider sizeSlider;
        public Image visibilityImg;
        public Sprite[] visibleIcons;

        private bl_ModifiableLayout currentLayout;
        private bool internalMod, internalOpaMod = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layout"></param>
        public void PrepareForLayout(bl_ModifiableLayout layout)
        {
            if (layout == null) return;
            internalMod = true;
            internalOpaMod = true;

            if (layout.allowModifyOpacity)
            {
                opacitySlider.minValue = layout.allowedOpacity.x;
                opacitySlider.maxValue = layout.allowedOpacity.y;
                opacitySlider.value = layout.CanvasAlpha.alpha;
                opacitySlider.gameObject.SetActive(true);
            }
            else opacitySlider.gameObject.SetActive(false);

            if (layout.allowModifySize)
            {
                sizeSlider.minValue = layout.allowedSizeRange.x;
                sizeSlider.maxValue = layout.allowedSizeRange.y;
                sizeSlider.value = layout.GetSizeFactor();
                sizeSlider.gameObject.SetActive(true);
            }
            else sizeSlider.gameObject.SetActive(false);

            currentLayout = layout;            
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnOpacityChange(float value)
        {
            if (currentLayout == null) return;
            if (internalOpaMod) { internalOpaMod = false; return; }

            currentLayout.CanvasAlpha.alpha = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnSizeChange(float value)
        {
            if (internalMod) { internalMod = false; return; }

            currentLayout?.ModifySizeByFactor(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        public void Move(int direction)
        {
            if (currentLayout == null) return;

            switch (direction)
            {
                case 1:
                    currentLayout.LayoutMark.Move(Vector2.right * buttonMoveSteps);
                    break;
                case -1:
                    currentLayout.LayoutMark.Move(Vector2.left * buttonMoveSteps);
                    break;
                case 2:
                    currentLayout.LayoutMark.Move(Vector2.up * buttonMoveSteps);
                    break;
                case -2:
                    currentLayout.LayoutMark.Move(Vector2.down * buttonMoveSteps);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void TogglePropsWindow()
        {
            var showing = propsWindow.activeSelf;
            showing = !showing;
            propsWindow.SetActive(showing);
            visibilityImg.sprite = showing ? visibleIcons[0] : visibleIcons[1];
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetLayout()
        {
            if (currentLayout == null) return;

            currentLayout.ResetToOriginal();
            PrepareForLayout(currentLayout);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Show()
        {
            content.SetActive(true);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Hide()
        {
            currentLayout = null;
            visibilityImg.sprite = visibleIcons[0];
            content.SetActive(false);    
        }
    }
}