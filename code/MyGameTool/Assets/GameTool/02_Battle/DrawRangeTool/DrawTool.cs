/****************************************************
    文件：DrawTool.cs
    功能：绘制攻击范围工具类
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class DrawTool : MonoBehaviour
{
    
    private static LineRenderer GetOrAddLineRenderer(Transform t)  
    {  
        LineRenderer lr = t.GetComponent<LineRenderer>();  
        if (lr == null)  
        {  
            lr = t.gameObject.AddComponent<LineRenderer>();  
        }  
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        return lr;  
    }  
  
    public static void DrawLine(Transform t, Vector3 start, Vector3 end)  
    {  
        LineRenderer lr = GetOrAddLineRenderer(t);  
        lr.positionCount = 2;
        lr.SetPosition(0, start);  
        lr.SetPosition(1, end);  
    }  
  
    
    /// <summary>
    /// 绘制空心的扇形圆
    /// </summary>
    /// <param name="t">对象</param>
    /// <param name="center">中心点</param>
    /// <param name="angle">角度</param>
    /// <param name="radius">半径</param>
    public static void DrawSector(Transform t, Vector3 center, float angle, float radius)  
    {  
        LineRenderer lr = GetOrAddLineRenderer(t);  
        int pointAmount = 100;//点的数目，值越大曲线越平滑    
        float eachAngle = angle / pointAmount;  
        Vector3 forward = t.forward;  
  
        lr.positionCount = pointAmount;
        lr.SetPosition(0, center);  
        lr.SetPosition(pointAmount - 1, center);  
  
        for (int i = 1; i < pointAmount - 1; i++)  
        {  
            Vector3 pos = Quaternion.Euler(0f, -angle / 2 + eachAngle * (i - 1), 0f) * forward * radius + center;  
            lr.SetPosition(i, pos);  
        }  
    }  
  
    /// <summary>
    /// 绘制空心圆
    /// </summary>
    /// <param name="t">对象</param>
    /// <param name="center">中心点</param>
    /// <param name="radius">角度</param>    
    public static void DrawCircle(Transform t, Vector3 center, float radius,Material material = null)  
    {  
        LineRenderer lr = GetOrAddLineRenderer(t);  
        int pointAmount = 100;//点的数目，值越大曲线越平滑    
        float eachAngle = 360f / pointAmount;  
        Vector3 forward = t.forward;  
  
        lr.positionCount = pointAmount + 1;
        Material tempMat = Resources.Load<Material>("Materials/attackRange");
        material = tempMat;
        if(material != null){
            lr.material = material;
        }
  
        lr.widthMultiplier = 0.13f;
        for (int i = 0; i <= pointAmount; i++)  
        {  
            Vector3 pos = Quaternion.Euler(0f, eachAngle * i, 0f) * forward * radius + center;  
            lr.SetPosition(i, pos);  
        }  
    }  
  
    
    /// <summary>
    ///   绘制空心长方形  
    ///   以长方形的底边中点为攻击方位置(从俯视角度来看)
    /// </summary>
    /// <param name="t">对象</param>
    /// <param name="bottomMiddle">绘制开始点</param>
    /// <param name="length">长度</param>
    /// <param name="width">宽度</param>  
    public static void DrawRectangle(Transform t, Vector3 bottomMiddle, float length, float width)  
    {  
        LineRenderer lr = GetOrAddLineRenderer(t);  
        lr.positionCount = 5;
        
        lr.SetPosition(0, bottomMiddle - t.right * (width / 2));  
        lr.SetPosition(1, bottomMiddle - t.right * (width / 2) + t.forward * length);  
        lr.SetPosition(2, bottomMiddle + t.right * (width / 2) + t.forward * length);  
        lr.SetPosition(3, bottomMiddle + t.right * (width / 2));  
        lr.SetPosition(4, bottomMiddle - t.right * (width / 2));  
    }  
  
    
    /// <summary>
    ///  绘制空心长方形2D  
    //   distance指的是这个长方形与Transform t的中心点的距离  
    /// </summary>
    /// <param name="t"></param>
    /// <param name="distance"></param>
    /// <param name="length"></param>
    /// <param name="width"></param>
    public static void DrawRectangle2D(Transform t, float distance, float length, float width)  
    {  
        LineRenderer lr = GetOrAddLineRenderer(t);  
        lr.positionCount = 5;
          
        if (MathTool.IsFacingRight(t))  
        {  
            Vector2 forwardMiddle = new Vector2(t.position.x + distance, t.position.y);  
            lr.SetPosition(0, forwardMiddle + new Vector2(0, width / 2));  
            lr.SetPosition(1, forwardMiddle + new Vector2(length, width / 2));  
            lr.SetPosition(2, forwardMiddle + new Vector2(length, -width / 2));  
            lr.SetPosition(3, forwardMiddle + new Vector2(0, -width / 2));  
            lr.SetPosition(4, forwardMiddle + new Vector2(0, width / 2));  
        }  
        else  
        {  
            Vector2 forwardMiddle = new Vector2(t.position.x - distance, t.position.y);  
            lr.SetPosition(0, forwardMiddle + new Vector2(0, width / 2));  
            lr.SetPosition(1, forwardMiddle + new Vector2(-length, width / 2));  
            lr.SetPosition(2, forwardMiddle + new Vector2(-length, -width / 2));  
            lr.SetPosition(3, forwardMiddle + new Vector2(0, -width / 2));  
            lr.SetPosition(4, forwardMiddle + new Vector2(0, width / 2));  
        }  
    }  
  
 
  
    //绘制实心扇形    
    public static GameObject DrawSectorSolid(Transform t, Vector3 center, float angle, float radius)  
    {  
        int pointAmount = 100;//点的数目，值越大曲线越平滑    
        float eachAngle = angle / pointAmount;  
        Vector3 forward = t.forward;  
  
        List<Vector3> vertices = new List<Vector3>();  
        vertices.Add(center);  
  
        for (int i = 1; i < pointAmount - 1; i++)  
        {  
            Vector3 pos = Quaternion.Euler(0f, -angle / 2 + eachAngle * (i - 1), 0f) * forward * radius + center;  
            vertices.Add(pos);  
        }  
  
        GameObject tempTr = CreateMesh(vertices);  
        
        tempTr.transform.SetParent(t);
        
        return tempTr;
    }  
  
    //绘制实心圆    
    public static GameObject DrawCircleSolid(Transform t, Vector3 center, float radius,bool isInDrag = false)  
    {  
        int pointAmount = 100;//点的数目，值越大曲线越平滑    
        float eachAngle = 360f / pointAmount;  
        Vector3 forward = t.forward;  
  
        List<Vector3> vertices = new List<Vector3>();  
  
        for (int i = 0; i <= pointAmount; i++)  
        {  
            Vector3 pos = Quaternion.Euler(0f, eachAngle * i, 0f) * forward * radius + center;  
            vertices.Add(pos);  
        }  
        GameObject temptr = null;
        if (isInDrag)
        {
            temptr = CreateMeshInDrag(vertices);
        }else
        {
            temptr = CreateMesh(vertices);
        }
  
         
        temptr.transform.SetParent(t);
        
        //适应地图的，添加测试用
        Vector3 tempPos = temptr.transform.position;
        temptr.transform.position = new Vector3(tempPos.x, -0.8f, tempPos.z);
        return temptr;
    }  
  
    //绘制实心长方形  
    //以长方形的底边中点为攻击方位置(从俯视角度来看)  
    public static GameObject DrawRectangleSolid(Transform t, Vector3 bottomMiddle, float length, float width)  
    {  
        List<Vector3> vertices = new List<Vector3>();  
  
        vertices.Add(bottomMiddle - t.right * (width / 2));  
        vertices.Add(bottomMiddle - t.right * (width / 2) + t.forward * length);  
        vertices.Add(bottomMiddle + t.right * (width / 2) + t.forward * length);  
        vertices.Add(bottomMiddle + t.right * (width / 2));  
  
        GameObject tempTr = CreateMesh(vertices);  
        tempTr.transform.SetParent(t);
        //适应地图的，添加测试用
        // Vector3 tempPos = tempTr.transform.localPosition;
        // tempTr.transform.localPosition = new Vector3(tempPos.x, -0.23f, tempPos.z);
        return tempTr;
    }

    /// <summary>
    /// 绘制实心正方形范围
    /// </summary>
    /// <param name="t"></param>
    /// <param name="MiddleVec"></param>
    /// <param name="length"></param>
    public static GameObject DrawRectangleSolidMiddle(Transform t, Vector3 MiddleVec, float length,bool isInDrag = false, float delatY = 0){
        List<Vector3> vertices = new List<Vector3>();
        length *= 2;

        vertices.Add(MiddleVec - t.right * (length / 2) + t.forward * (length / 2));
        vertices.Add(MiddleVec + t.right * (length / 2) + t.forward * (length / 2));
        vertices.Add(MiddleVec + t.right * (length / 2) - t.forward * (length / 2));
        vertices.Add(MiddleVec - t.right * (length / 2) - t.forward * (length / 2));
        GameObject tempTr = null;
        if (isInDrag)
        {
            tempTr = CreateMeshInDrag(vertices);
        }else{
            // Debug.Log("?/////??????????????");
            tempTr = CreateMesh(vertices);
        }
        
        tempTr.transform.SetParent(t);
        tempTr.transform.localPosition += new Vector3(0, 0f, 0);
        //适应地图的，添加测试用
        Vector3 tempPos = tempTr.transform.position;
        tempTr.transform.position = new Vector3(tempPos.x, -0.8f, tempPos.z);

        return tempTr;
    }
    
  
    //绘制实心长方形2D  
    //distance指的是这个长方形与Transform t的中心点的距离  
    public static void DrawRectangleSolid2D(Transform t, float distance, float length, float width)  
    {  
        List<Vector3> vertices = new List<Vector3>();  
  
        if (MathTool.IsFacingRight(t))  
        {  
            Vector3 forwardMiddle = new Vector3(t.position.x + distance, t.position.y);  
            vertices.Add(forwardMiddle + new Vector3(0, width / 2));  
            vertices.Add(forwardMiddle + new Vector3(length, width / 2));  
            vertices.Add(forwardMiddle + new Vector3(length, -width / 2));  
            vertices.Add(forwardMiddle + new Vector3(0, -width / 2));  
        }  
        else  
        {  
            //看不到颜色但点击mesh可以看到形状  
            Vector3 forwardMiddle = new Vector3(t.position.x - distance, t.position.y);  
            vertices.Add(forwardMiddle + new Vector3(0, width / 2));  
            vertices.Add(forwardMiddle + new Vector3(-length, width / 2));  
            vertices.Add(forwardMiddle + new Vector3(-length, -width / 2));  
            vertices.Add(forwardMiddle + new Vector3(0, -width / 2));  
        }  
  
        CreateMesh(vertices);  
    }  
    
     
    // public static GameObject go;  
    // public static MeshFilter mf;  
    // public static MeshRenderer mr;  
    // public static Shader shader;  
    /// <summary>
    /// 根据顶点创建Mesh
    /// </summary>
    /// <param name="vertices"></param>
    /// <returns></returns>
    private static GameObject CreateMesh(List<Vector3> vertices)  
    {

        GameObject go = null;
        MeshFilter mf = null;
        MeshRenderer mr = null;
        // Shader shader = null;

        int[] triangles;
        Mesh mesh = new Mesh();  
  
        int triangleAmount = vertices.Count - 2;  
        triangles = new int[3 * triangleAmount];  
  
        //根据三角形的个数，来计算绘制三角形的顶点顺序（索引）      
        //顺序必须为顺时针或者逆时针      
        for (int i = 0; i < triangleAmount; i++)  
        {  
            triangles[3 * i] = 0;//固定第一个点      
            triangles[3 * i + 1] = i + 1;  
            triangles[3 * i + 2] = i + 2;  
        }
        Material tempMat = Resources.Load<Material>("Materials/attackRange");
        if (go == null)  
        {  
            go = new GameObject("mesh");  
            go.transform.position = new Vector3(0, 0f, 0);//让绘制的图形上升一点，防止被地面遮挡  
            mf = go.AddComponent<MeshFilter>();  
            mr = go.AddComponent<MeshRenderer>();
            // shader = Shader.Find("UI/Default");
        }  
  
        mesh.vertices = vertices.ToArray();  
        mesh.triangles = triangles;  
  
        mf.mesh = mesh;  
        mr.sharedMaterial = tempMat;
        
        // mr.material.shader = shader;  
        // mr.material.color = new Color(1,0,0,0.4f);  
     
        return go;  
    }  



    private static GameObject CreateMeshInDrag(List<Vector3> vertices){
        GameObject go = null;
        MeshFilter mf = null;
        MeshRenderer mr = null;
        Shader shader = null;

        int[] triangles;
        Mesh mesh = new Mesh();  
  
        int triangleAmount = vertices.Count - 2;  
        triangles = new int[3 * triangleAmount];  
  
        //根据三角形的个数，来计算绘制三角形的顶点顺序（索引）      
        //顺序必须为顺时针或者逆时针      
        for (int i = 0; i < triangleAmount; i++)  
        {  
            triangles[3 * i] = 0;//固定第一个点      
            triangles[3 * i + 1] = i + 1;  
            triangles[3 * i + 2] = i + 2;  
        }
        // Material tempMat = Resources.Load<Material>("Materials/attackRange");
        if (go == null)  
        {  
            go = new GameObject("hhhh");  
            go.transform.position = new Vector3(0, -0f, 0);//让绘制的图形上升一点，防止被地面遮挡  
            mf = go.AddComponent<MeshFilter>();  
            mr = go.AddComponent<MeshRenderer>();
            // shader = Shader.Find("UI/Default");
        }  
  
        mesh.vertices = vertices.ToArray();  
        mesh.triangles = triangles;  
  
        mf.mesh = mesh;  
        mr.sharedMaterial = Resources.Load<Material>("Materials/hhhh");
        
        // mr.material.shader = shader;  
        // mr.material.color = new Color(1,0,0,0.4f);  
     
        return go;

    }
    
    
}
