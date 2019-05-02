// ********************************************************
// 描述：对象池中对象的接口
// 作者：ShadowRabbit 
// 创建时间：2019-04-26 15:04:13
// ********************************************************

public interface IPoolObject
{
    /// <summary>
    /// 实体生成
    /// </summary>
    void OnSpawn(BaseData data);
    /// <summary>
    /// 实体回收
    /// </summary>
    void OnUnSpawn();
}
