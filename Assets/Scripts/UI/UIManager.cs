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

        public static UIManager Instance;
        private void Awake()
        {
            Instance = this;
        }

    }
}
