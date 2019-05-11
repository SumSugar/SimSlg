// ********************************************************
// 描述：用于测试的UI
// 作者：ShadowRabbit 
// 创建时间：2019-05-02 10:39:58
// ********************************************************

using UnityEngine;
using UnityEngine.EventSystems;

public class TestUIEntity : UIEntity
{
    public override void OnInit(int entityId, BaseData data, string entityGroup, string assetPath)
    {
        //UIEntity自带canvas 与 CanvasGroup(不会重复添加 预设物可以持有该组件)
        //canvas用于触发ugui的事件
        //CanvasGroup用于渐变关闭 打开ui 微弱的动画效果
        //因为每个UIEntity都自带画布 所以层级不同会遮挡
        //通过depth属性可以更变UI层级 越高则优先显示
        base.OnInit(entityId, data, entityGroup, assetPath);
        //SetDepth(2);设置层级
    }
    /// <summary>
    /// 创建事件  用于初始化数据
    /// </summary>
    /// <param name="data"></param>
    public override void OnSpawn(BaseData data)
    {
        base.OnSpawn(data);
    }
    /// <summary>
    /// 回收事件回调 用于清除一些数据
    /// </summary>
    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }
    /// <summary>
    /// 点击事件 点击到的是UIEntity下的ugui控件，UIEntity监听了他们的触发事件，在UIEntity中根据他们的id 和事件，执行不同的UI逻辑
    /// </summary>
    /// <param name="id">所负责的子UI的id</param>
    /// <param name="eventData">子UI的UGUI接口触发回调的数据</param>
    public override void OnPointerClick(int id, PointerEventData eventData)
    {
        base.OnPointerClick(id, eventData);
        switch (id)
        {
            case 0:
                buttonEvent1(id);
                break;
            case 1:
                buttonEvent2(id);
                break;
            case 2:
                buttonEvent3(id);
                break;
            case 3:
                buttonEvent4(id);
                break;
            case 4:
                buttonEvent5(id);
                break;
        }
    }
    public override void Update()
    {
        base.Update();
    }
    //id为1的UI触发的方法
    void buttonEvent1(int id) {
        Debug.Log("点击了按钮" + id);
    }
    //id为2的UI触发的方法
    void buttonEvent2(int id) {
        Debug.Log("点击了按钮" + id);
    }
    //id为3的UI触发的方法
    void buttonEvent3(int id) {
        Debug.Log("点击了按钮" + id);
    }
    //id为4的UI触发的方法
    void buttonEvent4(int id) {
        Debug.Log("点击了按钮" + id);
    }
    //id为5的UI触发的方法
    void buttonEvent5(int id) {
        Debug.Log("点击了按钮" + id);
    }
    public override void OnPointerDown(int id, PointerEventData eventData)
    {
        base.OnPointerDown(id, eventData);
    }
    public override void OnPointerEnter(int id, PointerEventData eventData)
    {
        base.OnPointerEnter(id, eventData);
    }
    public override void OnPointerExit(int id, PointerEventData eventData)
    {
        base.OnPointerExit(id, eventData);
    }
    public override void OnPointerUp(int id, PointerEventData eventData)
    {
        base.OnPointerUp(id, eventData);
    }
} 
