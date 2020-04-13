/****************************************************
    文件：ResMgr.cs
	功能：资源加载
*****************************************************/
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ResMgr : BaseMgr<ResMgr>
{
    /// <summary>
    /// 同步加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T Load<T>(string name) where T : Object
    {
        T res = Resources.Load<T>(name);
        if (res is GameObject)
            return GameObject.Instantiate(res);
        else
            return res;
    }


    /// <summary>
    /// 异步加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="callBack"></param>
    public void LoadAsync<T>(string name,UnityAction<T> callBack) where T : Object
    {
        MonoMgr.getInst().StartCoroutine(ReallyLoadAsync<T>(name, callBack));
    }
    IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callBack) where T : Object
    {
        ResourceRequest r = Resources.LoadAsync<T>(name);

        yield return r;
        if (r.asset is GameObject)
            callBack(GameObject.Instantiate(r.asset) as T);
        else
            callBack(r.asset as T);
    }



}
