/****************************************************
    文件：DemoActor.cs
	功能：测试Buff
*****************************************************/
using UnityEngine;

public class DemoActor : MonoBehaviour
{
    EntityBuff entityBuff;

    private void Start()
    {
        entityBuff = gameObject.AddComponent<EntityBuff>();


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            DoOnceBuff();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DoFrequencyBuff();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DoAddLayerBuff();
        }
    }


    /// <summary>
    /// Buff进行一次操作
    /// </summary>
    void DoOnceBuff()
    {
        BuffCfg buffCfg = new BuffCfg
        {
            buffID = (int)BuffType.BuffOnce,
            buffType = BuffType.BuffOnce,
            buffCalculate = BuffCalculateType.Once,
            buffOverlap = BuffOverlap.ResterTime,
            maxLimit = 5,
            frequencyTime = 0,
        };
        BuffDataActor buffData = BuffDataActor.CreateData(buffCfg);
        buffData.OnStart += () =>
        {
            Debug.Log(">>>>>>>>>>>" + buffData.buffType.ToString() + "开始啦啦");
        };
        buffData.OnFinsh += () =>
        {
            Debug.Log(">>>>>>>>>>>" + buffData.buffType.ToString() + "结束啦啦");
        };

        // 倒计时回调
        buffData.OnCurTime += delegate (float curTime)
        {
            Debug.Log("结束计时回调>>>>>>>>>>>" + buffData.buffType.ToString() + curTime);
        };

        entityBuff.DoBuff(buffData);
    }

    /// <summary>
    /// buff按频率回调操作
    /// </summary>
    void DoFrequencyBuff()
    {
        BuffCfg buffCfg = new BuffCfg
        {
            buffID = (int)BuffType.BuffFrequency,
            buffType = BuffType.BuffFrequency,
            buffCalculate = BuffCalculateType.Frequency,
            buffOverlap = BuffOverlap.ResterTime,
            maxLimit = 5,
            frequencyTime = 1,                              //频率间隔
        };
        BuffDataActor buffData = BuffDataActor.CreateData(buffCfg);
        buffData.OnStart += () =>
        {
            Debug.Log(">>>>>>>>>>>" + buffData.buffType.ToString() + "开始啦啦");
        };
        buffData.OnFinsh += () =>
        {
            Debug.Log(">>>>>>>>>>>" + buffData.buffType.ToString() + "结束啦啦");
        };
        int count = 0;
        // 频率回调事件
        buffData.OnFrequency += () =>
        {
            count++;
            Debug.Log(">>>>>>>>>>>" + buffData.buffType.ToString() + "回调  " + count + "次");
        };

        entityBuff.DoBuff(buffData);
    }
    /// <summary>
    /// buff按叠加层进行操作
    /// </summary>
    void DoAddLayerBuff()
    {
        BuffCfg buffCfg = new BuffCfg
        {
            buffID = (int)BuffType.BuffAddLayer,
            buffType = BuffType.BuffAddLayer,
            buffCalculate = BuffCalculateType.Once,
            buffOverlap = BuffOverlap.AddLayer,             //叠加层
            maxLimit = 10,
            frequencyTime = 1,
            maxLayer = 5,
            numData = "/* 添加层的参数 */",
        };
        BuffDataActor buffData = BuffDataActor.CreateData(buffCfg);
        buffData.OnStart += () =>
        {
            Debug.Log(">>>>>>>>>>>" + buffData.buffType.ToString() + "开始啦啦");
        };
        buffData.OnFinsh += () =>
        {
            Debug.Log(">>>>>>>>>>>" + buffData.buffType.ToString() + "结束啦啦");
        };
        // 叠加回调事件
        buffData.OnAddLayer += (object numData) =>
        {
            Debug.Log(">>>>>>>>>>>" + buffData.buffType.ToString() + "当前层  " + buffData.CurLayer);
            Debug.Log(">>>>>>>>>>> 回调参数   " + numData.ToString());
        };

        entityBuff.DoBuff(buffData);
    }








}
