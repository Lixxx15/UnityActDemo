
/*************************************************************************
 *  Copyright © 2018-2020 LiXiang. All rights reserved.
 *------------------------------------------------------------------------
 *  File（文件名）       :  MoveModule.cs
 *  Author（作者）       :  李想
 *  Date（创建日期）     :  
 *  Description（说明）  :  移动模组
 *************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LX
{
    public class MoveModule : MonoBehaviour
    {
        public bool _RandomSpeed = false;//速度波动
        public float _Speed;//移动速度
        public UnityEvent onGetToTarget;//到达目标监听

        private Queue<Transform> _PathTarget = new Queue<Transform>();
        private bool _Pause = false;//暂停
        private bool _IsOpen = false;//是否打开
        private float _A;//加速度

        #region 控制部分
        /// <summary>
        /// 打开移动功能
        /// </summary>
        /// <param name="paths">路径队列</param>
        public void Open(Queue<Transform> paths)
        {
            if (_IsOpen)
            {
                _PathTarget.Clear();
            }
            _PathTarget = paths;
            _IsOpen = true;
        }
        /// <summary>
        /// 打开移动功能
        /// </summary>
        /// <param name="paths">路径链表</param>
        public void Open(List<Transform> paths)
        {
            for (int i = 0; i < paths.Count; i++)
            {
                _PathTarget.Enqueue(paths[i]);
            }
            _IsOpen = true;
        }
        /// <summary>
        /// 打开移动功能
        /// </summary>
        /// <param name="target">目标点</param>
        public void Open(Transform target)
        {
            if (_IsOpen)
            {
                _PathTarget.Clear();
            }
            _PathTarget.Enqueue(target);
            _IsOpen = true;
        }
        /// <summary>
        /// 暂停移动
        /// </summary>
        public void Pause()
        {
            _Pause = true;
        }
        /// <summary>
        /// 继续
        /// </summary>
        public void Continue()
        {
            _Pause = false;
        }
        /// <summary>
        /// 停止移动 (会触发到达目标事件)
        /// </summary>
        public void Stop()
        {
            _PathTarget.Clear();
        }
        #endregion

        private void Update()
        {
            if (!_IsOpen)
            {
                return;
            }
            if (_PathTarget.Count > 0)
            {
                MoveToWay(transform, _PathTarget.Peek(), _Speed);
            }
            else
            {
                _IsOpen = false;
                onGetToTarget.Invoke();
            }
        }
        private void MoveToWay(Transform obj, Transform target, float speed)
        {
            if (_Pause)
            {
                speed = 0;
            }
            if (_RandomSpeed)
            {
                speed = SpeedWave(speed);
            }
            speed = UnityEngine.Random.Range(speed - 0.1f, speed + 0.1f);
            Vector3 p = new Vector3(target.position.x, obj.position.y, target.position.z);
            obj.position = Vector3.MoveTowards(obj.position, p, speed * Time.deltaTime);
            if (Vector3.Distance(p, obj.position) < 0.1f)
            {
                //到达target
                _PathTarget.Dequeue();
            }
        }

        private int _SustainNum = 0;
        private float SpeedWave(float speed)
        {
            if (_SustainNum <= 0)
            {
                float a = UnityEngine.Random.Range(-0.1f, 0.1f);
                _A = a;
                _SustainNum = UnityEngine.Random.Range(1, 20);
            }
            _SustainNum--;
            speed += _A;
            return speed;
        }
    }
}
