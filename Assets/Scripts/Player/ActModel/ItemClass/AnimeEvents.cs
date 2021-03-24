using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [Serializable]
    public class AnimeEvents
    {
        [Range(0f, 1f)]
        [Tooltip("开启节点")]
        public float _OpenPoint;
        [Range(0f, 1f)]
        [Tooltip("关闭节点")]
        public float _ClosePoint = 0.8f;
        public bool _IsOpen;
        [Tooltip("执行事件")]
        public UnityEvent _PlayEvent;
        [Tooltip("停止事件")]
        public UnityEvent _StopEvent;

    }
}
