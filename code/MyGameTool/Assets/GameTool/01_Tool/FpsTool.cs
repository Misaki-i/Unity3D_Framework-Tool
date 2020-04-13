/****************************************************
    功能：游戏帧率显示
*****************************************************/
using UnityEngine;

public class FpsTool : MonoBehaviour
{
    [Header("帧率显示开关")]
    public bool IsOpenFps;
    [Header("帧率修改（关闭垂直）")]
    public int targetFPS;
    private float m_LastUpdateShowTime = 0f;    //上一次更新帧率的时间;

    private float m_UpdateShowDeltaTime = 1f;   //刷新帧率间隔;

    private int m_FrameUpdate = 0;              //帧数;

    private float m_FPS = 0;
    
    GUIStyle fontStyle;
    
    private void Awake() {
        Application.targetFrameRate = targetFPS;
    }
    private void Start() {
        m_LastUpdateShowTime = Time.realtimeSinceStartup;
        
        fontStyle = new GUIStyle();
        fontStyle.normal.background = null;    //设置背景填充
        fontStyle.normal.textColor= Color.blue;   //设置字体颜色
        fontStyle.fontSize = 40;       //字体大小

    }
    private void Update() {
        
        if (IsOpenFps)
        {
            m_FrameUpdate++;
            if (Time.realtimeSinceStartup - m_LastUpdateShowTime >= m_UpdateShowDeltaTime)
            {
                m_FPS = m_FrameUpdate / (Time.realtimeSinceStartup - m_LastUpdateShowTime);
                m_FrameUpdate = 0;
                m_LastUpdateShowTime = Time.realtimeSinceStartup;
            }
        }
    }
    private void OnGUI() {
        //帧率显示
        if(IsOpenFps){
            GUI.Label(new Rect(Screen.width / 2 + 100, 0, 100, 100), "FPS: " + m_FPS.ToString("0.0"), fontStyle);
        }
    }
    
    
    
}
