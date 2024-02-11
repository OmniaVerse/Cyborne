using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFPS.Addon.LayoutCustomizer
{
    public class bl_LayoutBase : MonoBehaviour
    {

        private LayoutRectData m_thisRect;
        public LayoutRectData ThisRectData
        {
            get
            {
                if (m_thisRect == null) m_thisRect = new LayoutRectData(ThisRectTransform);
                return m_thisRect;
            }set => m_thisRect = value;
        }


        private RectTransform m_rectTransform;
        public RectTransform ThisRectTransform
        {
            get
            {
                if (m_rectTransform == null) m_rectTransform = (RectTransform)transform;
                return m_rectTransform;
            }
        }

        private Canvas m_canvas;
        public Canvas ParentCanvas
        {
            get
            {
                if (m_canvas == null) m_canvas = transform.GetComponentInParent<Canvas>();
                return m_canvas;
            }
        }
    }
}