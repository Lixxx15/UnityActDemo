using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    /// <summary>
    /// 继承MonoBehaviour的泛型单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T m_Instance = null;
        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;
                    if (m_Instance == null)
                    {
                        m_Instance = new GameObject("Singleton of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();
                        if (m_Instance == null)
                        {
                            Debug.LogError("Problem during the creation of " + typeof(T).ToString());
                        }
                    }
                }
                return m_Instance;
            }
        }

        private void Awake()
        {
            if (m_Instance == null)
            {
                m_Instance = this as T;
            }
            m_Instance.Init();
        }

        public virtual void Init()
        {

        }

        private void OnApplicationQuit()
        {
            m_Instance = null;
        }
    }
}
