// ********************************************************
// 描述：对象池模块
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 15:27:20
// ********************************************************

using System;
using System.Collections.Generic;

public class ObjPoolModule : BaseModuleSingleton<ObjPoolModule> {
    private int defaultCapacity = 99;//池内对象的默认最大堆叠数
    private float defaultExpireTime = 60f;//池内对象的默认过期时间
    private Dictionary<string, ObjectPool> objPools = new Dictionary<string, ObjectPool>();//全部对象池
    //private 
    /// <summary>
    /// 生成实体
    /// </summary>
    /// <param name="name">对象池的名称</param>
    /// <param name="assetPath">资源路径</param>
    /// <returns></returns>
    public BaseEntity SpawnEntity(string name,string assetPath){
        ObjectPool op = GetObjectPool(name);
        if (op==null)
        {
            op=CreateObjectPool(name);
        }
        return op.SpawnEntity(assetPath);
    }
    /// <summary>
    /// 回收实体
    /// </summary>
    public void UnSpawnEntity(BaseEntity entity) {
        if (entity==null)
        {
            throw new Exception("null");
        }
        ObjectPool op = GetObjectPool(entity.name);
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
    public ObjectPool CreateObjectPool(int capacity, float expireTime ,string name)
    {
        ObjectPool pool = new ObjectPool(capacity, expireTime, name);
        objPools.Add(name,pool);
        return pool;
    }
    /// <summary>
    /// 创建对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expireTime"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public ObjectPool CreateObjectPool(float expireTime, string name)
    {
        ObjectPool pool = new ObjectPool(defaultCapacity, expireTime, name);
        objPools.Add(name, pool);
        return pool;
    }
    /// <summary>
    /// 创建对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public ObjectPool CreateObjectPool(string name)
    {
        ObjectPool pool = new ObjectPool(defaultCapacity, defaultExpireTime, name);
        objPools.Add(name, pool);
        return pool;
    }
    /// <summary>
    /// 撤销对象池
    /// </summary>
    /// <param name="name"></param>
    public void RemoveObjectPool(string name){
        ObjectPool op=GetObjectPool(name);
        if (op==null)
        {
            throw new Exception("撤销对象不存在");
        }
        objPools.Remove(name);
        op = null;
    }
    /// <summary>
    /// 获取对象池
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public ObjectPool GetObjectPool(string name) {
        ObjectPool op;
        objPools.TryGetValue(name,out op);
        if (op==null)
        {
            throw new Exception("null");
        }
        return op;
    }
    /// <summary>
    /// 自动释放资源
    /// </summary>
    public void Update() {
        foreach (var pool in objPools.Values)
        {
            pool.Update();
        }
    }
}
