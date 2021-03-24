/*************************************************************************
 *  Copyright © 2018-2020 LX. All rights reserved.
 *------------------------------------------------------------------------
 *  File（文件名）       :  UnityObjectPool.cs
 *  Author（作者）       :  李想
 *  Date（创建日期）     :  
 *  Description（说明）  :  对象池
 *************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;


namespace LX
{
    public class UnityObjectPool:Tools.MonoSingleton<UnityObjectPool>
    {

        private Dictionary<string, GameObject> ObjectDic = new Dictionary<string, GameObject>();

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        /// <summary>
        /// 加载物体
        /// </summary>
        /// <param name="goPath">物体路径</param>
        /// <param name="parent">设置父物体</param>
        /// <returns></returns>
        public GameObject GetObject(string goPath, Transform parent)
        {
            GameObject GOList;
            if (!ObjectDic.ContainsKey(goPath))
            {
                CreateNewPool(goPath);
                GameObject go = Instantiate(Resources.Load<GameObject>(goPath));
                go.transform.SetParent(parent);
                go.transform.localScale = Vector3.one;
                GOList = go;
            }
            else
            {
                Transform pool = ObjectDic[goPath].transform;
                if (pool.childCount > 0)
                {
                    Transform item = pool.GetChild(0);
                    item.gameObject.SetActive(true);
                    item.SetParent(parent);
                    GOList = item.gameObject;
                }
                else
                {
                    GameObject go = Instantiate(Resources.Load<GameObject>(goPath));
                    go.transform.SetParent(parent);
                    GOList = go;
                }
            }
            return GOList;
        }
        /// <summary>
        /// 回收物体
        /// </summary>
        /// <param name="GOPath"></param>
        /// <param name="go"></param>
        public void RecycleGo(string GOPath, GameObject go)
        {
            go.transform.SetParent(ObjectDic[GOPath].transform);
            go.SetActive(false);
        }
        /// <summary>
        /// 回收list中的所有物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="goList"></param>
        /// <param name="Path"></param>
        public void RecycleList<T>(string Path, List<T> goList) where T:MonoBehaviour
        {
            int count = goList.Count;
            for (int i = 0; i < count; i++)
            {
                RecycleGo(Path, goList[0].gameObject);
                goList.RemoveAt(0);
            }
        }
        /// <summary>
        /// 加载物体并获取其挂载的某组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="goPath"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public T GetObject<T>(string goPath, Transform parent) where T : MonoBehaviour
        {
            GameObject go = GetObject(goPath, parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            T t = go.GetComponent<T>();
            return t;
        }
        private void CreateNewPool(string GOName)
        {
            GameObject Pool = new GameObject(GOName + "Pool");
            Pool.transform.SetParent(transform);
            ObjectDic[GOName] = Pool;
        }
        
    }
}
