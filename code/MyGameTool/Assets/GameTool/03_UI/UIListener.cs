/****************************************************
    文件：UIListener.cs
    功能：UI事件监听
*****************************************************/
using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class UIListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    public Action<PointerEventData> onClickDown;
    public Action<PointerEventData> onClickUp;
    public Action<PointerEventData> onClickDrag;


    public void OnDrag(PointerEventData eventData)
    {
        if (onClickDrag != null)
            onClickDrag(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onClickDown != null)
            onClickDown(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onClickUp != null)
            onClickUp(eventData);
    }
}