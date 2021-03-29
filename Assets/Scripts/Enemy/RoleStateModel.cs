using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class RoleStateModel : MonoBehaviour
    {
        public RoleState _NowState = RoleState.None; 
        private Rigidbody _R;
        private void Start()
        {
            _R = gameObject.GetComponent<Rigidbody>();
        }

        /// <summary>
        /// 吹飞
        /// </summary>
        /// <param name="dir">方向</param>
        /// <param name="duration">状态持续时间</param>
        public void Blow(Vector3 dir, float duration)
        {
            _R.AddForce(dir * 300);
            UI.UIManager.Instance._BaDaoBtn.Flicker();
            _NowState = RoleState.Blow;
            StartCoroutine(StateDuration(duration));
        }

        /// <summary>
        /// 持续时间计时
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private IEnumerator StateDuration(float time)
        {
            yield return new WaitForSeconds(time);
            switch (_NowState)
            {
                case RoleState.None:
                    break;
                case RoleState.Blow:
                    UI.UIManager.Instance._BaDaoBtn.StopFlicker();
                    break;
                default:
                    break;
            }
            _NowState = RoleState.None;
        }
    }
}
