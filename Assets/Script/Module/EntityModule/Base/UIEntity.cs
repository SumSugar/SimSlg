// ********************************************************
// 描述：UI实体
// 作者：ShadowRabbit 
// 创建时间：2019-04-27 12:38:08
// ********************************************************

using UnityEngine;
using UnityEngine.EventSystems;

public class UIEntity : BaseEntity
{
    private Canvas canvas;//画布
    private CanvasGroup cg;//画布组
    [SerializeField]
    public int depth=0;//深度
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="data"></param>
    /// <param name="entityGroup"></param>
    /// <param name="assetPath"></param>
    public override void OnInit(BaseData data, string entityGroup, string assetPath)
    {
        canvas = gameObject.GetOrAddComponent<Canvas>();
        cg = gameObject.GetOrAddComponent<CanvasGroup>();
        canvas.sortingOrder = depth;
        canvas.overrideSorting = true;
        this.entityAssetPath = assetPath;
        this.entityGroup = entityGroup;
        this.entityName = gameObject.name + entityId;
    }
    /// <summary>
    /// 改变深度
    /// </summary>
    /// <param name="depth"></param>
    public void SetDepth(int depth) {
        this.depth = depth;
        canvas.sortingOrder = this.depth;
    }
    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="data"></param>
    public override void OnSpawn(BaseData data)
    {
        base.OnSpawn(data);
        StopAllCoroutines();
        StartCoroutine(cg.FadeToAlpha(1f, 0.3f));
    }
    /// <summary>
    /// 回收
    /// </summary>
    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
        StopAllCoroutines();
        StartCoroutine(cg.FadeToAlpha(0f, 0.3f));
    }
    /// <summary>
    /// 销毁
    /// </summary>
    public override void OnDes()
    {
        base.OnDes();
    }
    /// <summary>
    /// 更新
    /// </summary>
    public override void Update()
    {
        base.Update();
    }
    /// <summary>
    /// 界面暂停。
    /// </summary>
    protected virtual void OnPause()
    {
        StopAllCoroutines();
        StartCoroutine(cg.FadeToAlpha(0.5f, 0.3f));
    }
    /// <summary>
    /// 界面暂停恢复。
    /// </summary>
    protected virtual void OnResume()
    {
        StopAllCoroutines();
        StartCoroutine(cg.FadeToAlpha(1f, 0.3f));
    }
    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="id"></param>
    /// <param name="eventData"></param>
    public virtual void OnPointerEnter(int id, PointerEventData eventData) {
    }
    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="id"></param>
    /// <param name="eventData"></param>
    public virtual void OnPointerExit(int id, PointerEventData eventData)
    {
    }
    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="id"></param>
    /// <param name="eventData"></param>
    public virtual void OnPointerDown(int id, PointerEventData eventData)
    {
    }
    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="id"></param>
    /// <param name="eventData"></param>
    public virtual void OnPointerUp(int id, PointerEventData eventData)
    {
    }
    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="id"></param>
    /// <param name="eventData"></param>
    public virtual void OnPointerClick(int id, PointerEventData eventData)
    {
    }
}
