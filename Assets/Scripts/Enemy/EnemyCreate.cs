using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyCreate:MonoBehaviour
    {
        public Transform _Parent;
        public Transform _Target;
        private string _PrefabPath = "Enemy";

        private List<EnemyCore> _Enemies = new List<EnemyCore>();
        private bool IsOpen;

        
        public void Open()
        {
            IsOpen = true;
            StartCoroutine(C());
        }
        public void Close()
        {
            IsOpen = false;
            for (int i = 0; i < _Enemies.Count; i++)
            {
                _Enemies[i]._Move.Stop();
            }
            StopCoroutine(C());
        }
        private void Create()
        {
            EnemyCore enemy = Tools.UnityObjectPool.Instance.GetObject<EnemyCore>(_PrefabPath, _Parent);
            _Enemies.Add(enemy);
            enemy._Move.Open(_Target);
        }

        private IEnumerator C()
        {
            while (IsOpen)
            {
                Create();
                yield return new WaitForSeconds(30f);
            }
        }

    }
}
