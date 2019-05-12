// ********************************************************
// 描述：实体模块
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 15:14:45
// ********************************************************


using System;
using System.Collections.Generic;

public class EntityModule : BaseModuleSingleton<EntityModule> {
    private static int entityId = 0;//实体的id
    private Dictionary<string, Dictionary<int, BaseEntity>> entities = new Dictionary<string, Dictionary<int, BaseEntity>>();//全部实体
    /// <summary>
    /// 生成实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="entityGroup"></param>
    /// <param name="assetPath"></param>
    /// <returns></returns>
    public T SpawnEntity<T>(string entityGroup, string assetPath) where T:BaseEntity{
        BaseEntity entity = ObjPoolModule.Instance.SpawnEntity(null, entityGroup, assetPath);
        if (!entities.ContainsKey(entityGroup))
        {
            entities.Add(entityGroup, new Dictionary<int, BaseEntity>());
        }
        entities[entityGroup].Add(entity.entityId, entity);
        return entity as T;
    }
    //默认数据类中的name就是group名字
    public T SpawnEntity<T>(BaseData data, string assetPath) where T : BaseEntity
    {
        if (data==null)
        {
            throw new Exception("data and group cant be null at same time");
        }
        BaseEntity entity = ObjPoolModule.Instance.SpawnEntity(data, data.name, assetPath);
        if (!entities.ContainsKey(data.name))
        {
            entities.Add(data.name, new Dictionary<int, BaseEntity>());
        }
        entities[data.name].Add(entity.entityId, entity);
        return entity as T;
    }
    /// <summary>
    /// 回收实体
    /// </summary>
    /// <param name="entity"></param>
    public void UnSpawnEntity(BaseEntity entity)
    {
        if (entity == null)
        {
            throw new Exception("null");
        }
        ObjPoolModule.Instance.UnSpawnEntity(entity);
        //删除对应实体组中的实体
        entities[entity.entityGroup].Remove(entity.entityId);
        //该组没有实体 撤销该实体组
        if (entities[entity.entityGroup].Count == 0)
        {
            entities.Remove(entity.entityGroup);
        }
    }
    /// <summary>
    /// 回收全部实体
    /// </summary>
    public void UnSpawnAllEntities()
    {
        foreach (var entityDic in entities.Values)
        {
            foreach (var entity in entityDic.Values)
            {
                UnSpawnEntity(entity);
            }
        }
        entities.Clear();//清空实体记录
    }
    /// <summary>
    /// 获取某一组实体
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    public Dictionary<int, BaseEntity> GetEntitiesByGroup(string group)
    {
        Dictionary<int, BaseEntity> entities;
        this.entities.TryGetValue(group, out entities);
        return entities;
    }
    /// <summary>
    /// 根据id查找实体
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    public BaseEntity GetEntityById(int entityId)
    {
        foreach (var entityGroup in entities.Values)
        {
            if (entityGroup.ContainsKey(entityId))
            {
                return entityGroup[entityId];
            }
        }
        throw new Exception("no found");
    }
    /// <summary>
    /// 生成实体ID
    /// </summary>
    /// <returns></returns>
    public int GenerateId()
    {
        return entityId++;
    }
}

