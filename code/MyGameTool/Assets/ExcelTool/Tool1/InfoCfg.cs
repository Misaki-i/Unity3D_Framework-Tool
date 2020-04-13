/****************************************************
    文件：InfoCfg.cs
	功能：配置信息文件
*****************************************************/
using System.Collections.Generic;
using UnityEngine;

public class InfoCfg : ScriptableObject
{
    //敌人的
    [SerializeField]
    public EnemyProps[] enemyArray;





    //============  信息读取(最初的值，仅供读取的)    ===============

    //====》初始化，存在集合中
    public void InitData()
    {
        InitEnemyCfg();
    }

    //======》敌人
    Dictionary<int, EnemyProps> enemyDic = new Dictionary<int, EnemyProps>();
    void InitEnemyCfg()
    {
        enemyDic.Clear();
        for (int i = 0; i < enemyArray.Length; i++)
        {
            int enemyType = enemyArray[i].id;
            EnemyProps enemyProps = enemyArray[i];
            enemyDic.Add(enemyType, enemyProps);
        }
    }
    public EnemyProps GetEnemyPropsWithType(int type)
    {
        EnemyProps enemyProps = new EnemyProps();
        if (enemyDic.TryGetValue(type, out enemyProps))
        {
            return enemyProps;
        }
        Debug.LogError("找不到目标===》" + type);
        return enemyProps;
    }
}
