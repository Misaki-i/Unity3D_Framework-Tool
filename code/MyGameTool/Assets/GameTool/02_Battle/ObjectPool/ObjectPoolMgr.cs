/****************************************************
    文件：ObjectPoolMgr.cs
	功能：对象池管理类
*****************************************************/
using UnityEngine;

public class ObjectPoolMgr : MonoBehaviour
{
    static ObjectPoolMgr inst;
    public static ObjectPoolMgr getInst() { return inst; }
    

    #region 对象池字段

    const string HurtItemStr = "HurtItemStr";

    //ect...........

    #endregion

    private void Awake(){
        inst = this;

        InitObjectPool();
    }

    /// <summary>
    /// 初始化对象池
    /// </summary>
    public void InitObjectPool(){
        InitHurtItemObjPool();      //初始化数值UI对象池

        //etc.......
    }


    #region /***** 数值UI显示对象池 **********************************************************************/

    ObjectPool hurtItemPool;
    /// <summary>
    /// 初始化数值显示对象池
    /// </summary>
    void InitHurtItemObjPool(){
        GameObject hurtItemGo = Resources.Load<GameObject>("");            //加载
        hurtItemPool = new ObjectPool(ObjectPool.Config(HurtItemStr, 5, hurtItemGo));
    }
    /// <summary>
    /// 开启数值显示对象(PS：自定义初始化)
    /// </summary>
    /// <param name="target">挂载位置</param>
    /// <param name="des">显示字段</param>
    public Transform OpenHurtItem(Transform target, string des){
        GameObject temp = hurtItemPool.Open(HurtItemStr, target);
        return temp.transform;
    }
    /// <summary>
    /// 回收数值信息对象
    /// </summary>
    /// <param name="targetGo">回收的对象</param>
    public void CloseHurtItem(GameObject targetGo){
        hurtItemPool.Close(HurtItemStr, targetGo);
    }

    #endregion

    #region //etc....... 

    #endregion






}
