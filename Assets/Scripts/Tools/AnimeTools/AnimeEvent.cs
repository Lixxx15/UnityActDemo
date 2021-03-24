using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Tools
{
    /// <summary>
    /// 动画帧事件监听
    /// </summary>
    public class AnimeEvent : MonoBehaviour
    {
        [Serializable]
        public class EventClip
        {
            public string _Name;//事件名称
            public UnityEvent _Event;//事件
        }

        public List<EventClip> _AnimeEvents;
        /// <summary>
        /// 通过名称查找事件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private EventClip FindEventClip(string name)
        {
            for (int i = 0; i < _AnimeEvents.Count; i++)
            {
                if (name == _AnimeEvents[i]._Name)
                {
                    return _AnimeEvents[i];
                }
            }
            //如果不存在则添加新事件
            EventClip newClip = new EventClip
            {
                _Name = name,
                _Event = new UnityEvent(),
            };
            _AnimeEvents.Add(newClip);
            return newClip;
        }
        #region 执行（动画帧事件）
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="name">名称</param>
        public void DoEvent(string name)
        {
            FindEventClip(name)._Event?.Invoke();
        }
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="name">下标</param>
        public void DoEvent(int index)
        {
            _AnimeEvents[index]._Event?.Invoke();
        }
        #endregion

        #region 监听 增 删 改 查
        /// <summary>
        /// 注册监听
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="act">事件</param>
        public void InjectionEvent(string name, UnityAction act)
        {
            EventClip clip = FindEventClip(name);
            clip._Event.AddListener(act);
        }
        /// <summary>
        /// 注册监听
        /// </summary>
        /// <param name="index">事件下标</param>
        /// <param name="act">事件</param>
        public void InjectionEvent(UnityAction act)
        {
            EventClip clip = new EventClip();
            _AnimeEvents.Add(clip);
            clip._Name = _AnimeEvents.Count + "";
            clip._Event.AddListener(act);
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="act">事件</param>
        public void RemoveEvent(string name, UnityAction act)
        {
            EventClip clip = FindEventClip(name);
            clip._Event.RemoveListener(act);
        }
        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="index">事件名称</param>
        /// <param name="act">事件</param>
        public void RemoveEvent(int index, UnityAction act)
        {
            EventClip clip = _AnimeEvents[index];
            clip._Event.RemoveListener(act);
        }

        /// <summary>
        /// 修改监听
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="oldAct">原事件</param>
        /// <param name="newAct">新事件</param>
        public void ChangeEvent(string name,UnityAction oldAct,UnityAction newAct)
        {
            RemoveEvent(name, oldAct);
            InjectionEvent(name, newAct);
        }
        /// <summary>
        /// 修改监听
        /// </summary>
        /// <param name="index">下标</param>
        /// <param name="oldAct">原事件</param>
        /// <param name="newAct">新事件</param>
        public void ChangeEvent(int index, UnityAction oldAct, UnityAction newAct)
        {
            if (_AnimeEvents.Count <= index)
            {
                throw new Exception(
                    "index 超出 list 长度（list长度" + _AnimeEvents.Count + ")"
                    + "index = " + index
                    );
            }
            RemoveEvent(index, oldAct);
            _AnimeEvents[index]._Event.AddListener(newAct);
        }
        
        /// <summary>
        /// 查找监听
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public UnityEvent FindClip(string name)
        {
            return FindEventClip(name)._Event;
        }
        /// <summary>
        /// 查找监听
        /// </summary>
        /// <param name="index">下标</param>
        /// <returns></returns>
        public UnityEvent FindClip(int index)
        {
            return _AnimeEvents[index]._Event;
        }
         #endregion
    }
}
