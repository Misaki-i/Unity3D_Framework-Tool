/****************************************************
    文件：MonoMgr.cs
	功能：公共Mono管理模块
*****************************************************/
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///    公共Mono管理模块
/// 1、提供帧更新事件方法
/// 2、提供协成方法
/// </summary>
public class MonoMgr : BaseMgr<MonoMgr>
{
    MonoContoller controller;
    public MonoMgr(){
        GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoContoller>();
    }

    /// <summary>
    /// 添加帧更新事件
    /// </summary>
    /// <param name="action"></param>
    public void AddUpdateListener(UnityAction action)
    {
        controller.AddUpdateListener(action);
    }
    /// <summary>
    /// 移除帧更新事件
    /// </summary>
    /// <param name="action"></param>
    public void RemoveUpdateListener(UnityAction action)
    {
        controller.RemoveUpdateListener(action);
    }
    /// <summary>
    /// 开启事件事件
    /// </summary>
    /// <param name="routine"></param>
    /// <returns></returns>
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }



}
