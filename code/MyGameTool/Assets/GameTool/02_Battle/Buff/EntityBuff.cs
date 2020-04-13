/****************************************************
    文件：EntityBuff.cs
	功能：Buff实体类（挂载添加到对象上）
*****************************************************/
using System.Collections.Generic;
using UnityEngine;

public class EntityBuff : MonoBehaviour
{
    /// <summary>
    /// Buff集合
    /// </summary>
    [SerializeField]
    List<BuffDataBase> buffs = new List<BuffDataBase>();


    #region 外部接口
    /// <summary>
    /// 外部调用执行Buff
    /// </summary>
    /// <param name="buffData">Buff信息</param>
    public virtual void DoBuff(BuffDataBase buffData)
    {
        if (buffData == null) return;

        if (!IsExitBuff(buffData))
        {
            AddBuff(buffData);
        }
    }
    /// <summary>
    /// buff添加
    /// </summary>
    /// <param name="buffData"></param>
    public void AddBuff(BuffDataBase buffData)
    {
        if (!buffs.Contains(buffData))
        {
            buffs.Add(buffData);
            buffData.StartBuff();
        }
    }

    /// <summary>
    /// 关闭Buff
    /// </summary>
    /// <param name="buffID">id</param>
    public virtual void CloseBuff(int buffID)
    {
        BuffDataBase tempBuff = GetCurBuffData(buffID);
        if (tempBuff != null)
        {
            tempBuff.CloseBuff();
            buffs.Remove(tempBuff);
        }
    }


    /// <summary>
    /// 获取当前Buff层数
    /// </summary>
    /// <param name="buffID">id</param>
    /// <returns></returns>
    public int getBuffLayer(int buffID)
    {
        BuffDataBase tempBuff = GetCurBuffData(buffID);
        int count = 0;
        if (tempBuff != null)
        {
            count = tempBuff.CurLayer;
        }
        return count;
    }

    /// <summary>
    /// 检测Buff存在
    /// </summary>
    /// <param name="buffID">id</param>
    /// <returns></returns>
    public bool IsExitBuff(int buffID)
    {
        BuffDataBase tempBuff = GetCurBuffData(buffID);
        if (tempBuff != null)
        {
            return true;
        }
        return false;
    }
    #endregion

    #region 内部的
    /// <summary>
    /// 判断buff是否存在
    /// </summary>
    /// <param name="buff">buff信息</param>
    /// <returns></returns>
    bool IsExitBuff(BuffDataBase buff)
    {
        BuffDataBase tempBuff = GetCurBuffData(buff.buffID);
        if (tempBuff != null)
        {
            switch (tempBuff.buffOverlap)
            {
                case BuffOverlap.AddTime:
                    tempBuff.AddTime(buff.MaxLimit);
                    break;
                case BuffOverlap.ResterTime:
                    tempBuff.RestartTime();
                    break;
                case BuffOverlap.AddLayer:
                    tempBuff.RestartTime();
                    tempBuff.AddLayer(tempBuff.NumData);
                    break;
                case BuffOverlap.None:
                    break;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// 获取buff信息
    /// </summary>
    /// <param name="buffID">id</param>
    /// <returns></returns>
    BuffDataBase GetCurBuffData(int buffID)
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i].buffID == buffID)
            {
                return buffs[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 刷新Buff事件
    /// </summary>
    private void FixedUpdate()
    {
        for (int i = buffs.Count - 1; i >= 0; i--)
        {
            buffs[i].OnTick(Time.fixedDeltaTime);
            if (buffs[i].IsFinish)
            {
                buffs[i].CloseBuff();
                buffs.Remove(buffs[i]);
            }
        }
    }
    #endregion

    #region 备注
    /*

 PS:使用（建议配置表格）



        // 1、buff实体类
        EntityBuff entityBuff = gameObject.AddComponent<EntityBuff>();

        // 2、buff信息（自定义/表格）
        BuffCfg buffCfg = new BuffCfg
        {
            buffID = (int)BuffType.BuffOnce,
            buffType = BuffType.BuffOnce,
            buffCalculate = BuffCalculateType.Once,
            buffOverlap = BuffOverlap.ResterTime,
            maxLimit = 5,
            frequencyTime = 0,

            //....
        };
        // 3、创建buff数据信息(添加回调事件)
        BuffDataActor buffData = BuffDataActor.CreateData(buffCfg);
        buffData.OnStart += () => { };
        buffData.OnFinsh += () => { };

        //...

        // 4、执行buff
        entityBuff.DoBuff(buffData);

*/ 
    #endregion



}
