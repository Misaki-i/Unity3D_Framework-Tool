/****************************************************
    功能：适配屏幕
*****************************************************/
using UnityEngine;

public enum ScreenType
{
    /// <summary>
    /// 标准  1080x1920    长宽比  1.77777
    /// </summary>
    Screen_Standard,
    /// <summary>
    /// 1536x2048
    /// 大多数平板  1.3333333    小于1.6
    /// </summary>
    Screen_Pad,
    /// <summary>
    /// 1125x2436
    /// iphoneX类型  全面屏 大于等于2 
    /// </summary>
    Screen_2k,

}
public class DeviceUtil : MonoBehaviour
{
    static DeviceUtil instance;

    public float screen_2k_scale = 0.82f;

    [HideInInspector]
    public ScreenType curScreenType;
    public static DeviceUtil getInst()
    {
        return instance;
    }
    void Awake()
    {
        instance = this;

        setScreenType();
    }

    void setScreenType()
    {
        // curScreenType = ScreenType.Screen_Standard;
        float bl = Screen.height * 1.0f / Screen.width;
        if (bl > 1.7 && bl < 2)
        {
            curScreenType = ScreenType.Screen_Standard;
        }
        else if (bl >= 2)
        {
            curScreenType = ScreenType.Screen_2k;
        }
        else if (bl <= 1.7)
        {
            curScreenType = ScreenType.Screen_Pad;
        }
        else
        {
            curScreenType = ScreenType.Screen_Standard;
        }
    }

    // 特殊全面屏
    public bool isSpecialScreen_2k()
    {
        float bl = Screen.height * 1.0f / Screen.width;
        if (bl >= 2 && bl < 2436 * 1.0f / 1125)
        {
            return true;
        }
        return false;
    }


    public float getScreenScale()
    {
        float sc = 1920 * 1.0f / 1080 / (Screen.height * 1.0f / Screen.width);

        if (sc > 1 && DeviceUtil.getInst().curScreenType != ScreenType.Screen_Pad) sc = 1;

        return sc;
    }

    public float getCameraSize()
    {
        return 1920*1.0f/200/getScreenScale();
    }
}

