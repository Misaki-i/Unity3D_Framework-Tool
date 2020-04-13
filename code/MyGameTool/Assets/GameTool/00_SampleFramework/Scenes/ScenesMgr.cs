/****************************************************
    文件：ScenesMgr.cs
	功能：场景切换模块
*****************************************************/
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScenesMgr : BaseMgr<ScenesMgr>
{
    /// <summary>
    /// 切换场景 同步
    /// </summary>
    /// <param name="name"></param>
    public void LoadScene(string name, UnityAction action)
    {
        //同步加载场景
        SceneManager.LoadScene(name);
        //切换完才执行
        action();
    }

    /// <summary>
    /// 切换场景 异步
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void LoadSceneAsyn(string name,UnityAction action)
    {
        MonoMgr.getInst().StartCoroutine(ReallyLoadSceneAsyn(name, action));

    }
    /// <summary>
    /// 协程异步加载场景
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    IEnumerator ReallyLoadSceneAsyn(string name,UnityAction action)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);

        //得到场景进度
        while (!ao.isDone){

            EventCenter.getInst().EventTrigger("SceneLoadProgress", ao.progress);

            yield return ao.progress;
        }

        //加载完执行action
        action();


    }




}
