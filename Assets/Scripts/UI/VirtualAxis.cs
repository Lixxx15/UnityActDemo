using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    public class VirtualAxis : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Serializable]
        public class DragAxis : UnityEvent<float, float> { }

        public float _Range = 50f;

        public DragAxis _OnDragAxis;
        //public UnityEvent _OnDragEnd;

        private RectTransform rectTransform;
        private bool isDraging;
        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }
        public void OnDrag(PointerEventData eventData)
        {
            Vector3 pos;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
            rectTransform.position = pos;
            Vector3 dir = rectTransform.localPosition;
            if (dir.magnitude > _Range)
            {
                rectTransform.localPosition = dir.normalized * _Range;
            }
            _OnDragAxis.Invoke(dir.normalized.x, dir.normalized.y);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            isDraging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDraging = false;
            rectTransform.localPosition = Vector3.zero;
            _OnDragAxis.Invoke(0,0);
        }
    }
}
