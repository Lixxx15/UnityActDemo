using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    /// <summary>
    /// 计数器
    /// </summary>
    public class Counter:MonoBehaviour
    {
        public int _Now;
        public bool _CounterLock = false;
        public int _ResetTime;
        public int _NowTime;
        public List<UnityEvent> _Actions;

        public void ResetNow()
        {
            _Now = 0;
        }

        public void Add()
        {
            if (_CounterLock)
            {
                return;
            }
            _NowTime = _ResetTime;
            if (_Now >= _Actions.Count)
            {
                _Now = 0;
            }
            _Actions[_Now].Invoke();
            _Now++;
        }

        public void Lock()
        {
            _CounterLock = true;
        }

        public void Unlock()
        {
            _CounterLock = false;
        }
        private void Update()
        {
            if (_Now != 0)
            {
                _NowTime--;
                if (_NowTime == 0)
                {
                    _Now = 0;
                }
            }
        }

    }
}
