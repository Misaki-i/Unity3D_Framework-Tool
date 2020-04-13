/****************************************************
    文件：BasePanel.cs
	功能：UI面板基类
*****************************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    //里式转化原则 存储所有控件
    Dictionary<string, List<UIBehaviour>> controllDic = new Dictionary<string, List<UIBehaviour>>();

    private void Start()
    {
        findChildrenContol<Button>();
        findChildrenContol<Image>();
        findChildrenContol<Text>();
        findChildrenContol<Toggle>();
        findChildrenContol<ScrollRect>();
        findChildrenContol<Scrollbar>();

    }

    protected T getControl<T>(string controlName) where T : UIBehaviour
    {
        if (controllDic.ContainsKey(controlName))
        {
            for (int i = 0; i < controllDic[controlName].Count; i++){
                if(controllDic[controlName][i] is T)
                {
                    return controllDic[controlName][i] as T;
                }
            }
        }
        return null;
    }


    void findChildrenContol<T>() where T : UIBehaviour
    {
        T[] controls = GetComponentsInChildren<T>();
        string objName;
        for (int i = 0; i < controls.Length; i++)
        {
            objName = controls[i].gameObject.name;
            if (controllDic.ContainsKey(objName))
            {
                controllDic[objName].Add(controls[i]);
            }
            else
            {
                controllDic.Add(objName, new List<UIBehaviour>() { controls[i] });
            }
        }
    }

    public virtual void Show() { }
    public virtual void Hide() { }




}
