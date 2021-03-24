using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Player;

namespace Enemy
{
    public class EnemyCore : MonoBehaviour
    {
        public float Duration;
        public float Strength;
        public ParticleSystem _HitP;
        public Transform _Front;
        public RoleStateModel _SModel;
        private Rigidbody _R;

        private void Start()
        {
            _HitP.Stop();
            _R = GetComponent<Rigidbody>();
        }

        #region Trigger
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Vector3 point = other.ClosestPointOnBounds(transform.position);
                _HitP.transform.position = point;
                Vector3 nor = point - transform.position;
                _HitP.transform.forward = nor;
                _HitP.Play();
                Vector3 force = Vector3.ProjectOnPlane((transform.position - other.transform.position), transform.up).normalized;
                transform.forward = force;
                Weapon w = other.GetComponent<Weapon>();
                w.SetNowEnemy(this);
                switch (w._WeaponState)
                {
                    case RoleState.None:
                        _R.AddForce(force * 100);
                        break;
                    case RoleState.Blow:
                        _SModel.Blow(force, 3);
                        break;
                    default:
                        break;
                }

            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                ObjectShake(Duration, Strength);
            }
        }

        #endregion

        #region Shake
        public void ObjectShake(float duration, float strength)
        {
            if (!isShake)
                StartCoroutine(Shake(duration, strength));
        }

        private bool isShake;

        private IEnumerator Shake(float duration, float strength)
        {
            isShake = true;
            Vector3 startPosition = transform.position;

            while (duration > 0)
            {
                transform.position = UnityEngine.Random.insideUnitSphere * strength + startPosition;
                duration -= Time.deltaTime;
                yield return null;
            }
            transform.position = startPosition;
            isShake = false;
        }
        #endregion
    }
}