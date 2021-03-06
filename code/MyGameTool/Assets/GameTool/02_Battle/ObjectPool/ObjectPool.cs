﻿/****************************************************
    功能：对象池管理工具
    
    PS：
    1、根据配置信息初始化对象池
    2、需要自己写好打开/回收对象的方法
    3、注意初始化

*****************************************************/

using System.Collections.Generic;
using UnityEngine;
public class ObjectPool : MonoBehaviour
{
    /// <summary>
    /// 按照key 保存 ObjectList集合
    /// </summary>
    private Dictionary<string, ObjectList> listDictionary;

    /// <summary>
    /// 挂载父类
    /// </summary>
    private Transform allRoot;

    /// <summary>
    /// 构造函数，实例化对象池
    /// </summary>
    /// <param name="configs">对象配置信息</param>
    public ObjectPool(params ObjectPoolConfig[] configs)
    {
        listDictionary = new Dictionary<string, ObjectList>();
        if(GameObject.Find("ObjPool_Clone") != null)
        {
            allRoot = GameObject.Find("ObjPool_Clone").transform;
        }
        else
        {
            allRoot = new GameObject("ObjPool_Clone").transform;
        }
        for (int i = 0; i < configs.Length; i++)
        {
            ObjectPoolConfig config = configs[i];
            GenerateObject(config);
        }
    }

    /// <summary>
    /// 根据配置生成对象
    /// </summary>
    /// <param name="config">配置信息</param>
    private void GenerateObject(ObjectPoolConfig config)
    {
        if (listDictionary == null) return;

        //配置信息
        string key = config.Key;
        int count = config.Count;
        GameObject target = config.Target;
        //放置位置
        GameObject root = new GameObject(key + "_Clone");
        root.transform.parent = allRoot;
        //生成对象
        ObjectList list = new ObjectList(root.transform, target);
        for (int i = 0; i < count; i++)
        {
            GameObject buff = GameObject.Instantiate(target, root.transform);
            buff.gameObject.SetActive(false);
            list.CloseList.Add(buff);
        }
        listDictionary.Add(key, list);
    }

    /// <summary>
    /// 打开一个对象
    /// </summary>
    /// <param name="key">对象字段</param>
    /// <returns></returns>
    public GameObject Open(string key, Transform root = null)
    {
        if (listDictionary == null) return null;
        if (!listDictionary.ContainsKey(key))
        {
            Debug.Log("不存在对应Key");
            return null;
        }
        ObjectList list = listDictionary[key];
        //集合空间足够时
        if (list.CloseList.Count > 0)
        {
            GameObject buff = list.CloseList[0];
            list.CloseList.RemoveAt(0);
            if (root != null)
                buff.transform.SetParent(root);
            buff.gameObject.SetActive(true);
            list.OpenList.Add(buff);
            return buff;
        }
        //集合空间不足处理
        else
        {
            GameObject clone = GameObject.Instantiate(list.Target, list.Root);
            // Debug.Log("空间不足，生成新列表：" + key + " , " + clone.name);
            if(root != null){
                clone.transform.SetParent(root);
            }
            else{
                clone.transform.SetParent(allRoot);
            }
            
            clone.gameObject.SetActive(true);
            list.OpenList.Add(clone);
            return clone;
        }
    }

    /// <summary>
    /// 关闭对象
    /// </summary>
    /// <param name="key">对象字段</param>
    /// <param name="target">对象</param>
    public void Close(string key, GameObject target)
    {
        if (listDictionary == null) return;
        if (!listDictionary.ContainsKey(key)) return;

        ObjectList list = listDictionary[key];
        if (list.OpenList.Count <= 0) return;
        list.OpenList.RemoveAt(0);
        target.transform.SetParent(allRoot);
        target.SetActive(false);
        list.CloseList.Add(target);
    }

    /// <summary>
    /// 对象池的克隆队列
    /// </summary>
    public class ObjectList
    {
        public Transform Root;              //放置位置
        public GameObject Target;           //放置目标
        public List<GameObject> OpenList;
        public List<GameObject> CloseList;

        public ObjectList(Transform root, GameObject target)
        {
            OpenList = new List<GameObject>();      //开启的集合
            CloseList = new List<GameObject>();     //关闭的集合
            Root = root;
            Target = target;
        }
    }

    /// <summary>
    /// 对象池配置设置
    /// </summary>
    /// <param name="key">字段</param>
    /// <param name="count">初始数量</param>
    /// <param name="target">对象</param>
    /// <returns> 返回配置信息 </returns>
    public static ObjectPoolConfig Config(string key, int count, GameObject target)
    {
        ObjectPoolConfig config;
        config.Key = key;
        config.Count = count;
        config.Target = target;
        return config;
    }

}

/// <summary>
/// 对象池配置信息类
/// </summary>
public struct ObjectPoolConfig
{
    public string Key;
    public GameObject Target;
    public int Count;
}




