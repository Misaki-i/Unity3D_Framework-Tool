/****************************************************
    功能：  Tween一些表现的小工具
*****************************************************/
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class TweenerTool : MonoBehaviour{


    /// <summary>
    /// 实现黑幕/白幕渐变的
    /// </summary>
    /// <param name="delay">延迟播放</param>
    /// <param name="closeTimeDelta">变黑时间间隔</param>
    /// <param name="openTimeDelta">变亮时间间隔</param>
    /// <returns></returns>
    public static GameObject OpenGradualChangeMask(float delay = 0, float closeTimeDelta = 1, float openTimeDelta = 1f, bool isWhite = false,System.Action callBack = null)
    {
        
        Transform parentCanvas = GameObject.Find("Canvas").transform;
        GameObject maskObj = new GameObject("BlackMask");
        Image maskImg = maskObj.AddComponent<Image>();
        CanvasGroup ConnectLevelMask = maskObj.AddComponent<CanvasGroup>();
        maskObj.transform.SetParent(parentCanvas);
        maskObj.transform.SetAsLastSibling();
        
        //赋值
        if(isWhite){
            maskImg.color = Color.white;            
        }
        else{
            maskImg.color = Color.black;
        }
        maskImg.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
        maskImg.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
        maskImg.rectTransform.anchorMin = Vector2.zero;
        maskImg.rectTransform.anchorMax = Vector2.one;
        
        maskImg.transform.localScale = Vector3.one;
        //屏幕效果
        ConnectLevelMask.alpha = 0;
        TimerTool.OpenOnce(delay, () =>{
            Sequence sequenceTw = DOTween.Sequence();
            sequenceTw.Append(ConnectLevelMask.DOFade(1,closeTimeDelta));
            sequenceTw.Append(ConnectLevelMask.DOFade(0,openTimeDelta)).OnComplete(()=>{
                if (maskObj != null)
                    Destroy(maskObj);
                if(callBack != null)
                    callBack();
            });
        });
        return maskObj;
    }


    /// <summary>
    /// 透明Mask，用于禁止用户点击的
    /// </summary>
    /// <param name="closeTime">关闭时间</param>
    /// <param name="closeCallBack">关闭回调</param>
    /// <returns></returns>
    public static GameObject OpenUIMask(float closeTime,System.Action closeCallBack = null){
        Transform parentCanvas = GameObject.Find("Canvas").transform;
        GameObject maskObj = new GameObject("UIMask");
        Image maskImg = maskObj.AddComponent<Image>();
        maskObj.transform.SetParent(parentCanvas);
        maskObj.transform.SetAsLastSibling();
        
        //赋值
        maskImg.color = Color.clear;
        maskImg.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
        maskImg.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
        maskImg.rectTransform.anchorMin = Vector2.zero;
        maskImg.rectTransform.anchorMax = Vector2.one;

        TimerTool.OpenOnce(closeTime,()=>{
            if(maskObj != null){
                Destroy(maskObj);
            }
            if (closeCallBack != null){
                closeCallBack();
            } 
        });
        return maskObj;
    }
    
    /****************************************************
        功能：  上升消失提示Todo
    *****************************************************/
    public static Transform ShowUpTips(Transform targetTr,float showTime){
        
        return targetTr;
    }
    
    
    /****************************************************
        功能：  一些弹窗效果
    *****************************************************/
    public static Sequence ShowUIPrope(Transform targetTr,float durtion){

        Sequence sequenceTW = DOTween.Sequence();
        CanvasGroup group = getOrAddComponet<CanvasGroup>(targetTr.gameObject);
        group.alpha = 0;
        targetTr.localScale = Vector3.one * 1.5f;
        sequenceTW.Append(group.DOFade(1, durtion)).SetAutoKill(false);
        sequenceTW.Join(targetTr.DOScale(Vector3.one, durtion));
        
        return sequenceTW;
    }
    
    

    /// <summary>
    /// UI实现随机炸开再集合的
    /// </summary>
    /// <param name="exploreTrs">爆炸的子物体</param>
    /// <param name="startPos">起始位置</param>
    /// <param name="targetPos">目标位置</param>
    /// <param name="exploreDes">爆开距离</param>
    /// <param name="EndCallBack">子物体到达时的事件</param>
    public static void PlayExploreUI(Transform[] exploreTrs, Vector3 startPos, Vector3 targetPos, float exploreDes, System.Action EndCallBack = null)
    {
        for (int i = 0; i < exploreTrs.Length; i++)
        {
            exploreTrs[i].gameObject.SetActive(true);
            exploreTrs[i].localPosition = startPos;
            PlayExploreUIEvent(exploreTrs[i], targetPos, exploreDes, EndCallBack);
        }
    }

    static void PlayExploreUIEvent(Transform exploreTr, Vector3 targetPos, float exploreDes, System.Action EndCallBack = null)
    {
        Vector3 tempVec = new Vector3(Random.Range(-exploreDes, exploreDes), Random.Range(-exploreDes, exploreDes), 0);
        float startTime = Random.Range(0.4f, 0.6f);
        float endTime = Random.Range(0.3f, 0.6f);
        Sequence sequenceTw = DOTween.Sequence();
        sequenceTw.Append(exploreTr.DOLocalMove(tempVec, startTime).SetEase(Ease.InOutQuad));
        sequenceTw.Append(exploreTr.DOLocalMove(targetPos, endTime).SetEase(Ease.InOutQuad)).OnComplete(() =>
        {
            // exploreTr.gameObject.SetActive(false);
            if (EndCallBack != null) EndCallBack();
        });
    }

    /// <summary>
    /// 3D物体实现随机炸开再集合的  (待优化)
    /// </summary>
    /// <param name="exploreTrs">爆炸的子物体</param>
    /// <param name="startPos">起始位置</param>
    /// <param name="targetPos">目标位置</param>
    /// <param name="exploreDes">爆开距离</param>
    /// <param name="EndCallBack">子物体到达时的事件</param>
    public static void PlayExplore3D(Transform[] exploreTrs, Vector3 startPos, Vector3 targetPos, float exploreDes, System.Action EndCallBack = null)
    {
        for (int i = 0; i < exploreTrs.Length; i++)
        {
            exploreTrs[i].gameObject.SetActive(true);
            exploreTrs[i].position = startPos;
            PlayExplore3DEvent(exploreTrs[i], targetPos,exploreDes, EndCallBack);
        }
    }
    static void PlayExplore3DEvent(Transform exploreTr, Vector3 targetPos,float exploreDes, System.Action EndCallBack = null)
    {
        Vector3 tempVec = new Vector3(Random.Range(-exploreDes, exploreDes), Random.Range(-exploreDes, exploreDes), Random.Range(-exploreDes, exploreDes));
        float startTime = Random.Range(0.4f, 0.6f);
        float endTime = Random.Range(0.3f, 0.6f);
        Sequence sequenceTw = DOTween.Sequence();
        sequenceTw.Append(exploreTr.DOMove(tempVec, startTime).SetEase(Ease.InOutQuad));
        sequenceTw.Append(exploreTr.DOMove(targetPos, endTime).SetEase(Ease.InOutQuad)).OnComplete(() =>
        {
            // exploreTr.gameObject.SetActive(false);
            if (EndCallBack != null) EndCallBack();
        });
    }

    /// <summary>
    /// 获取或者添加组件
    /// </summary>
    protected static T getOrAddComponet<T>(GameObject go) where T : Component{
        return CommonTool.getOrAddComponet<T>(go);
    }







}