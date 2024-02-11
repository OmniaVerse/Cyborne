using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MFPS.Addon.LayoutCustomizer
{
    public class bl_MarkInput : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        public bl_LayoutMark layoutMark;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData)
        {
            layoutMark?.Move(eventData.delta);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            layoutMark?.Selected();
        }
    }
}