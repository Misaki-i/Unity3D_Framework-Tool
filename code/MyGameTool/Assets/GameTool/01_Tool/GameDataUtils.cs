/****************************************************
    功能：和游戏数据相关的实用类
*****************************************************/
using UnityEngine;
using System;
using System.Collections.Generic;



// 和游戏数据相关的实用类 //
public class GameDataUtils
{
//     // 单位
//     public static List<string> unitLst = new List<string>(){"",
// "K","M","B","T",
// "aa","ab","ac","ad","ae","af","ag","ah","ai","aj","ak","al","am","an","ao","ap","aq","ar","as","at","au","av","aw","ax","ay","az",
// "ba","bb","bc","bd","be","bf","bg","bh","bi","bj","bk","bl","bm","bn","bo","bp","bq","br","bs","bt","bu","bv","bw","bx","by","bz",
// "ca","cb","cc","cd","ce","cf","cg","ch","ci","cj","ck","cl","cm","cn","co","cp","cq","cr","cs","ct","cu","cv","cw","cx","cy","cz",
// "da","db","dc","dd","de","df","dg","dh","di","dj","dk","dl","dm","dn","do","dp","dq","dr","ds","dt","du","dv","dw","dx","dy","dz",
// };

    static string tmpStr;
    // 将数据转换成bool类型 //
    public static bool ReadBool(object data)
    {
        string str = data.ToString();
        if (str == "1" || str == "true")
        {
            return true;
        }

        return false;
    }

    // 将数据转换成int类型 //
    public static int ReadInt(object data)
    {
        if (data == null)
        {
            return 0;
        }
        int nIntData = 0;
        try
        {
            nIntData = System.Convert.ToInt32(data);
        }
        catch (Exception e)
        {
            //Debug.Log(e.Message);
            return 0;
        }
        return nIntData;
    }

    // 将数据转换成float类型 //
    public static float ReadFloat(object data)
    {
        if (data == null)
        {
            return 0;
        }
        float fResult;
        if (float.TryParse(data.ToString(), out fResult))
        {
            return fResult;
        }
        return 0f;
    }

    // 将数据转换成string list //
    public static List<string> ReadStringList(object data)
    {
        List<string> result = new List<string>();
        string[] strData = data.ToString().Split('|');
        for (int i = 0; i < strData.Length; i++)
        {
            if (strData[i].Length > 0)
            {
                result.Add(strData[i]);
            }
        }

        return result;
    }

    // 将数据转换成int list //
    public static List<int> ReadIntList(object data)
    {
        List<int> result = new List<int>();
        string[] strData = data.ToString().Split('|');
        for (int i = 0; i < strData.Length; i++)
        {
            if (strData[i].Length > 0)
            {
                result.Add(ReadInt(strData[i]));
            }
        }

        return result;
    }

    //根据自定义字符 Y 转换 int list//
    public static List<int> ReadIntListY(object data, char[] Y)
    {
        List<int> result = new List<int>();
        string[] strData = data.ToString().Split(Y);
        for (int i = 0; i < strData.Length; i++)
        {
            if (strData[i].Length > 0)
            {
                result.Add(ReadInt(strData[i]));
            }
        }

        return result;
    }

    // 将数据转换成float list //
    public static List<float> ReadFloatList(object data)
    {
        List<float> result = new List<float>();
        string[] strData = data.ToString().Split('|');
        for (int i = 0; i < strData.Length; i++)
        {
            if (strData[i].Length > 0)
            {
                result.Add(ReadFloat(strData[i]));
            }
        }

        return result;
    }

    /// <summary>
    /// 转换成Vector2列
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static List<Vector2> ReadVector2List(object data)
    {
        List<Vector2> result = new List<Vector2>();
        string[] strData = data.ToString().Split('|');
        for (int i = 0; i < strData.Length; i++)
        {
            Vector2 v2 = new Vector2();
            if (strData[i].Length > 0)
            {
                v2.x = float.Parse(strData[i].Split('-')[0].ToString());
                v2.y = float.Parse(strData[i].Split('-')[1].ToString());
                result.Add(v2);
            }
        }
        return result;
    }

    /// <summary>
    /// 转换成Vector2
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Vector2 ReadVector2(object data)
    {
        if (data == null)
        {
            return new Vector2(0, 0);
        }
        Vector2 v2Result;

        if (data.ToString().Split('-').Length > 1)
        {
            v2Result.x = float.Parse(data.ToString().Split('-')[0]);
            v2Result.y = float.Parse(data.ToString().Split('-')[1]);
            return v2Result;
        }

        return new Vector2(0, 0);
    }
    /// <summary>
    /// 字符串转Vector3
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static Vector3 ParseVec3(string str){
        str = str.Replace("(", "").Replace(")", "");
        string[] s = str.Split(',');
        return new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
    }
    /// <summary>
    /// 字符串转Vector2
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static Vector2 ParseVec2(string str){
        str = str.Replace("(", "").Replace(")", "");
        string[] s = str.Split(',');
        return new Vector2(float.Parse(s[0]), float.Parse(s[1]));
    }
    
    
    


    // 时间 

    /// <summary>
    /// unix时间戳转换成日期
    /// </summary>
    /// <param name="unixTimeStamp">时间戳（秒）</param>
    /// <returns></returns>
    public static double getTimeStampUnix()
    {
        System.DateTime time = System.DateTime.Now;
        double intResult = 0;
        System.DateTime startTime = System.TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        intResult = (time - startTime).TotalSeconds;
        return intResult;
    }

