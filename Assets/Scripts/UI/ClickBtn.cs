using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

namespace UI
{
    public class ClickBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool interactable = true;
        [Range(0.1f, 10)]
        public float _HoldTime;

        public KeyCode _Key;

        public Animation _FlickerAnime;

        //点击事件
        public UnityEvent onBtnClick;
        //长按后抬起事件
        public UnityEvent onHoldBtn;
        //长按达到时长事件
        public UnityEvent onHoldingBtn;

        public UnityEvent onFlicker;
        public UnityEvent onStopFlicker;
        private bool isHold = false;
        private bool isUp = false;

        private void Update()
        {
#if !UNITY_ANDROID 
            if (_Key == KeyCode.None)
            {
                return;
            }
            if (Input.GetKeyDown(_Key))
            {
                Down();
            }
            if (Input.GetKeyUp(_Key))
            {
                Up();
            }
#endif
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!interactable)
            {
                return;
            }
            Down();
        }

        private void Down()
        {
            if (!interactable)
            {
                return;
            }
            isUp = false;
            isHold = false;
            StartCoroutine("Timer"); 
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!interactable)
            {
                return;
            }
            Up();
        }

        private void Up()
        {
            if (isHold)
            {
                onHoldBtn.Invoke();
            }
            else
            {
                onBtnClick.Invoke();
            }
            isUp = true;
            StopCoroutine("Timer");
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(_HoldTime);
            if (isUp)
            {
                isHold = false;
            }
            else
            {
                isHold = true;
                onHoldingBtn.Invoke();
            }
        }
        
        /// <summary>
        /// 闪烁
        /// </summary>
        public void Flicker()
        {
            _FlickerAnime.Play();
            onFlicker.Invoke();
        }
        /// <summary>
        /// 停止闪烁
        /// </summary>
        public void StopFlicker()
        {
            _FlickerAnime.Stop();
            _FlickerAnime.GetComponent<Image>().color = new Color(1,1,1,0);
            _FlickerAnime.transform.localScale = Vector3.one;
            onStopFlicker.Invoke();
        }
        //停用按钮
        public void Forbid()
        {
            interactable = false;
        }
        //启用
        public void StartUsing()
        {
            interactable = true;
        }
    }
}
