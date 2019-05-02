// ********************************************************
// 描述：
// 作者：ShadowRabbit 
// 创建时间：2019-05-02 10:39:58
// ********************************************************

using UnityEngine;
using UnityEngine.EventSystems;

public class TestUIEntity : UIEntity
{
    public override void OnSpawn(BaseData data)
    {
        base.OnSpawn(data);
    }
    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }
    public override void OnPointerClick(int id, PointerEventData eventData)
    {
        base.OnPointerClick(id, eventData);
        Debug.Log("点击了按钮" + id);
    }
    public override void Update()
    {
        base.Update();
    }
} 
