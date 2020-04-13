/****************************************************
    文件：MonoContoller.cs
	功能：公共Mono控制
*****************************************************/
using UnityEngine;
using UnityEngine.Events;

public class MonoContoller : MonoBehaviour
{
    event UnityAction updateEvent;

    private void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (updateEvent != null)
            updateEvent();
    }

    /// <summary>
    /// 添加帧更新事件
    /// </summary>
    /// <param name="action"></param>
    public void AddUpdateListener(UnityAction action)
    {
        updateEvent += action;
    }
    /// <summary>
    /// 移除帧更新事件
    /// </summary>
    /// <param name="action"></param>
    public void RemoveUpdateListener(UnityAction action)
    {
        updateEvent -= action;
    }
}
