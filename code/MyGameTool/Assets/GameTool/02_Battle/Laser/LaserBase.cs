/****************************************************
    文件：LaserBase.cs
    功能：射线基类
*****************************************************/

using System.Collections;
using DG.Tweening;
using UnityEngine;

public class LaserBase : MonoBehaviour
{

    protected float maxDistance = 100;                              //  射线最长距离
    protected float laserSpeed;                                     //  射线速度
    protected LayerMask mask;                                       //  Mask
    protected Transform attacker;                                   //  持有者
    protected float damage;                                         //  伤害
    protected bool attackFlag = false;


    public LineRenderer lineRenderer;                               //  射线组件
    public float delatVal;                                          //  延迟值
    public Transform startTr;                                       //  开始对象
    public GameObject startFx;                                      //  开始特效
    public GameObject endFx;                                        //  击中特效
    float distance;                                                 //  距离


    // 普通Mask标签
    public virtual void InitForwardMask(){
        mask = LayerMask.GetMask("Default");
    }
    public virtual void InitAttackMask() {
        mask = LayerMask.GetMask("Default");
    }


    // 反射Mask标签
    public virtual void InitRefForwardMask() {
        mask = LayerMask.GetMask("Default", "Ground");
    }
    public virtual void InitRefAttackMask() {
        mask = LayerMask.GetMask("Default", "Ground");
    }

