// ********************************************************
// 描述：基础单位实体
// 作者：ShadowRabbit 
// 创建时间：2019-04-29 13:35:23
// ********************************************************


public class UnitEntity : Entity
{
    private Fsm<UnitEntity> fsm;
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
    }
    public override void Update()
    {
        base.Update();
    }
    public override void Destroy()
    {
        base.Destroy();
    }
}
