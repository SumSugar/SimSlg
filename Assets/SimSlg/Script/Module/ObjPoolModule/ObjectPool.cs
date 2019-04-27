// ********************************************************
// 描述：对象池
// 作者：ShadowRabbit 
// 创建时间：2019-04-24 13:57:09
// ********************************************************

using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool 
{
    public List<BaseEntity> objects;//池内对象
    public int capacity;//池内对象最大储存数
    public int expireTime;//池内对象过期时间
    public string name;//对象池的名字
    public event Action<string> onDestroy;//对象池销毁事件
    public ObjectPool(int capacity, int expireTime ,string name)
    {
        objects = new List<BaseEntity>();
        this.capacity = capacity;
        this.expireTime = expireTime;
        this.name = name;
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
            Object entityObj = CreateEntity(assetPath);//获取资源
            GameObject go = Object.Instantiate(entityObj) as GameObject;//实例化实体
            entity = go.GetOrAddComponent<BaseEntity>();//添加实体脚本
            entity.OnInit(EntityModule.Instance.GetEntityId(),data,logic,entityGroup,assetPath,expireTime);//初始化实体
            entity.onDestroy += OnEntityDestroy;//注册实体销毁事件
            objects.Add(entity);//添加到对象池容器中
        }
        //调用生成方法
        entity.OnSpawn();
        return entity;
    }
    /// <summary>
    /// 回收实体
    /// </summary>
    /// <param name="entity"></param>
    public void UnSpawnEntity(BaseEntity entity)
    {
        //超过了池内最大容量
        if (objects.Count > capacity)
        {
            entity.SendMessage("Destroy");//通知实体执行销毁事件
        }
        else {
            entity.OnUnSpawn();//回收实体
        }
    }
    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="data"></param>
    /// <param name="logic"></param>
    /// <param name="entityGroup"></param>
    /// <param name="assetPath"></param>
    /// <returns></returns>
    public Object CreateEntity(string assetPath)
    {
        return ResModule.Instance.LoadRes(assetPath);
    }

    /// <summary>
    /// 对象池中的实体 销毁事件
    /// </summary>
    /// <param name="entity"></param>
    private void OnEntityDestroy(BaseEntity entity) {
        objects.Remove(entity);
        if (objects.Count == 0)
        {
            //通知对象池模块销毁对象池
            onDestroy(name);
        }
    }
    /// <summary>
    /// 对象池每帧调用
    /// </summary>
    public void Update() {
        DateTime timeNow = DateTime.Now;//获取当前时间
        //注意foreach不支持循环中删除元素 所以用for循环
        for (int i = 0; i < objects.Count; i++)
        {
            //如果物体已经回收
            if (objects[i].entityLogic.available)
            {
                if ((timeNow - objects[i].recycleTime).Seconds > expireTime)
                {
                    objects[i].gameObject.SetActive(true);
                    objects[i].SendMessage("Destroy");//通知实体执行销毁事件
                }
            }
        }
    }
}
