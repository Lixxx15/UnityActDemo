using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Tools
{
    public class PhoneShake:MonoBehaviour
    {
        
        public void Shake(int price)
        {
            p = price;
            StartCoroutine(ShakeTimer());
        }

        private int p;
        private IEnumerator ShakeTimer()
        {
            while (p > 0)
            {
                yield return new WaitForSeconds(0.5f);
                p--;
                Handheld.Vibrate();
                Debug.Log("Shake");
            }

        }
    }
}
