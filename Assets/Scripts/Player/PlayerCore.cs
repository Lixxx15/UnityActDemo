using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerCore : MonoBehaviour
    {
        public Enemy.EnemyCore _NowEnemy;

        public void MoveToNowEnemy()
        {
            transform.position = _NowEnemy._Front.position;
            transform.forward = _NowEnemy._Front.forward;
        }
    }
}