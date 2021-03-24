using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Weapon:MonoBehaviour
    {
        public PlayerCore _Core;
        public RoleState _WeaponState;

        public void SetState(int state)
        {
            _WeaponState = (RoleState)state;
        }

        public void SetNowEnemy(Enemy.EnemyCore e)
        {
            _Core._NowEnemy = e;
        }
    }
}
