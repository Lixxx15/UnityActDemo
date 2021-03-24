using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// 攻击吸附
    /// </summary>
    public class AttackAdsorb : MonoBehaviour
    {
        public float _AngleRange;
        public string _TargetTag = "Enemy";
        public Transform _Rotation;
        public List<Transform> _Enemies = new List<Transform>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == _TargetTag)
            {
                _Enemies.Add(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_Enemies.Contains(other.transform))
            {
                _Enemies.Remove(other.transform);
            }
        }

        public void FindTarget()
        {
            float minAngle = 180f;
            Transform target = null;
            Vector3 front = Vector3.zero;
            for (int i = 0; i < _Enemies.Count; i++)
            {
                Vector3 dir = Vector3.ProjectOnPlane(_Enemies[i].position - _Rotation.position, _Rotation.up);
                float angle = Mathf.Abs(Vector3.Angle(_Rotation.forward, dir));
                if (angle < minAngle)
                {
                    minAngle = angle;
                    target = _Enemies[i];
                    front = dir;
                }
            }
            if (target != null)
            {

                _Rotation.forward = front;
            }
        }

    }
}
