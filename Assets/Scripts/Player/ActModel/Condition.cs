using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    //条件
    public class Condition : MonoBehaviour
    {
        [Tooltip("达成条件持续时间")]
        public float _ConditonTime;
        public bool _Accomplish { get; private set; }
        public UnityEvent onAccTrue;
        public UnityEvent onAccFalse;

        public void SetAccomplish()
        {
            _Accomplish = true;
            onAccTrue.Invoke();
            StartCoroutine("SetFalse");
        }

        public void ResetAccomplish()
        {
            _Accomplish = false;
            onAccFalse.Invoke();
            StopCoroutine("SetFalse");
        }

        private IEnumerator SetFalse()
        {
            yield return new WaitForSeconds(_ConditonTime);
            _Accomplish = false;
            onAccFalse.Invoke();
        }
    }
}
