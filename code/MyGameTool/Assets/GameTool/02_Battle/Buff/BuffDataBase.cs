/****************************************************
    文件：BuffDataBase.cs
	功能：Buff数据基类
*****************************************************/
using System;
using UnityEngine;

[Serializable]
public class BuffDataBase
{
    #region ========== Buff数据信息 ==========

    public int buffID;                                                              // Buff ID
    public BuffType buffType;                                                       // Buff类型
    public BuffOverlap buffOverlap;                                                 // Buff叠加类型
    public BuffCalculateType BuffCalculate;                                         // Buff执行类型

    [SerializeField]
    protected float curTime;                                                        // 当前时间
    public float CurTime { get { return curTime; } }

    [SerializeField]
    protected float maxLimit;                                                       // 最大限制
    public float MaxLimit { get { return maxLimit; } }

    [SerializeField]
    protected float curFrequencyTime;                                               // 当前频率计时
    public float CurFrequencyTime { get { return curFrequencyTime; } }

    [SerializeField]
    protected float frequencyTime;
    public float FrequencyTime { get { return frequencyTime; } }                    // 调用频率间隔

    [SerializeField]
    protected bool isFinish;                                                        // 是否结束
    public bool IsFinish { get { return isFinish; } }

    [SerializeField]
    protected int maxLayer;                                                         // 最大层
    public int MaxLayer { get { return maxLayer; } }

    [SerializeField]
    protected int curLayer;                                                         // 当前层
    public int CurLayer { get { return curLayer; } }

    protected object numData;                                                       // 事件参数
    public object NumData { get { return numData; } }           


    #endregion

    #region ========== 回调事件 ==========
    /// <summary>
    /// Buff开始回调
    /// </summary>
    public Action OnStart;                                      
    /// <summary>
    /// Buff结束回调
    /// </summary>
    public Action OnFinsh;                                      
    /// <summary>
    /// 检测是否已经保留buff回调
    /// </summary>
    public Func<bool> OnCheckCache;                             
    /// <summary>
    /// 按照频率调用方法
    /// </summary>
    public Action OnFrequency;                                  
    /// <summary>
    /// 叠加层的事件
    /// </summary>
    public Action<object> OnAddLayer;                            
    /// <summary>
    /// 计时回调
    /// </summary>
    public Action<float> OnCurTime;                                 
    #endregion

    #region ========== 叠加方法 ==========

    /// <summary>
    /// 重置时间
    /// </summary>
    public void RestartTime()
    {
        curTime = 0;
        curFrequencyTime = 0;
    }
    /// <summary>
    /// 添加时间
    /// </summary>
    /// <param name="addTime">时间</param>
    public void AddTime(float addTime)
    {
        maxLimit += addTime;
    }
    /// <summary>
    /// 叠加层的方法
    /// </summary>
    /// <param name="value"></param>
    public void AddLayer(object value){
        if (curLayer < maxLayer){
            curLayer++;
            if (OnAddLayer != null) OnAddLayer(value);
        }
    }


    #endregion

    #region ========== Buff处理 ==========

    /// <summary>
    /// Buff开启
    /// </summary>
    public void StartBuff()
    {
        isFinish = false;
        curLayer++;
        if (OnStart != null) OnStart();
    }
    /// <summary>
    /// Buff关闭
    /// </summary>
    public void CloseBuff()
    {
        if (OnFinsh != null) OnFinsh();
        ClearBuffData();
    }
    /// <summary>
    /// 清除回收
    /// </summary>
    public virtual void ClearBuffData()
    {
        maxLimit = 0;
        buffID = 0;
        curTime = 0;
        curFrequencyTime = 0;
        frequencyTime = 0;
        numData = 0;
        curLayer = 0;
        maxLayer = 0;
        isFinish = true;

        //回调事件清除
        ClearAllCallBack();
    }
    /// <summary>
    /// 清除回调
    /// </summary>
    protected void ClearAllCallBack()
    {
        OnFrequency = null;
        OnStart = null;
        OnFinsh = null;
        OnCheckCache = null;
        OnCurTime = null;
        OnAddLayer = null;
    }

    /// <summary>
    /// Buff执行中的方法 
    /// </summary>
    /// <param name="delayTime">延时</param>
    public void OnTick(float delayTime)
    {
        if (isFinish) return;

        curTime += delayTime;
        //== 一次，按时间的
        if (BuffCalculate == BuffCalculateType.Once)
        {
            if (OnCurTime != null)
            {
                OnCurTime(maxLimit - curTime);

            }
            //判断时间
            if (curTime >= maxLimit)
            {
                isFinish = true;
                return;
            }
        }
        //== 按频率的
        else if (BuffCalculate == BuffCalculateType.Frequency)
        {
            curFrequencyTime += delayTime;
            if (curTime >= FrequencyTime)
            {
                curTime = 0;
                if (OnFrequency != null)
                    OnFrequency();
                return;
            }
            if (curFrequencyTime >= maxLimit)
            {
                isFinish = true;
                curTime = 0;
                if (OnFrequency != null)
                    OnFrequency();
                return;
            }
        }
        //== 按回调条件的
        else if (BuffCalculate == BuffCalculateType.Cache)
        {
            if (OnCheckCache())
            {
                isFinish = true;
                return;
            }
        }
    }

    #endregion

}


