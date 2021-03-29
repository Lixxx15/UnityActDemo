using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HitCount:MonoBehaviour
    {
        public Text _HitText;
        public Slider _Slider;
        public Animation _HitAnima;

        private int _HitConst;
        private int _Time = 1000;
        private int _NowTime;

        private void Start()
        {
            _Slider.maxValue = _Time;
            _HitText.gameObject.SetActive(false);
            _Slider.gameObject.SetActive(false);
        }
        public void Hit()
        {
            _NowTime = _Time;
            _HitConst++;
            if (_HitConst > 1)
            {
                _HitText.gameObject.SetActive(true);
                _Slider.gameObject.SetActive(true);
                _HitText.text = _HitConst.ToString();
            }
        }

        private void Update()
        {
            if (_NowTime > 0)
            {
                _NowTime--;
                _Slider.value = _NowTime;
                if (_NowTime == 0)
                {
                    _HitConst = 0;
                    _HitText.gameObject.SetActive(false);
                    _Slider.gameObject.SetActive(false);
                }
            }
        }

    }
}
