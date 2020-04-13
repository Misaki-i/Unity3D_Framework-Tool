/****************************************************
    文件：CommonTool.cs
	功能：一些通用工具类
*****************************************************/
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;

public static class CommonTool
{

    #region /***** 转向处理 **********************************************************************/

    // ============= 3D坐标旋转的 ==============

    /// <summary>
    /// 3D坐标旋转（有渐变效果）
    /// </summary>
    /// <param name="owner">自身</param>
    /// <param name="target">目标</param>
    /// <param name="lerpTime">lerp值</param>
    public static void LookAtTargetLerp(Transform owner, Transform target, float lerpTime = 0.2f)
    {
        if (owner == null || target == null) return;
        Vector3 dir = (target.position - owner.position).normalized;          //获取方向        
        Quaternion quaDir = Quaternion.LookRotation(dir, Vector3.up);                    //将方向转换为四元数
        owner.DORotateQuaternion(quaDir, lerpTime);
    }
    //无渐变，直接用LookAt


    // ============= 2D坐标旋转的 ==============

    /// <summary>
    /// 2D坐标旋转（有渐变效果）
    /// </summary>
    /// <param name="owner">自身</param>
    /// <param name="target">目标</param>
    /// <param name="lerpTime">lerp值</param>
    /// <param name="callBack">旋转后回调</param>
    public static void LookAtTarget2DLerp(Transform owner, Transform target, float lerpTime = 0.2f, Action callBack = null)
    {
        if (owner == null || target == null) return;
        Vector3 targetTrTemp = new Vector3(target.position.x, owner.position.y, target.position.z);
        Vector3 dir = (targetTrTemp - owner.position).normalized;          //获取方向        
        Quaternion quaDir = Quaternion.LookRotation(dir, Vector3.up);                    //将方向转换为四元数
        owner.DORotateQuaternion(quaDir, lerpTime).OnComplete(() => {
            if (callBack != null) callBack();
        });
    }
    /// <summary>
    /// 2D坐标旋转（无渐变效果）
    /// </summary>
    /// <param name="owner">自身</param>
    /// <param name="target">目标</param>
    public static void LookAtTarget2D(Transform owner, Transform target)
    {
        if (owner == null || target == null) return;
        Vector3 targetTrTemp = new Vector3(target.position.x, owner.position.y, target.position.z);
        Vector3 dir = (targetTrTemp - owner.position).normalized;          //获取方向        
        Quaternion quaDir = Quaternion.LookRotation(dir, Vector3.up);                    //将方向转换为四元数
        owner.rotation = quaDir;
    }
    #endregion

    #region /***** 检测范围的 **********************************************************************/

    //  圆形
    public static bool CheckCircleRange(Transform startTr, Transform target, float radius)
    {
        float distance = Vector3.Distance(startTr.position, target.position);
        if (distance <= radius)
            return true;
        return false;
    }
    //  扇形
    public static bool CheckSectorRange(Transform attacker, Transform attacked, float radius, float angle)
    {
        Vector3 deltaA = attacked.position - attacker.position;

        //Mathf.Rad2Deg : 弧度值到度转换常度
        //Mathf.Acos(f) : 返回参数f的反余弦值
        float tmpAngle = Mathf.Acos(Vector3.Dot(deltaA.normalized, attacker.forward)) * Mathf.Rad2Deg;
        if (tmpAngle < angle * 0.5f && deltaA.magnitude < radius)
        {
            return true;
        }
        return false;
    }

    //  矩形

