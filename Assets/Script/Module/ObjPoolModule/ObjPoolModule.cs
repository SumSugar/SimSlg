// ********************************************************
// 描述：对象池模块
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 15:27:20
// ********************************************************

using System;
using System.Collections.Generic;

public class ObjPoolModule : BaseModuleSingleton<ObjPoolModule> {
    private int defaultCapacity = 99;//池内对象的默认最大堆叠数
    private int defaultExpireTime = 3;//池内对象的默认过期时间
    private Dictionary<string, ObjectPool> objPools = new Dictionary<string, ObjectPool>();//全部对象池
    //private 
    /// <summary>
    /// 生成实体
    /// </summary>
    /// <param name="name">对象池的名称</param>
    /// <param name="assetPath">资源路径</param>
    /// <returns></returns>
    public BaseEntity SpawnEntity(BaseData data, string entityGroup, string assetPath)
    {
        ObjectPool op = GetObjectPool(entityGroup);
        //如果没有获取到对象池，则创建一个新的对象池
        if (op==null)
        {
            op=CreateObjectPool(entityGroup,assetPath);
        }
        return op.SpawnEntity(data);
    }
    /// <summary>
    /// 回收实体
    /// </summary>
    public void UnSpawnEntity(BaseEntity entity) {
        if (entity==null)
        {
            throw new Exception("null");
        }
        ObjectPool op = GetObjectPool(entity.entityGroup);
        if (op == null)
        {
            throw new Exception("null");
        }
        op.UnSpawnEntity(entity);
    }
    /// <summary>
    /// 创建对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="capacity"></param>
    /// <param name="expireTime"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public ObjectPool CreateObjectPool(int capacity, int expireTime ,string name, string assetPath)
    {
        ObjectPool op = new ObjectPool(capacity, expireTime, name,assetPath);
        op.onDestroy += OnPoolDestroy;//注册对象池销毁事件
        objPools.Add(name,op);
        return op;
    }
    /// <summary>
    /// 创建对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expireTime"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public ObjectPool CreateObjectPool(int expireTime, string name, string assetPath)
    {
        ObjectPool op = new ObjectPool(defaultCapacity, expireTime, name, assetPath);
        op.onDestroy += OnPoolDestroy;//注册对象池销毁事件
        objPools.Add(name, op);
        return op;
    }
    /// <summary>
    /// 创建对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public ObjectPool CreateObjectPool(string name,string assetPath)
    {
        ObjectPool op = new ObjectPool(defaultCapacity, defaultExpireTime, name, assetPath);
        op.onDestroy += OnPoolDestroy;//注册对象池销毁事件
        objPools.Add(name, op);
        return op;
    }
    /// <summary>
    /// 获取对象池
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public ObjectPool GetObjectPool(string name) {
        ObjectPool op;
        objPools.TryGetValue(name,out op);
        return op;
    }
    /// <summary>
    /// 对象池销毁事件
    /// </summary>
    /// <param name="group"></param>
    private void OnPoolDestroy(string group) {
        objPools.Remove(group);
    }
    /// <summary>
    /// 所有对象池的帧调用
    /// </summary>
    public void Update() {
        //foreach不能删除 
        List<string> keys = new List<string>(objPools.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            objPools[keys[i]].Update();
        }
    }
}
