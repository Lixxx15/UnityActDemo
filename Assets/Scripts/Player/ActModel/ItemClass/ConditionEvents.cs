using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [Serializable]
    public class ConditionEvents
    {
        public Condition _Conditon;
        public UnityEvent onFinish;
    }
}
