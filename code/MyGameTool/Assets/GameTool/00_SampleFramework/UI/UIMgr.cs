/****************************************************
    文件：UIMgr.cs
	功能：UI面板管理器
*****************************************************/
using System.Collections.Generic;
using UnityEngine;

public enum UI_Layer
{
    Bot,
    Mid,
    Top,
    System
}

public class UIMgr : MonoBehaviour
{
    public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();


    Transform bot;
    Transform mid;
    Transform top;
    Transform system;

    public UIMgr()
    {
        GameObject obj = ResMgr.getInst().Load<GameObject>("UI/Canvas");
        Transform canvas = obj.transform;
        GameObject.DontDestroyOnLoad(obj);

        //
        bot = canvas.Find("Bot");
        mid = canvas.Find("Mid");
        top = canvas.Find("Top");
        system = canvas.Find("System");


    }

    public void ShowPanel(string panelName,UI_Layer layer)
    {
        ResMgr.getInst().LoadAsync<GameObject>("UI/"+ panelName, (obj) =>
        {
            Transform parent = bot;
            switch (layer)
            {
                case UI_Layer.Bot:
                    parent = bot;
                    break;
                case UI_Layer.Mid:
                    parent = mid;
                    break;
                case UI_Layer.Top:
                    parent = top;
                    break;
                case UI_Layer.System:
                    parent = system;
                    break;
            }

            //初始化设置面板位置
            obj.transform.SetParent(parent);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            obj.GetComponent<BasePanel>().Show();
        });
    }
    public void HidePanel(string panelName)
    {

    }


}
