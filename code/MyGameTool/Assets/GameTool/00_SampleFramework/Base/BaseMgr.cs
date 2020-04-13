/****************************************************
    文件：BaseMgr.cs
	功能：单例模板
*****************************************************/
using UnityEngine;

public class BaseMgr<T> where T:new()
{
    static T inst;
    public static T getInst()
    {
        if (inst == null) inst = new T();
        return inst;
    }
}
