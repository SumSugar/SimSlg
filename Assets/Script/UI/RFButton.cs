// ********************************************************
// 描述：自定义普通按钮
// 作者：ShadowRabbit 
// 创建时间：2019-05-01 21:48:32
// ********************************************************
using System;
using UnityEngine.EventSystems;

public sealed class RFButton : RFUI, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler,IPointerClickHandler
{
    private const float FadeTime = 0.3f;//渐变时间
    private const float OnHoverAlpha = 0.7f;//悬挂时透明度
    private const float OnClickAlpha = 0.6f;//点击时透明度
    private event Action<int,PointerEventData>  onPointerEnter = null;
    private event Action<int, PointerEventData> onPointerExit = null;
    private event Action<int, PointerEventData> onPointerDown = null;
    private event Action<int, PointerEventData> onPointerUp = null;
    private event Action<int, PointerEventData> onPointerClick = null;
    private void Start()
    {
        UIEntity uie = GetComponentInParent<BaseEntity>() as UIEntity;
        if (uie==null)
        {
            throw new Exception("null");
        }
        //UIEntity订阅所有子UI事件
        onPointerEnter += uie.OnPointerEnter;
        onPointerExit += uie.OnPointerExit;
        onPointerDown += uie.OnPointerDown;
        onPointerUp += uie.OnPointerUp;
        onPointerClick += uie.OnPointerClick;
    }
    /// <summary>
    /// 进入事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!active)
        {
            return;
        }
        StopAllCoroutines();
        StartCoroutine(cg.FadeToAlpha(OnHoverAlpha, FadeTime));
        onPointerEnter?.Invoke(id, eventData);
    }
    /// <summary>
    /// 离开事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!active)
        {
            return;
        }
        StopAllCoroutines();
        StartCoroutine(cg.FadeToAlpha(1f, FadeTime));
        onPointerExit?.Invoke(id, eventData);
    }
    /// <summary>
    /// 按下事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!active)
        {
            return;
        }
        cg.alpha = OnClickAlpha;
        onPointerDown?.Invoke(id, eventData);
    }
    /// <summary>
    /// 释放事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!active)
        {
            return;
        }
        cg.alpha = OnHoverAlpha;
        onPointerUp?.Invoke(id, eventData);
    }
    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!active)
        {
            return;
        }
        onPointerClick?.Invoke(id, eventData);
    }
}
