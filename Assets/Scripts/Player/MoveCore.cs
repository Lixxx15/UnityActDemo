using Const;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class MoveCore:MonoBehaviour
    {
        public float _RotateSpeed = 6;
        public Transform _Camera;
        public Animator _Animator;
        private Vector3 m_V3;
        private float _Speed = 0;
        private bool _TurnLock = false;
        private bool _Stop = false;

        private void Update()
        {
            if (_Stop && _Speed > 0)
            {
                _Speed -= 0.02f;
                _Speed = Mathf.Max(_Speed, 0);
                _Animator.SetFloat(AnimatorConst.Speed, _Speed);
            }
        }

        public void MoveWithFront(float h, float v)
        {
            Move(h, v, new Vector3(_Camera.forward.x, 0, _Camera.forward.z));
        }

        public void Move(float h, float v, Vector3 frontDir)
        {
            AnimatorStateInfo info = _Animator.GetCurrentAnimatorStateInfo(0);

            if (info.shortNameHash == AnimatorConst.Turn_180 && info.normalizedTime > 0.8f)
            {
                //transform.forward = m_V3;
                _TurnLock = false;
            }
            else if (info.shortNameHash != AnimatorConst.Turn_180)
            {
                _TurnLock = false;
            }
            if (_TurnLock)
            {
                return;
            }
            if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
            {
                _Stop = false;
                _Speed += 0.1f;
                _Speed = Mathf.Min(_Speed, 2);
                _Animator.SetFloat(AnimatorConst.Speed, _Speed);

                //指令方向
                m_V3 = new Vector3(h, 0, v);
                //镜头方向
                Vector3 dir = frontDir;
                //指令方向与正方向夹角
                float angle = Vector3.Angle(Vector3.forward, m_V3);
                //移动方向
                Vector3 moveDir;
                if (h < 0)
                {
                    moveDir = Quaternion.AngleAxis(-angle, transform.up) * dir;
                }
                else
                {
                    moveDir = Quaternion.AngleAxis(angle, transform.up) * dir;
                }

                if (Vector3.Angle(transform.forward, moveDir) > 179.9f)
                {
                    _Animator.SetTrigger(AnimatorConst.Turn180);
                    //transform.forward = m_V3;
                    _TurnLock = true;
                }
                else
                {
                    Quaternion targetRot = Quaternion.FromToRotation(transform.forward, moveDir) * transform.rotation;
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _RotateSpeed * Time.deltaTime);
                }
            }
            else
            {
                _Stop = true;
            }
        }

    }
}
