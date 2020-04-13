/****************************************************
    文件：EventCenter.cs
	功能：事件中心
*****************************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter : BaseMgr<EventCenter>
{
    /// <summary>
    /// 事件字典
    /// </summary>
    Dictionary<string, UnityAction<object>> eventDic = new Dictionary<string, UnityAction<object>>();

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="name">事件名字</param>
    /// <param name="action">事件</param>
    public void AddEventListener(string name,UnityAction<object> action)
    {
        if (eventDic.ContainsKey(name)){
            eventDic[name] += action;
        }
        else{
            eventDic.Add(name, action);
        }

    }
    /// <summary>
    /// 事件触发
    /// </summary>
    /// <param name="name">事件名</param>
    public void EventTrigger(string name,object param)
    {
        if (eventDic.ContainsKey(name)){
            eventDic[name].Invoke(param);
        }
    }
    /// <summary>
    /// 清空事件中心
    /// </summary>
    public void ClearEvent(){
        eventDic.Clear();
    }





}
