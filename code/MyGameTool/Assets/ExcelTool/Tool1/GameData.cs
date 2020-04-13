/****************************************************
    文件：GameData.cs
	功能：配置信息
*****************************************************/
using UnityEngine;

public class GameData 
{
    
}

/// <summary>
/// 敌人战斗实体的数据
/// </summary>
[System.Serializable]
public struct EnemyProps
{
    public int id;             //敌人ID
    public string enemyName;        //名字
    public string des;              //描述
    public string iconPath;         //icon路径
    public float checkRange;        //察觉范围
    public float attack;            //攻击力
    public float adRange;           //射程
    public float adSpeed;           //攻击速度
    public float adInterval;        //攻击僵直
    public float moveSpeed;         //移动速度
    public float defense;           //防御力
    public float hp;                //血量
    public float exp;               //经验值
}