    /// <summary>
    /// 检测激光触碰攻击事件
    /// </summary>
    /// <param name="other"></param>
    protected virtual void AttackTriggerEnter(Collider other){
        // 子类定义  ....
    }
    /// <summary>
    /// Trigger回调事件
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (!attackFlag) return;
        AttackTriggerEnter(other);
    }



    #region  /***** 瞄准激光 **********************************************************************/

    /// <summary>
    /// 瞄准激光
    /// </summary>
    /// <returns></returns>
    public virtual Transform InitForwardLaser()
    {
        StartCoroutine(OpenForwardLaser());
        return transform;
    }
    /// <summary>
    /// 瞄准协成
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator OpenForwardLaser()
    {
        //====mask初始化设置
        InitForwardMask();
        endFx.SetActive(false);
        startFx.SetActive(false);

        while (true)
        {
            delatVal -= Time.deltaTime;

            Ray ray = new Ray(startTr.position, startTr.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 80, mask))
            {
                distance = Vector3.Distance(startTr.position, hit.point);
                lineRenderer.SetPosition(1, new Vector3(0, 0, distance));
            }
            else
            {
                distance = 80;
                lineRenderer.SetPosition(1, new Vector3(0, 0, distance));
            }
            yield return new WaitForEndOfFrame();
        }
    }

    Transform targetTr;
    /// <summary>
    /// 目标到点的激光
    /// </summary>
    /// <param name="_attacker"></param>
    /// <param name="_targetTr"></param>
    public void InitSpecialIdleLaser(Transform _attacker, Transform _targetTr)
    {
        attackFlag = true;
        attacker = _attacker;
        targetTr = _targetTr;
        lineRenderer.gameObject.SetActive(true);
        transform.Find("LineRender").gameObject.SetActive(false);
        mask = LayerMask.GetMask("Default", "Ground");
        startFx.gameObject.SetActive(true);
        StartCoroutine(UpdateLaser2());
    }

    IEnumerator UpdateLaser2()
    {
        while (true)
        {
            startFx.gameObject.SetActive(true);
            startFx.transform.position = startTr.position;

            delatVal -= Time.deltaTime;
            lineRenderer.material.SetTextureOffset("_MainTex", new Vector2(delatVal, 0f));
            distance = Vector3.Distance(startTr.position, targetTr.position);
            lineRenderer.SetPosition(1, new Vector3(0, 0, distance));
            endFx.SetActive(true);
            endFx.transform.position = targetTr.position;
            yield return new WaitForEndOfFrame();
        }
    }




    #endregion

    #region  /***** 瞄准攻击激光 **********************************************************************/

    /// <summary>
    /// 攻击激光
    /// </summary>
    /// <param name="_attacker"></param>
    /// <param name="_damage"></param>
    public void InitSpecialAttack(Transform _attacker, float _damage)
    {
        attackFlag = true;
        attacker = _attacker;
        damage = _damage;
        InitAttackMask();

        lineRenderer.gameObject.SetActive(true);
        startFx.gameObject.SetActive(true);
        StartCoroutine(UpdateLaser());
        StartCoroutine(CheckAttack());
    }
    IEnumerator UpdateLaser()
    {
        while (true)
        {
            startFx.gameObject.SetActive(true);
            startFx.transform.position = startTr.position;

            delatVal -= Time.deltaTime;

            Ray ray = new Ray(startTr.position, startTr.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 80, mask))
            {
                endFx.SetActive(true);
                endFx.transform.position = hit.point;
                distance = Vector3.Distance(startTr.position, hit.point);
                lineRenderer.SetPosition(1, new Vector3(0, 0, distance));
            }
            else
            {
                endFx.SetActive(false);
                distance = 80;
                lineRenderer.SetPosition(1, new Vector3(0, 0, distance));
            }

            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// 碰撞体伤害监测
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckAttack()
    {
        //使用碰撞体
        BoxCollider col = transform.GetComponent<BoxCollider>();
        col.isTrigger = true;

        while (true)
        {
            col.enabled = true;
            col.size = new Vector3(0.8f, 0.8f, distance);
            col.center = new Vector3(0, 0, distance / 2);
            yield return new WaitForSeconds(0.1f);
            col.enabled = false;
            yield return new WaitForSeconds(0.4f);
        }
    } 
    #endregion

    #region /***** 激光瞄准反射 **********************************************************************/

    LaserBase previousRayReflectItem;          //上一個反射射线Item

    //=======附给下一个反射射线Item的
    Vector3 nextStartPos;
    Vector3 nextAngle;
    bool nextActive;
    public Vector3 NextStartPos { get { return nextStartPos; } }
    public Vector3 NextAngle { get { return nextAngle; } }
    public bool NextActive { get { return nextActive; } }
    //=======开启瞄准的射线
    public void InitRefForwardLaser(LaserBase _previousRayReflectItem)
    {
        InitRefForwardMask();
        previousRayReflectItem = _previousRayReflectItem;
        //开启
        StartCoroutine(OpenRefForwardLaser());
    }
    //=======瞄准Item协程
    Vector3 targetZero;
    IEnumerator OpenRefForwardLaser()
    {
        InitRefForwardMask();
        targetZero = new Vector3(transform.localScale.x, transform.localScale.y, 0);

        while (true)
        {
            if (previousRayReflectItem != null)
            {
                transform.position = previousRayReflectItem.NextStartPos;
                transform.eulerAngles = previousRayReflectItem.NextAngle;
                endFx.transform.position = transform.position;
                endFx.SetActive(true);
                if (!previousRayReflectItem.NextActive)
                {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0);
                    nextActive = false;
                    yield return null;
                    continue;
                }
            }
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            Vector3 reflectVec = new Vector3();
            //Vector3 targetScale;
            float distance = 0;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, maxDistance, mask))
            {
                Vector3 incomingVec = hit.point - transform.position;
                distance = Vector3.Distance(hit.point, transform.position);
                //hit.normal获取模型法线
                //Vector3.Reflect沿着法线反射向量
                reflectVec = Vector3.Reflect(incomingVec, hit.normal);
                reflectVec.y = 0;

                nextActive = true;
                nextStartPos = hit.point;
                nextAngle = Quaternion.FromToRotation(Vector3.forward, reflectVec).eulerAngles;
            }
            else
            {
                distance = maxDistance;
                nextActive = false;
                nextStartPos = hit.point;
                nextAngle = Quaternion.FromToRotation(Vector3.forward, reflectVec).eulerAngles;
            }

            delatVal -= Time.deltaTime;
            lineRenderer.material.SetTextureOffset("_MainTex", new Vector2(delatVal, 0f));
            lineRenderer.SetPosition(1, new Vector3(0, 0, distance));

            yield return null;
        }
    }
    #endregion














    //==================     激光瞄准     ======================

    //public virtual Transform InitForwardLaser(){
    //    StartCoroutine(OpenForwardLaser());
    //    return transform;
    //}
    //protected virtual IEnumerator OpenForwardLaser(){
    //    //====mask初始化设置
    //    InitForwardMask();
    //    while (true){
    //        Ray ray = new Ray(transform.position, transform.forward);
    //        RaycastHit hit;
    //        float distance = 0;
    //        if (Physics.Raycast(ray.origin, ray.direction, out hit, maxDistance, mask))
    //        {
    //            distance = Vector3.Distance(hit.point, transform.position);
    //            Vector3 targetScale = transform.localScale;
    //            transform.localScale = new Vector3(targetScale.x, targetScale.y, distance);
    //        }
    //        else
    //        {
    //            distance = maxDistance;
    //            Vector3 targetScale1 = transform.localScale;
    //            transform.localScale = new Vector3(targetScale1.x, targetScale1.y, distance);
    //        }
    //        yield return new WaitForSeconds(0.01f);
    //    }
    //}

    //==================     激光攻击     ======================

    //public virtual void InitAttackLaser(Transform _attacker, float _damage, float _laserSpeed){
    //    attacker = _attacker;
    //    laserSpeed = _laserSpeed;
    //    damage = _damage;
    //    attackFlag = true;
    //    transform.GetComponent<Collider>().enabled = true;
    //    OpenAttackLaser();
    //}
    //protected virtual float OpenAttackLaser(){
    //    InitAttackMask();
    //    Ray ray = new Ray(transform.position, transform.forward);
    //    RaycastHit hit;
    //    float distance = 0;
    //    Vector3 targetScale1 = transform.localScale;
    //    if (Physics.Raycast(ray.origin, ray.direction, out hit, maxDistance, mask)){
    //        distance = Vector3.Distance(hit.point, transform.position);
    //    }
    //    else{
    //        distance = maxDistance;
    //    }
    //    DOTween.To(() => transform.localScale, r => transform.localScale = r, new Vector3(targetScale1.x, targetScale1.y, distance), distance / laserSpeed);
    //    return distance / laserSpeed;
    //}


    //==================     激光瞄准反射     ======================

    //LaserBase previousRayReflectItem;          //上一個反射射线Item

    ////=======附给下一个反射射线Item的
    //Vector3 nextStartPos;
    //Vector3 nextAngle;
    //bool nextActive;
    //public Vector3 NextStartPos { get { return nextStartPos; } }
    //public Vector3 NextAngle { get { return nextAngle; } }
    //public bool NextActive { get { return nextActive; } }
    ////=======开启瞄准的射线
    //public void InitRefForwardLaser(LaserBase _previousRayReflectItem){
    //    previousRayReflectItem = _previousRayReflectItem;
    //    //开启
    //    StartCoroutine(OpenRefForwardLaser());
    //}
    ////=======瞄准Item协程
    //Vector3 targetZero;
    //IEnumerator OpenRefForwardLaser(){
    //    InitRefForwardMask();
    //    targetZero = new Vector3(transform.localScale.x, transform.localScale.y, 0);

    //    while (true)
    //    {
    //        if (previousRayReflectItem != null){
    //            transform.position = previousRayReflectItem.NextStartPos;
    //            transform.eulerAngles = previousRayReflectItem.NextAngle;
    //            if (!previousRayReflectItem.NextActive){
    //                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0);
    //                nextActive = false;
    //                yield return null;
    //                continue;
    //            }
    //        }
    //        Ray ray = new Ray(transform.position, transform.forward);
    //        RaycastHit hit;
    //        Vector3 reflectVec = new Vector3();
    //        Vector3 targetScale;
    //        float distance = 0;
    //        if (Physics.Raycast(ray.origin, ray.direction, out hit, maxDistance, mask))
    //        {
    //            Vector3 incomingVec = hit.point - transform.position;
    //            distance = Vector3.Distance(hit.point, transform.position);
    //            //hit.normal获取模型法线
    //            //Vector3.Reflect沿着法线反射向量
    //            reflectVec = Vector3.Reflect(incomingVec, hit.normal);
    //            reflectVec.y = 0;

    //            nextActive = true;
    //            nextStartPos = hit.point;
    //            nextAngle = Quaternion.FromToRotation(Vector3.forward, reflectVec).eulerAngles;
    //            targetScale = transform.localScale;
    //        }
    //        else
    //        {
    //            distance = maxDistance;
    //            nextActive = false;
    //            nextStartPos = hit.point;
    //            nextAngle = Quaternion.FromToRotation(Vector3.forward, reflectVec).eulerAngles;
    //            targetScale = transform.localScale;
    //        }
    //        transform.localScale = new Vector3(targetScale.x, targetScale.y, distance);
    //        yield return null;
    //    }
    //}



    //==================     激光伤害反射     ======================

    //// float reflectLaserSpeed = 500;                     //射线速度
    //public float InitRefAttackLaser(LaserBase _previousRayReflectItem, Transform _attacker, float _damage, float _laserSpeed = 500){
    //    previousRayReflectItem = _previousRayReflectItem;
    //    attacker = _attacker;
    //    damage = _damage;
    //    laserSpeed = _laserSpeed;
    //    attackFlag = true;
    //    transform.GetComponent<Collider>().enabled = true;

    //    //发射激光，返回一个延迟        
    //    return OpenRefAttackLaser();
    //}

    ////=======》射线发射，返回一个时间
    //float OpenRefAttackLaser(){
    //    InitRefAttackMask();
    //    if (previousRayReflectItem != null){
    //        transform.position = previousRayReflectItem.NextStartPos;
    //        transform.eulerAngles = previousRayReflectItem.NextAngle;
    //        if (!previousRayReflectItem.NextActive){
    //            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0);
    //            nextActive = false;
    //        }
    //    }
    //    Ray ray = new Ray(transform.position, transform.forward);
    //    RaycastHit hit;
    //    Vector3 reflectVec = new Vector3();
    //     Vector3 targetScale;
    //    float distance = 0;
    //    if (Physics.Raycast(ray.origin, ray.direction, out hit, maxDistance, mask)){
    //        Vector3 incomingVec = hit.point - transform.position;
    //        distance = Vector3.Distance(hit.point, transform.position);
    //        //hit.normal获取模型法线
    //        //Vector3.Reflect沿着法线反射向量
    //        reflectVec = Vector3.Reflect(incomingVec, hit.normal);
    //        reflectVec.y = 0;
    //        // Debug.DrawLine(transform.position, hit.point, Color.red);
    //        // Debug.DrawRay(hit.point, reflectVec, Color.green);

    //        nextActive = true;
    //        nextStartPos = hit.point;
    //        nextAngle = Quaternion.FromToRotation(Vector3.forward, reflectVec).eulerAngles;
    //        targetScale = transform.localScale;
    //    }
    //    else{
    //        distance = maxDistance;
    //        nextActive = false;
    //        nextStartPos = hit.point;
    //        nextAngle = Quaternion.FromToRotation(Vector3.forward, reflectVec).eulerAngles;
    //        targetScale = transform.localScale;
    //    }
    //    transform.localScale = new Vector3(targetScale.x, targetScale.y, distance);
    //    float delatTime = distance / laserSpeed;
    //    return delatTime;
    //}


    //==================     激光固定（直射到目标的）     ======================
    //public virtual Transform InitFixedLaser(Transform targetTr){
    //    OpenFixedLaser(targetTr);
    //    return transform;
    //}
    //void OpenFixedLaser(Transform targetTr){
    //    float distance = Vector3.Distance(transform.position,targetTr.position);
    //    Vector3 targetScale = transform.localScale;
    //    transform.localScale = new Vector3(targetScale.x, targetScale.y, distance);
    //}


























}
