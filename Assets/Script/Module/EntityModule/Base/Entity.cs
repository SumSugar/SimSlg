// ********************************************************
// 描述：游戏物体实体
// 作者：ShadowRabbit 
// 创建时间：2019-04-27 12:38:50
// ********************************************************

using UnityEngine;

public class Entity : BaseEntity
{
    public override void OnInit(int entityId, BaseData data, string entityGroup, string assetPath)
    {
        base.OnInit(entityId, data, entityGroup, assetPath);
    }
    public override void OnSpawn(BaseData data)
    {
        base.OnSpawn(data);
    }
    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
        gameObject.transform.position = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.layer = defaultLayer;
    }
    public override void Update()
    {
        base.Update();
    }
    public override void OnDes()
    {
        base.OnDes();
    }
}