    public static bool havaSign()
    {
        System.DateTime day = System.DateTime.Now;
        string curDate = day.ToString("yyyyMMdd");
        string lastSignDate = PlayerPrefs.GetString("signDate", "");
        if (curDate == lastSignDate)
        {
            return true;
        }
        return false;
    }

    public static bool havaDoubleSign()
    {
        System.DateTime day = System.DateTime.Now;
        string curDate = day.ToString("yyyyMMdd");
        return PlayerPrefs.GetInt("doubleState" + curDate, 0) != 0;
    }

    // 新的一天
    public static bool isNewDay()
    {
        System.DateTime day = System.DateTime.Now;
        string curDate = day.ToString("yyyyMMdd");
        string lastLoginDate = PlayerPrefs.GetString("lastLoginDate", "");
        if (lastLoginDate != curDate)
        {
            PlayerPrefs.SetString("lastLoginDate", curDate);
            return true;
        }
        return false;
    }

    //是否新的一天
    public static bool isNewDay222(string day){
        if (day == getCurDayString() ) 
        {
            return false;
        }else
        {
            return true;
        }

    }

    public static bool isNeedBallStoreAds()
    {
        System.DateTime day = System.DateTime.Now;
        string curDate = day.ToString("yyyyMMdd");
        string lastLoginDate = PlayerPrefs.GetString("ballStoreAdsDate", "");
        if (lastLoginDate != curDate)
        {
            return true;
        }
        return false;
    }

    public static string getCurDayString()
    {
        System.DateTime day = System.DateTime.Now;
        return day.ToString("yyyyMMdd");
    }

    public static long getCurDayTotalSeconds()
    {
        TimeSpan dalt = new TimeSpan(System.DateTime.Now.Ticks);
        return (long)dalt.TotalSeconds;
    }

    // 获得當前时间戳
    public static long getCurTicks()
    {
        System.DateTime day = System.DateTime.Now;

        return day.Ticks;
    }

    // 获取倒计时时间格式
    public static void getDownTimeStr(int seconds, out string timeStr)
    {
        int h, m, s;
        timeStr = "";
        if (seconds > 0)
        {
            // console.log(times)
            h = Mathf.FloorToInt(seconds / 3600);
            m = Mathf.FloorToInt((seconds % 3600) / 60);
            s = seconds % 60;
            // if (h > 9)
            // {
            //     timeStr = h + ":";
            // }
            // else if (h > 0)
            // {
            //     timeStr = "0" + h + ":";
            // }
            if(h > 0)
                timeStr += h > 9? h + ":":"0" + h + ":";

            timeStr += m > 9 ? m + ":" : "0" + m + ":";

            timeStr += s > 9 ? s + "" : "0" + s;


        }else
        {
            timeStr = "00:00";
        }

    }

    // 获取倒计时时间格式
    public static void getDownTimeDayStr(int seconds, out string timeStr)
    {
        int d,h, m, s;
        timeStr = "";
        if (seconds > 0)
        {
            // console.log(times)
            d = Mathf.FloorToInt(seconds / 86400);
            h = Mathf.FloorToInt((seconds % 86400) / 3600);
            m = Mathf.FloorToInt(seconds % 3600 / 60);
            s = seconds % 60;
            if(d > 0)
                timeStr += d > 9? h + ":":"0" + d + ":";
            if(d > 0 || h > 0){
                timeStr += h > 9? h + ":":"0" + h + ":";
            }
            else{
                timeStr += h > 9? h + ":":"0" + h + ":";
            }

            timeStr += m > 9 ? m + ":" : "0" + m + ":";

            timeStr += s > 9 ? s + "" : "0" + s;


        }else
        {
            timeStr = "00:00";
        }

    }

    //  // 获取倒计时时间格式
    // public static void getTimeStruct(int seconds, out TimeStruct t)
    // {
    //     t.d = Mathf.FloorToInt(seconds / 86400);
    //     t.h = Mathf.FloorToInt((seconds % 86400) / 3600);
    //     t.m = Mathf.FloorToInt(seconds % 3600 / 60);
    //     t.s = seconds % 60;
    // }


         // 获取倒计时时间格式
    public static void getTimeStr(int seconds, out string t)
    {
         int d,h, m, s;
        d = Mathf.FloorToInt(seconds / 86400);
        h = Mathf.FloorToInt((seconds % 86400) / 3600);
        m = Mathf.FloorToInt(seconds % 3600 / 60);
        s = seconds % 60;

         if (d > 0)
        {
           t = d + "d " + h + "h " + m + "m " + s + "s";
        }
        else if (h > 0)
        {
            t = h + "h " + m + "m " + s + "s";
        }
        else
        {
            t = m + "m " + s + "s";
        } 
    }




    



    public static void changeLayer(Transform tr, string layerName)
    {
         if (LayerMask.NameToLayer(layerName)==-1)
        {
            Debug.Log("Layer中不存在,请手动添加LayerName");
            
            return;
        }
        //遍历更改所有子物体layer
        tr.gameObject.layer = LayerMask.NameToLayer(layerName);
        foreach (Transform child in tr)
        {
            child.gameObject.layer = LayerMask.NameToLayer(layerName);
        }
 
    }

}
