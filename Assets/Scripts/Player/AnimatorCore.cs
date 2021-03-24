using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AnimatorCore : MonoBehaviour
    {
        public Animator _Animator;
        public static AnimatorStateInfo _Info;

        private void FixedUpdate()
        {
            _Info = _Animator.GetCurrentAnimatorStateInfo(0);
        }
    }
}
