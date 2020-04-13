/****************************************************
    文件：MathTool.cs
    功能：计算工具类
*****************************************************/

using UnityEngine;

public class MathTool : MonoBehaviour
{
    public static float piDivide180 = Mathf.PI / 180;  
  
    public static bool IsFacingRight(Transform t)  
    {  
        if (t.localEulerAngles.y > 0) return false;  
        else return true;  
    }  
  
    public static void FacingRight(Transform t)  
    {  
        t.localEulerAngles = new Vector3(0, 0, 0);  
    }  
  
    public static void FacingLeft(Transform t)  
    {  
        t.localEulerAngles = new Vector3(0, 180, 0);  
    }  
    
    /// <summary>
    ///  Vector3 转 Vector2
    /// </summary>
    public static Vector2 GetVector2(Vector3 a)  
    {  
        Vector2 posA = new Vector2(a.x, a.z);  
        return posA;  
    }  
    /// <summary>
    /// 获取2维距离
    /// </summary>
    public static float GetDistance(Transform a, Transform b)  
    {  
        Vector2 posA = GetVector2(a.position);  
        Vector2 posB = GetVector2(b.position);  
        return Vector2.Distance(posA, posB);  
    } 
    public static float GetDistance(Vector3 a, Vector3 b)  
    {  
        Vector2 posA = GetVector2(a);  
        Vector2 posB = GetVector2(b);  
        return Vector2.Distance(posA, posB);  
    }  

    /// <summary>
    /// 获取2维平面向量方向
    /// </summary>
    public static Vector2 GetDirection(Transform a, Transform b)  
    {  
        Vector2 posA = GetVector2(a.position);  
        Vector2 posB = GetVector2(b.position);  
        return posB - posA;  
    }  
}
