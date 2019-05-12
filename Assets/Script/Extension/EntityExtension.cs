// ********************************************************
// 描述：实体拓展类
// 作者：ShadowRabbit 
// 创建时间：2019-05-12 09:56:28
// ********************************************************


public static class EntityExtension
{
    /// <summary>
    /// 获取实体ID
    /// </summary>
    /// <returns></returns>
    public static int GetId(this BaseEntity entity)
    {
        return entity.entityId;
    }
}
