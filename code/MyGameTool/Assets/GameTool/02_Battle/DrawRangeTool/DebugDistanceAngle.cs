﻿/****************************************************
    文件：DebugDistanceAngle.cs
    功能：Debug绘制范围
*****************************************************/

using UnityEngine;
public enum DrawType  
{  
    DrawSector,              //绘制空心扇形  
    DrawCircle,              //绘制空心圆  
    DrawRectangle,           //绘制空心长方形  
    DrawRectangle2D,         //绘制空心长方形2D  
    DrawSectorSolid,         //绘制实心扇形  
    DrawCircleSolid,         //绘制实心圆  
    DrawRectangleSolid,      //绘制实心长方形  
    DrawRectangleSolid2D,    //绘制实心长方形2D  
}  

/// <summary>  
/// 要把动画上的脚本(继承StateMachineBehaviour)取消勾选  
/// </summary>  
public class DebugDistanceAngle : MonoBehaviour {  
  
    public GameObject go;  
    private Animator animator;  
  
    public string modelName;//角色路径  
    public string animationName;//动画名字,即Animator窗口看到的动画名字  
  
    public float time;//播放进度  
    public DrawType drawType;  
  
    public float distance;  
    public float angle;  
    public float radius;  
    public float length;  
    public float width;  
  
    void Start ()   
    {  
        // if (modelName.Length > 0)  
        // {  
        //     go = Instantiate(Resources.Load<GameObject>(modelName));  
        //     go.transform.localPosition = Vector3.zero;  
        //     animator = go.GetComponent<Animator>();  
        // }  
        
        go = this.gameObject;
    }  
      
    void Update ()   
    {  
        if ((go != null) && (time != 0))  
        {  
            animator.Play(animationName, 0, time);   
        }  
  
        switch (drawType)  
        {  
            case DrawType.DrawSector :  
                DrawTool.DrawSector(go.transform, go.transform.position, angle, radius);  
                break;  
            case DrawType.DrawCircle:  
                DrawTool.DrawCircle(go.transform, go.transform.position, radius);  
                break;  
            case DrawType.DrawRectangle:  
                DrawTool.DrawRectangle(go.transform, go.transform.position, length, width);  
                break;  
            case DrawType.DrawRectangle2D:  
                DrawTool.DrawRectangle2D(go.transform, distance, length, width);  
                break;  
            case DrawType.DrawSectorSolid:  
                DrawTool.DrawSectorSolid(go.transform, go.transform.position, angle, radius);  
                break;  
            case DrawType.DrawCircleSolid:  
                DrawTool.DrawCircleSolid(go.transform, go.transform.position, radius);  
                break;  
            case DrawType.DrawRectangleSolid:  
                DrawTool.DrawRectangleSolid(go.transform, go.transform.position, length, width);  
                break;  
            case DrawType.DrawRectangleSolid2D:  
                DrawTool.DrawRectangleSolid2D(go.transform, distance, length, width);  
                break;  
            default :  
                break;  
        }  
    }  
  
}  