/****************************************************
    文件：BuffCfg.cs
	功能：Buff数据类
*****************************************************/
using UnityEngine;

/// <summary>
/// Buff基础数据
/// </summary>
public class BuffCfg
{
    public int buffID;                          // Buff ID
    public BuffType buffType;                   // Buff枚举
    public BuffOverlap buffOverlap;             // 叠加类型
    public BuffCalculateType buffCalculate;     // 执行类型
    public float frequencyTime;                 // 频率间隔时间
    public float maxLimit;                      // 最大时间
    public object numData;                      // 执行传递参数（自定义）
    public int maxLayer;                        // 最大层数
}


/// <summary>
/// Buff叠加类型（用于重复调用判断）
/// </summary>
public enum BuffOverlap
{
    /// <summary>
    /// 增加时间
    /// </summary>
    AddTime = 1,            
    /// <summary>
    /// 重置时间
    /// </summary>
    ResterTime = 2,      
    /// <summary>
    /// 叠加层
    /// </summary>
    AddLayer = 3,           
    /// <summary>
    /// 仅一次操作（不做调用）
    /// </summary>
    None = 4,               //  仅一次操作的
}

/// <summary>
/// Buff执行类型
/// </summary>
public enum BuffCalculateType
{
    /// <summary>
    /// 进行一次操作
    /// </summary>
    Once,                       
    /// <summary>
    /// 按照频率进行操作
    /// </summary>
    Frequency,                  
    /// <summary>
    /// 保留，按特定条件移除
    /// </summary>
    Cache                       
}

/// <summary>
/// Buff枚举
/// </summary>
public enum BuffType
{
    BuffOnce = 1001,
    BuffFrequency = 1002,
    BuffAddLayer = 1003,





    //NociceptiveReflex = 10001,      //普通伤害
    //BloodSucking = 10002,           //治疗
    //Rage = 10003,                   //攻击提高 
    //FlameI = 10004,                 //攻击提高 
    //Recovery = 10005,               //治疗
    //Blinding = 10006,               //命中下降
    //BulletTime = 10007,             //攻击增益（射程)
    //FlameII = 10008,                //攻击增益（装填）
    //Slamming = 10009,               //攻击提高 
    //EasilyInjured = 10010,          //防御下降
    //ShootSeed = 10011,              //攻击增益（射速)
    //RunSeed = 10012,                //攻击增益（移速）
    //SlowDown = 10013,               //减速
    //CrazyDance = 10014,             // 技能，狂抢乱舞
    //BloodRange = 10015,             // 技能，吸血光环
    //AddBullet = 10016,              // 量子子弹，


    Null
}