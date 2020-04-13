/****************************************************
    文件：DebugDistanceAngleEditor.cs
    功能：Inpector视图设置范围显示的
*****************************************************/

using UnityEngine;  
using System.Collections;  
using UnityEditor;  
  
[CustomEditor(typeof(DebugDistanceAngle))]  
[ExecuteInEditMode]  
public class DebugDistanceAngleEditor : Editor {  
  
    private bool canGetDragEvent;  
  
    public override void OnInspectorGUI()  
    {  
        DebugDistanceAngle d = target as DebugDistanceAngle;  
  
        EditorGUILayout.LabelField("请把角色放在Resources文件夹中，点击Play之后将通过Resources进行加载");  
        canGetDragEvent = EditorGUILayout.Toggle("通过拖拽获取角色路径", canGetDragEvent);  
        EditorGUILayout.LabelField("角色路径");  
        if (canGetDragEvent)  
        {  
            if (Event.current.type == EventType.DragExited)  
            {  
                if (DragAndDrop.paths != null && DragAndDrop.paths.Length > 0)  
                {  
                    string s = DragAndDrop.paths[0];  
                    s = s.Substring(s.IndexOf("Resources/"));   
                    s = s.Remove(0, "Resources/".Length);  
                    s = s.Substring(0, s.IndexOf('.'));  
                    d.modelName = s;  
                }  
            }  
        }  
        d.modelName = EditorGUILayout.TextArea(d.modelName);  
  
        // d.animationName = EditorGUILayout.TextField("动画名字", d.animationName);  
  
        EditorGUILayout.LabelField("播放进度");  
        d.time = EditorGUILayout.Slider(d.time, 0, 1);  
  
        d.drawType = (DrawType)EditorGUILayout.EnumPopup("绘制类型" ,d.drawType);  
        Debug.Log(d.drawType);
        switch (d.drawType)  
        {  
            
            case DrawType.DrawSector:  
                EditorGUILayout.LabelField("绘制空心扇形");  
                EditorGUILayout.LabelField("角度");  
                d.angle = EditorGUILayout.Slider(d.angle, 0, 180);  
                EditorGUILayout.LabelField("半径");  
                d.radius = EditorGUILayout.Slider(d.radius, 0, 10);  
                break;  
  
  
            case DrawType.DrawCircle:  
                EditorGUILayout.LabelField("绘制空心圆");  
                EditorGUILayout.LabelField("半径");  
                d.radius = EditorGUILayout.Slider(d.radius, 0, 10);  
                break;  
  
  
            case DrawType.DrawRectangle:  
                EditorGUILayout.LabelField("绘制空心长方形");  
                EditorGUILayout.LabelField("长度");  
                d.length = EditorGUILayout.Slider(d.length, 0, 10);  
                EditorGUILayout.LabelField("宽度");  
                d.width = EditorGUILayout.Slider(d.width, 0, 10);  
                break;  
  
  
            case DrawType.DrawRectangle2D:  
                EditorGUILayout.LabelField("绘制空心长方形2D");  
                EditorGUILayout.LabelField("距离");  
                d.distance = EditorGUILayout.Slider(d.distance, 0, 10);  
                EditorGUILayout.LabelField("长度");  
                d.length = EditorGUILayout.Slider(d.length, 0, 10);  
                EditorGUILayout.LabelField("宽度");  
                d.width = EditorGUILayout.Slider(d.width, 0, 10);  
                break;  
  
  
            case DrawType.DrawSectorSolid:  
                EditorGUILayout.LabelField("绘制实心扇形");  
                EditorGUILayout.LabelField("角度");  
                d.angle = EditorGUILayout.Slider(d.angle, 0, 180);  
                EditorGUILayout.LabelField("半径");  
                d.radius = EditorGUILayout.Slider(d.radius, 0, 10);  
                break;  
  
  
            case DrawType.DrawCircleSolid:  
                EditorGUILayout.LabelField("绘制实心圆");  
                EditorGUILayout.LabelField("半径");  
                d.radius = EditorGUILayout.Slider(d.radius, 0, 10);  
                break;  
  
  
            case DrawType.DrawRectangleSolid:  
                EditorGUILayout.LabelField("绘制实心长方形");  
                EditorGUILayout.LabelField("长度");  
                d.length = EditorGUILayout.Slider(d.length, 0, 10);  
                EditorGUILayout.LabelField("宽度");  
                d.width = EditorGUILayout.Slider(d.width, 0, 10);  
                break;  
  
  
            case DrawType.DrawRectangleSolid2D:  
                EditorGUILayout.LabelField("绘制实心长方形2D");  
                EditorGUILayout.LabelField("距离");  
                d.distance = EditorGUILayout.Slider(d.distance, 0, 10);  
                EditorGUILayout.LabelField("长度");  
                d.length = EditorGUILayout.Slider(d.length, 0, 10);  
                EditorGUILayout.LabelField("宽度");  
                d.width = EditorGUILayout.Slider(d.width, 0, 10);  
                break;  
  
  
            default:  
                break;  
        }  
        
        if(tempGo != null){
            DestroyImmediate(tempGo);
        }
        
        
        GameObject go = d.gameObject;
        tempGo = null;
         switch (d.drawType)  
        {  
            case DrawType.DrawSector :  
                DrawTool.DrawSector(go.transform, go.transform.position, d.angle,d.radius);  
                break;  
            case DrawType.DrawCircle:  
                DrawTool.DrawCircle(go.transform, go.transform.position, d.radius);  
                break;  
            case DrawType.DrawRectangle:  
                DrawTool.DrawRectangle(go.transform, go.transform.position, d.length, d.width);  
                break;  
            case DrawType.DrawRectangle2D:  
                DrawTool.DrawRectangle2D(go.transform, d.distance, d.length, d.width);  
                break;  
            case DrawType.DrawSectorSolid:  
                tempGo = DrawTool.DrawSectorSolid(go.transform, go.transform.position,d.angle, d.radius);  
                break;  
            case DrawType.DrawCircleSolid:  
                tempGo = DrawTool.DrawCircleSolid(go.transform, go.transform.position, d.radius);  
                break;  
            case DrawType.DrawRectangleSolid:  
                tempGo = DrawTool.DrawRectangleSolid(go.transform, go.transform.position, d.length, d.width);  
                break;  
            case DrawType.DrawRectangleSolid2D:  
                //  DrawTool.DrawRectangleSolid2D(go.transform, d.distance, d.length, d.width);  
                break;  
            default :  
                break;  
        }  
        
        
        
        
    }  
    GameObject tempGo;
    
    
    
    
    
}  
