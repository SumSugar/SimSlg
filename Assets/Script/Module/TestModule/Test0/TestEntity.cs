// ********************************************************
// 描述：测试实体 
// 作者：ShadowRabbit 
// 创建时间：2019-05-11 15:06:54
// ********************************************************


public class TestEntity : Entity
{
    /// <summary>
    /// 被对象池调用初始化时
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="data"></param>
    /// <param name="entityGroup"></param>
    /// <param name="assetPath"></param>
    public override void OnInit(int entityId, BaseData data, string entityGroup, string assetPath)
    {
        base.OnInit(entityId, data, entityGroup, assetPath);
    }
    /// <summary>
    /// 被对象池调用创建时
    /// </summary>
    /// <param name="data"></param>
    public override void OnSpawn(BaseData data)
    {
        base.OnSpawn(data);
    }
    /// <summary>
    /// 被对象池调用回收时
    /// </summary>
    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }
    /// <summary>
    /// 每帧执行
    /// </summary>
    public override void Update()
    {
        base.Update();
    }
    /// <summary>
    /// 销毁时
    /// </summary>
    public override void OnDes()
    {
        base.OnDes();
    }
}