    /// <summary>
    /// 对象正前方的矩形检测
    /// </summary>
    /// <param name="owner">自身</param>
    /// <param name="target">目标</param>
    /// <param name="forwardDistance">前方距离</param>
    /// <param name="with">宽</param>
    /// <returns></returns>
    public static bool CheckRectangleRange(Transform owner, Transform target, float forwardDistance, float with)
    {
        Vector3 deltaA = target.position - owner.position;
        float forwardDotA = Vector3.Dot(owner.forward, deltaA);
        if (forwardDotA > 0 && forwardDotA <= forwardDistance)
        {
            if (Mathf.Abs(Vector3.Dot(owner.right, deltaA)) < with / 2)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 对象中间的矩形检测
    /// </summary>
    /// <param name="owner">自身</param>
    /// <param name="target">目标</param>
    /// <param name="forwardDistance">前方距离</param>
    /// <param name="with">宽</param>
    /// <returns></returns>
    public static bool CheckRectangleMiddleRange(Transform owner, Transform target, float forwardDistance, float with)
    {
        Vector3 deltaA = target.position - owner.position;

        float forwardDotA = Vector3.Dot(owner.forward, deltaA);
        if (forwardDotA > 0 && forwardDotA <= forwardDistance / 2)
        {
            if (Mathf.Abs(Vector3.Dot(owner.right, deltaA)) < with / 2)
            {
                return true;
            }
        }
        else if (forwardDotA < 0 && forwardDotA >= -forwardDistance / 2)
        {
            if (Mathf.Abs(Vector3.Dot(owner.right, deltaA)) < with / 2)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 判断是否在屏幕内
    /// </summary>
    /// <param name="targetTr">对象</param>
    /// <returns></returns>
    public static bool CheckEnemyIsView(Transform targetTr)
    {
        if (targetTr == null) return false;
        Transform camreatra = Camera.main.transform;
        Vector3 viewPos = Camera.main.WorldToViewportPoint(targetTr.position);
        Vector3 dir = (targetTr.position - camreatra.position).normalized;
        float dot = Vector3.Dot(camreatra.forward, dir);
        if (dot > 0 && viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1)
        {
            return true;
        }
        return false;
    }
    #endregion

    #region /***** 2维，3维坐标转换 **********************************************************************/


    /// <summary>
    /// Vector3 转 Vector2
    /// </summary>
    public static Vector2 getVector2(Vector3 a)
    {
        Vector2 posA = new Vector2(a.x, a.z);
        return posA;
    }
    /// <summary>
    /// Vector2 转 Vector3 y初始值为0
    /// </summary>
    public static Vector3 GetVector3(Vector2 a)
    {
        Vector3 posA = new Vector3(a.x, 0, a.y);
        return posA;
    }
    /// <summary>
    /// 获取Vector2距离(Vectpr2)
    /// </summary>
    public static float getDistance2D(Transform a, Transform b)
    {
        Vector2 posA = getVector2(a.position);
        Vector2 posB = getVector2(b.position);
        return Vector2.Distance(posA, posB);
    }
    /// <summary>
    /// 获取Vector2距离(Vector3)
    /// </summary>
    public static float getDistance2D(Vector3 a, Vector3 b)
    {
        Vector2 posA = getVector2(a);
        Vector2 posB = getVector2(b);
        return Vector2.Distance(posA, posB);
    }

    /// <summary>
    /// 获取Vector2方向向量
    /// </summary>
    public static Vector2 getDirection(Transform a, Transform b)
    {
        Vector2 posA = getVector2(a.position);
        Vector2 posB = getVector2(b.position);
        return posB - posA;
    }

    #endregion

    #region 好像有用的小工具 (๑╹◡╹)ﾉ"""
    /// <summary>
    /// 获取或者添加组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <returns></returns>
    public static T getOrAddComponet<T>(GameObject go) where T : Component
    {
        T t = go.GetComponent<T>();
        if (t == null)
        {
            t = go.AddComponent<T>();
        }
        return t;
    }
    /// <summary>
    /// 仅返回目标下的子物体
    /// </summary>
    /// <param name="targetParent"></param>
    /// <returns></returns>
    public static Transform[] getTransformChilds(Transform targetParent)
    {
        Transform[] tempTrs = new Transform[targetParent.childCount];
        for (int i = 0; i < targetParent.childCount; i++)
        {
            tempTrs[i] = targetParent.GetChild(i);
        }
        return tempTrs;
    }

    /// <summary>
    /// 获取互不相同的随机Index
    /// </summary>
    /// <param name="min">最小Index</param>
    /// <param name="max">最大Index(不包含)</param>
    /// <param name="count">抽取Index数</param>
    /// <returns></returns>
    public static int[] getRandoms(int min, int max, int count)
    {
        int[] maxArray = new int[max];
        for (int i = 0; i < max; i++)
        {
            maxArray[i] = min + i;
        }
        int[] rArray = new int[count];
        System.Random rd = new System.Random();
        int temp = max;
        for (int i = 0; i < count; i++)
        {
            int tIndex = rd.Next(0, temp);
            rArray[i] = maxArray[tIndex];
            maxArray[tIndex] = maxArray[--temp];
        }
        return rArray;
    }


    #endregion

    #region 少用的小工具 ┗( ▔, ▔ )┛


    /// <summary>
    /// 合并Mesh工具(子物体用同种材质，不然材质赋予出错)
    /// </summary>
    /// <param name="parentTr"></param>
    public static void MeshCombine(Transform parentTr)
    {
        if (parentTr.GetComponent<MeshFilter>() == null)
        {
            parentTr.gameObject.AddComponent<MeshFilter>();
            parentTr.gameObject.AddComponent<MeshRenderer>();
        }

        MeshFilter[] meshFilters = parentTr.GetComponentsInChildren<MeshFilter>();       //获取自身和所有子物体中所有MeshFilter组件
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];    //新建CombineInstance数组


        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = parentTr.worldToLocalMatrix * meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].transform.GetComponent<MeshRenderer>().enabled = false;
            meshFilters[i].gameObject.SetActive(false);
        }

        parentTr.GetComponent<MeshFilter>().mesh = new Mesh();
        parentTr.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);       //合并
        parentTr.gameObject.SetActive(true);
        parentTr.GetComponent<MeshRenderer>().enabled = true;
    }



    #endregion


}
