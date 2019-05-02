// ********************************************************
// 描述：实体模块
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 15:14:45
// ********************************************************


using System.Collections.Generic;

public class EntityModule : BaseModuleSingleton<EntityModule> {
    private static int entityId = 0;//实体的id
    Dictionary<string, List<BaseEntity>> entities = new Dictionary<string, List<BaseEntity>>();//全部实体
    /// <summary>
    /// 生成实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="data"></param>
    /// <param name="logic"></param>
    /// <param name="entityGroup"></param>
    /// <param name="assetPath"></param>
    /// <returns></returns>
    public T SpawnEntity<T>(BaseData data, string entityGroup, string assetPath) where T:BaseEntity{ 
        BaseEntity entity = ObjPoolModule.Instance.SpawnEntity(data,entityGroup,assetPath);
        return entity as T;
    }
    /// <summary>
    /// 回收实体
    /// </summary>
    /// <param name="entity"></param>
    public void UnSpawnEntity(BaseEntity entity) {
        ObjPoolModule.Instance.UnSpawnEntity(entity);
    }
    /// <summary>
    /// 回收全部实体
    /// </summary>
    public void UnSpawnAllEntities() {
        foreach (var entityList in entities.Values)
        {
            foreach (var entity in entityList)
            {
                UnSpawnEntity(entity);
            }
        }
    }
    /// <summary>
    /// 获取某一组实体
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    public List<BaseEntity> GetEntitiesByGroup(string group) {
        List<BaseEntity> entities;
        this.entities.TryGetValue(group, out entities);
        return entities;
    }
    /// <summary>
    /// 生成实体ID
    /// </summary>
    /// <returns></returns>
    public int GetEntityId()
    {
        return entityId++;
    }
}

