using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager:MonoBehaviour
    {
        public ClickBtn _LightBtn;
        public ClickBtn _BaDaoBtn;
        public ClickBtn _DashBtn;
        public ClickBtn _StrongBtn;
        public HitCount _HitCount;
        public Text _Live;
        public Text _KillNum;
        public Enemy.EnemyCreate _Create;
        public int Hp;
        public int KillNum;
        public Animation _OverAnimation;
        public static UIManager Instance;
        private void Awake()
        {
            Instance = this;
            _Live.text = Hp.ToString();
            KillNum = 0;
            _KillNum.text = KillNum.ToString();
        }

        public void HomeHit()
        {
            Hp--;
            _Live.text = Hp.ToString();
            if (Hp == 0)
            {
                _Create.Close();
                GameOver();
            }
        }

        public void Kill()
        {
            KillNum++;
            _KillNum.text = KillNum.ToString();
        }

        public void GameOver()
        {
            _OverAnimation.gameObject.SetActive(true);
            _OverAnimation.Play();
        }

        public void Reload()
        {
            string na = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            UnityEngine.SceneManagement.SceneManager.LoadScene(na);
        }
    }
}
