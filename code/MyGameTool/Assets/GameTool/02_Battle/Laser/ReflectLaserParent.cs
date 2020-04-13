/****************************************************
    文件：ReflectLaserParent.cs
    功能：反射激光父节点管理
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReflectLaserParent : MonoBehaviour
{

    private void Start()
    {
        InitForwardLaser(5,"111");
    }

    public GameObject laserObject;

    // //=========》跟踪反射激光
    List<LaserBase>  laserList = new List<LaserBase>();
    public Transform InitForwardLaser(int laserCount, string prefabPath){
        //====获取预制体，存入集合
        for (int i = 0; i < laserCount; i++){
            //Transform tempTr = ResourcesMgr.getInst().getPrefab(prefabPath, transform);
            Transform tempTr = Instantiate(laserObject, transform).transform;
            LaserBase temp = tempTr.GetComponent<LaserBase>();
            laserList.Add(temp);
        }
        //====初始化射线，建立链接
        for (int i = 0; i < laserList.Count; i++){
            if (i == 0){
                laserList[i].transform.position = transform.position;
                laserList[i].transform.eulerAngles = transform.eulerAngles;
                LaserBase temp1 = laserList[i].transform.GetComponent<LaserBase>();
                temp1.InitRefForwardLaser(null);
            }
            else{
                LaserBase temp1 = laserList[i].transform.GetComponent<LaserBase>();
                temp1.InitRefForwardLaser(laserList[i - 1]);
            }
        }
        return this.transform;
    }


    //==========》有伤害的反射激光
    public Transform InitAttackLaser(Transform attacker, float damage, int laserCount,string prefabPath, float laserSpeed, System.Action overCallBack = null){
        StartCoroutine(InitAttackLaserEvent(attacker, damage, laserCount, prefabPath, laserSpeed, overCallBack));
        return this.transform;
    }


    //======协程延迟发射的
    IEnumerator InitAttackLaserEvent(Transform attacker, float damage, int laserCount, string prefabPath, float laserSpeed, System.Action overCallBack = null){
        for (int i = 0; i < laserCount; i++){
            //Transform tempTr = ResourcesMgr.getInst().getPrefab(prefabPath, transform);
            Transform tempTr = null;
            LaserBase temp = tempTr.GetComponent<LaserBase>();
            laserList.Add(temp);
        }
        for (int i = 0; i < laserList.Count; i++){
            float delatTime = 0.2f;
            if (i == 0){
                laserList[i].transform.position = transform.position;
                laserList[i].transform.eulerAngles = transform.eulerAngles;
                LaserBase temp1 = laserList[i].transform.GetComponent<LaserBase>();
                //delatTime = temp1.InitRefAttackLaser(null, attacker, damage, laserSpeed);
            }
            else{
                LaserBase temp1 = laserList[i].transform.GetComponent<LaserBase>();
                //delatTime = temp1.InitRefAttackLaser(laserList[i - 1], attacker, damage, laserSpeed);
            }
            //=====返回一个延迟....
            yield return new WaitForSeconds(delatTime);
        }
        
        yield return new WaitForSeconds(0.5f);          //延迟触发的
        //激光结束后调用回调
        if (overCallBack != null) overCallBack();
    }

}
