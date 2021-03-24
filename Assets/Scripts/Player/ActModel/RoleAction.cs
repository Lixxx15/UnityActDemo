using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class RoleAction:MonoBehaviour
    {
        public string _AnimaClipName;
        public string _AnimatorTrigger;
        public Animator _Animator;
        public List<AnimeEvents> _Events;
        public List<ConditionEvents> _ConEvents;

        private int _AnimaNameHash; 
        private void Awake()
        {
            _AnimaNameHash = Animator.StringToHash(_AnimaClipName);
        }

        private bool Play()
        {
            for (int i = 0; i < _ConEvents.Count; i++)
            {
                if (_ConEvents[i]._Conditon._Accomplish)
                {
                    _ConEvents[i].onFinish.Invoke();
                    return false;
                }
            }
            return true;
        }

        private void FixedUpdate()
        {
            if (AnimatorCore._Info.shortNameHash == _AnimaNameHash)
            {
                float sc = AnimatorCore._Info.normalizedTime;
                for (int i = 0; i < _Events.Count; i++)
                {
                    if (sc > _Events[i]._OpenPoint && sc < _Events[i]._ClosePoint && !_Events[i]._IsOpen)
                    {
                        _Events[i]._PlayEvent.Invoke();
                        _Events[i]._IsOpen = true;
                    }
                    if (sc > _Events[i]._ClosePoint && _Events[i]._IsOpen)
                    {
                        _Events[i]._StopEvent.Invoke();
                        _Events[i]._IsOpen = false;
                    }
                }
            }
        }

        public void PlayAnima()
        {
            if (Play())
            {
                _Animator.SetTrigger(_AnimatorTrigger);
            }
        }
    }
}
