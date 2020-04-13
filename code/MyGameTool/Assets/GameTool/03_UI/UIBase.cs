/****************************************************
    文件：UIBase.cs
    功能：UI基类
*****************************************************/

using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIBase : MonoBehaviour
{

    #region /***** ClickEvent **********************************************************************/ 
    /// <summary>
    /// 点击UI事件
    /// </summary>
    /// <param name="go">触碰对象</param>
    /// <param name="cb">回调</param>
    protected void OnClickDown(GameObject go, System.Action<PointerEventData> cb)
    {
        UIListener listener = getOrAddComponet<UIListener>(go);
        listener.onClickDown = cb;
    }
    protected void OnClickUp(GameObject go, System.Action<PointerEventData> cb)
    {
        UIListener listener = getOrAddComponet<UIListener>(go);
        listener.onClickUp = cb;
    }
    protected void OnClickDrag(GameObject go, System.Action<PointerEventData> cb)
    {
        UIListener listener = getOrAddComponet<UIListener>(go);
        listener.onClickDrag = cb;
    }
    #endregion

    #region /***** UI弹窗动画 **********************************************************************/

    /// <summary>
    /// 开启弹窗动画（alpha 0->1）
    /// </summary>
    /// <param name="target">弹窗对象</param>
    /// <param name="callBack">结束回调</param>
    public void DailogAnimOpen(Transform target, System.Action callBack = null)
    {
        Sequence sequenceTW = DOTween.Sequence();
        CanvasGroup group = getOrAddComponet<CanvasGroup>(target.gameObject);
        group.alpha = 0;
        target.localScale = Vector3.one * 1.3f;
        sequenceTW.Append(group.DOFade(1, 0.3f)).SetAutoKill(false).SetUpdate(true);
        sequenceTW.Join(target.DOScale(Vector3.one, 0.3f)).OnComplete(() => {
            if (callBack != null) callBack();
        }).SetUpdate(true);
    }
    /// <summary>
    /// 关闭弹窗动画（alpha 1->0）
    /// </summary>
    /// <param name="target">弹窗对象</param>
    /// <param name="callBack">结束回调</param>
    public void DialogAnimClose(Transform target, System.Action callBack = null)
    {
        Sequence sequenceTW = DOTween.Sequence();
        CanvasGroup group = getOrAddComponet<CanvasGroup>(target.gameObject);
        group.alpha = 1;
        target.localScale = Vector3.one;
        sequenceTW.Append(group.DOFade(0, 0.15f)).SetAutoKill(false).SetUpdate(true);
        sequenceTW.Join(target.DOScale(Vector3.one * 1.3f, 0.15f)).OnComplete(() => {
            if (callBack != null) callBack();
        }).SetUpdate(true);
    }


    public float ationDutation = 0.3f;
    float startScaleDouble = 0.5f;
    public void playAtion(Transform tr)
    {
        float oriScale = tr.localScale.x;
        tr.localScale = new Vector3(startScaleDouble * oriScale, startScaleDouble * oriScale, 1);
        DOTween.Sequence().Append(tr.DOScale(oriScale, ationDutation)
                        .SetEase(Ease.OutBack))
                        .SetUpdate(true);
    }
    public void playAtion(Transform tr, Ease ease)
    {
        float oriScale = tr.localScale.x;
        tr.localScale = new Vector3(startScaleDouble * oriScale, startScaleDouble * oriScale, 1);
        DOTween.Sequence().Append(tr.DOScale(oriScale, ationDutation)
                        .SetEase(ease))
                        .SetUpdate(true);
    }
    public void closeAtion(Transform tr, Vector2 toPos, Ease ease)
    {
        float oriScale = tr.localScale.x;

        DOTween.Sequence().Append(tr.DOScale(0, ationDutation)
                        .SetEase(ease))
                        .Insert(0, tr.GetComponent<RectTransform>().DOMove(toPos, ationDutation))
                        .SetUpdate(true);
    }




    #endregion

    /// <summary>
    /// 获取或者添加组件
    /// </summary>
    protected T getOrAddComponet<T>(GameObject go) where T : Component
    {
        return CommonTool.getOrAddComponet<T>(go);
    }





}
