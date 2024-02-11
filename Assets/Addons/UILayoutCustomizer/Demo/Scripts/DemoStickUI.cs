using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MFPS.Addon.LayoutCustomizer
{
    public class DemoStickUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public Camera gameCamera;

        private RectTransform m_rectTransform;
        private Vector2 defaultPos;

        void Start()
        {
            m_rectTransform = (RectTransform)transform;
            GetDefaultPosition();
        }

        void OnEnable()
        {
            // Add a local callback that will be called when the player made changes to the UI
            bl_LayoutCustomizer.Instance.onChangesApplied += OnLayoutChangesMade;
        }

        void OnDisable()
        {
           if(bl_LayoutCustomizer.Instance != null) bl_LayoutCustomizer.Instance.onChangesApplied -= OnLayoutChangesMade;
        }

        void OnLayoutChangesMade()
        {
            // In this callback when make sure to update the default information (if any) of this UI element
            GetDefaultPosition();
        }

        public void GetDefaultPosition()
        {
            defaultPos = m_rectTransform.anchoredPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            m_rectTransform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            m_rectTransform.anchoredPosition = defaultPos;
        }
    }
}