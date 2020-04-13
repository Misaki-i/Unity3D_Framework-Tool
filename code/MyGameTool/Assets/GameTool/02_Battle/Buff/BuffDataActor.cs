/****************************************************
    文件：BuffDataActor.cs
	功能：Buff数据角色类
*****************************************************/
using System.Collections.Generic;
using UnityEngine;

public class BuffDataActor : BuffDataBase{
    protected static Stack<BuffDataActor> poolCache = new Stack<BuffDataActor>();        // 栈存储

    /// <summary>
    /// 外部创建实例化对象
    /// </summary>
    /// <param name="buffCfg">Buff信息</param>
    /// <returns></returns>
    public static BuffDataActor CreateData(BuffCfg buffCfg)
    {
        BuffDataActor tempData = Pop();
        tempData.buffID = buffCfg.buffID;
        tempData.buffType = buffCfg.buffType;
        tempData.BuffCalculate = buffCfg.buffCalculate;
        tempData.buffOverlap = buffCfg.buffOverlap;
        tempData.maxLimit = buffCfg.maxLimit;
        tempData.frequencyTime = buffCfg.frequencyTime;
        tempData.numData = buffCfg.numData;
        tempData.maxLayer = buffCfg.maxLayer;

        //DataBase 内部动态属性初始化
        tempData.curLayer = 0;
        tempData.curTime = 0;
        tempData.curFrequencyTime = 0;
        tempData.isFinish = false;
        tempData.ClearAllCallBack();    // 回调

        return tempData;
    }

    /// <summary>
    /// 清除回收buff
    /// </summary>
    public override void ClearBuffData()
    {
        base.ClearBuffData();
        Push(this);
    }


    //==== 栈存储方法

    /// <summary>
    /// 弹出
    /// </summary>
    /// <returns></returns>
    protected static BuffDataActor Pop(){
        if (poolCache.Count < 1)
        {
            BuffDataActor bd = new BuffDataActor();
            return bd;
        }
        return poolCache.Pop();
    }
    /// <summary>
    /// 压入
    /// </summary>
    /// <param name="buffData"></param>
    protected static void Push(BuffDataActor buffData){
        poolCache.Push(buffData);
    }
}
