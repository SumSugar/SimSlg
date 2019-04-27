// ********************************************************
// 描述：实体模块
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 15:14:45
// ********************************************************


using System.Collections.Generic;
using UnityEngine;

public class EntityModule : BaseModuleSingleton<EntityModule> {
    private static int entityId = 0;
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
    private T SpawnEntity<T>(string name, BaseData data, BaseLogic logic, string entityGroup, string assetPath) where T:BaseEntity{ 
        ObjPoolModule opm = ObjPoolModule.Instance as ObjPoolModule;
        BaseEntity entity = opm.SpawnEntity(name,assetPath);
        entity.OnInit(GetEntityId(),data,logic,entityGroup,assetPath);
        return entity as T;
    }
    /// <summary>
    /// 回收实体
    /// </summary>
    /// <param name="entity"></param>
    private void UnSpawnEntity(BaseEntity entity) {
        ObjPoolModule opm = ObjPoolModule.Instance as ObjPoolModule;
        opm.UnSpawnEntity(entity);
    }
    /// <summary>
    /// 回收全部实体
    /// </summary>
    private void UnSpawnAllEntities() {
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
    private List<BaseEntity> GetEntitiesByGroup(string group) {
        List<BaseEntity> entities;
        this.entities.TryGetValue(group, out entities);
        return entities;
    }
    /// <summary>
    /// 生成实体ID
    /// </summary>
    /// <returns></returns>
    public static int GetEntityId() {
        return entityId++;
    }

}

