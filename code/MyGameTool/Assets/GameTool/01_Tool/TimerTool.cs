﻿/****************************************************
    功能：  计时器小工具
    
    PS：要控制生成的计时器回调，需要获取其对象进行操作
*****************************************************/
using UnityEngine;

/// <summary>
/// 计时器模块封装
/// </summary>
public class TimerTool : MonoBehaviour
{
    /// <summary>
    /// 计时器开启一次
    /// </summary>
    /// <param name="time">时间</param>
    /// <param name="func">调用方法</param>
    /// <returns></returns>
    public static TimerTool OpenOnce(float time, System.Action func)
    {
        return Open(time, false, 0, func);
    }
    /// <summary>
    /// 计时器循环开启
    /// </summary>
    /// <param name="time">时间</param>
    /// <param name="loopCount">循环次数</param>
    /// <param name="func">调用方法</param>
    /// <param name="loopFinishFun">循环结束调用的</param>
    /// <returns></returns>
    public static TimerTool OpenLoop(float time, int loopCount, System.Action func, System.Action loopFinishFun = null)
    {
        // 可以先调用一次，（不然会延迟....）
        // if (func != null) func();
        return Open(time, true, loopCount, func, loopFinishFun);
    }


    //状态机中调用的
    public void StatusOpenOnce(float time, System.Action fun)
    {
        Open(time, false, 0, fun);
    }
    public void StatusOpenLoop(float time, int loopCount, System.Action fun, System.Action loopFinishFun = null)
    {
        Open(time, true, loopCount, fun, loopFinishFun);
    }

    /// <summary>
    /// 暂停计时器
    /// </summary>
    public void Pause()
    {
        if (isPause) return;
        isPause = true;
    }
    /// <summary>
    /// 继续计时器
    /// </summary>
    public void Continue()
    {
        if (!isPause) return;
        isPause = false;
    }
    /// <summary>
    /// 停止计时器
    /// </summary>
    public void Stop()
    {
        Close();
    }


    private static Transform TimerRoot;                         //计时器挂载位置
    /// <summary>
    /// 开启计时器
    /// </summary>
    /// <param name="mark">标识</param>
    /// <param name="time">时间</param>
    /// <param name="isLoop">是否循环</param>
    /// <param name="loopCount">循环次数</param>
    /// <param name="func">调用方法</param>
    /// <returns></returns>
    private static TimerTool Open(float time, bool isLoop, int loopCount, System.Action func, System.Action loopFinishFun = null)
    {

        if (TimerRoot == null)
        {
            TimerRoot = new GameObject("TimerRoot").transform;
        }

        GameObject buf;

        //通常方法
        buf = new GameObject("timer");
        buf.transform.parent = TimerRoot;

        //初始化
        TimerTool timer = buf.AddComponent<TimerTool>();
        timer.TimerInit(time, isLoop, loopCount, func, loopFinishFun);

        return timer;
    }


    private float maxTime = 0;
    private float currentTime = 0;
    private bool isLoop = false;
    private int loopCount = 0;
    private bool isOpen = false;
    private bool isPause = false;
    private System.Action CallBack;
    private System.Action LoopFinishCallBack;

    /// <summary>
    /// 倒计时时间
    /// </summary>
    public float MaxTime { get { return maxTime; } }
    /// <summary>
    /// 当前倒计时
    /// </summary>
    public float CurrentTime { get { return currentTime; } }
    /// <summary>
    /// 是否循环
    /// </summary>
    public bool IsLoop { get { return isLoop; } }
    /// <summary>
    /// 循环次数
    /// </summary>
    public int LoopCount { get { return loopCount; } }
    /// <summary>
    /// 剩余时间
    /// </summary>
    public float TimeRemainder { get { return maxTime - currentTime; } }
    /// <summary>
    /// 计时是否开启
    /// </summary>
    public bool IsOpen { get { return isOpen; } }
    /// <summary>
    /// 计时是否暂停
    /// </summary>
    public bool IsPause { get { return isPause; } }
    /// <summary>
    /// 计时器初始化
    /// </summary>
    /// <param name="_mark"></param>
    /// <param name="_time"></param>
    /// <param name="_isLoop"></param>
    /// <param name="_loopCount"></param>
    /// <param name="func"></param>
    private void TimerInit(float _time, bool _isLoop, int _loopCount, System.Action func, System.Action loopFinishFun = null)
    {
        isOpen = true;
        maxTime = _time;
        isLoop = _isLoop;
        loopCount = _loopCount;
        CallBack = func;
        LoopFinishCallBack = loopFinishFun;
    }
    /// <summary>
    /// 计时关闭
    /// </summary>
    /// <param name="_mark"></param>
    private void Close()
    {
        isOpen = false;
        isPause = false;
        maxTime = 0;
        currentTime = 0;
        loopCount = 0;
        CallBack = null;
        Destroy(gameObject);
    }

    //计时器更新处理
    private void Update()
    {
        if (!isOpen) return;
        if (isPause) return;

        currentTime += Time.deltaTime;

        if (currentTime >= maxTime)
        {
            //循环处理
            if (isLoop)
            {
                if (loopCount > 0)
                {
                    loopCount--;

                    if (CallBack != null) CallBack();

                    if (loopCount <= 0)
                    {
                        Close();
                        if (LoopFinishCallBack != null) LoopFinishCallBack();
                    }
                    else
                    {
                        currentTime = 0;
                    }
                }
                else if (loopCount == -1)        //无限循环处理
                {
                    if (CallBack != null) CallBack();
                    currentTime = 0;
                }
                else
                {
                    if (CallBack != null) CallBack();
                    Close();
                    
                    if (LoopFinishCallBack != null) LoopFinishCallBack();
                }
            }
            //一次处理
            else
            {
                if (CallBack != null) CallBack();
                Close();
                
            }
        }
    }

}




