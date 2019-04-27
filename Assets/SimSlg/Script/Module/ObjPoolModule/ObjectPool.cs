// ********************************************************
// 描述：对象池
// 作者：ShadowRabbit 
// 创建时间：2019-04-24 13:57:09
// ********************************************************

using System;
using System.Collections.Generic;
using UnityEngine;
public class ObjectPool 
{
    public List<BaseEntity> objects;//池内对象
    public int capacity;//池内对象最大储存数
    public float expireTime;//池内对象过期时间
    public string name;//对象池的名字
    public event Action<string> onDestroy;
    public ObjectPool(int capacity, float expireTime ,string name)
    {
        objects = new List<BaseEntity>();
    }
    /// <summary>
    /// 释放实体
    /// </summary>
    /// <param name="entity"></param>
    public void release(BaseEntity entity) {
        objects.Remove(entity);
        GameObject.Destroy(entity);
        if (objects.Count==0)
        {
            //通知对象池模块销毁对象池
        }
    }
    /// <summary>
    /// 获取一个池内的可用对象 如果没有则创建新对象
    /// </summary>
    /// <param name="data"></param>
    /// <param name="logic"></param>
    /// <param name="entityGroup"></param>
    /// <param name="assetPath"></param>
    /// <returns></returns>
    public BaseEntity SpawnEntity(BaseData data, BaseLogic logic, string entityGroup, string assetPath)
    {
        BaseEntity entity=null;
        foreach (var obj in objects)
        {
            if (obj.entityLogic.available)
            {
                entity = obj;
            }
        }
        //资源模块获取预设物  并加载
        if (entity==null)
        {
            entity = CreateEntity( data,  logic,  entityGroup,  assetPath);
            entity.OnInit(EntityModule.Instance,data,logic,entityGroup,assetPath);
            InstantiateEntity(entity);
        }
        //调用生成方法
        entity.OnSpawn();
        return entity;
    }
    public void UnSpawnEntity(BaseEntity entity)
    {
        if (objects.Count > capacity)
        {
            release(entity);
        }
        else {
            entity.OnUnSpawn();
        }
    }
    public void Update() { 
        
    }
    public BaseEntity CreateEntity(BaseData data, BaseLogic logic, string entityGroup, string assetPath)
    {

        return null;
    }
    public BaseEntity InstantiateEntity(BaseEntity entity)
    {
        return null;
    }
}
