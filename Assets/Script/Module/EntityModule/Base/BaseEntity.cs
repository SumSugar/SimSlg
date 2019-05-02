// ********************************************************
// 描述：实体的基类
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 22:05:21
// ********************************************************
using System;
using UnityEngine;

public class BaseEntity :BaseLogic,IPoolObject
{
    public int entityId;//实体的id
    private BaseData entityData;//数据
    public string entityGroup;//实体所属的组
    public string entityAssetPath;//实体的资源路径
    public string entityName;//实体的名字
    public DateTime recycleTime;//上一次回收时间 用于对象池过期释放判断
    public event Action<BaseEntity> onDestroy;//销毁事件
    /// <summary>
    /// 获取实体实例
    /// </summary>
    /// <returns></returns>
    public GameObject getInstance() {
        return gameObject;
    }
    /// <summary>
    /// 实体的初始化方法
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="data"></param>
    /// <param name="logic"></param>
    /// <param name="entityGroup"></param>
    /// <param name="assetPath"></param>
    public virtual void OnInit(int entityId,BaseData data,string entityGroup,string assetPath) {
        if (data==null)
        {
            new Exception("null");
        }
        this.entityId = entityId;
        this.entityData = data;
        this.entityGroup = entityGroup;
        this.entityAssetPath = assetPath;
        this.entityName = gameObject.name + entityId;
    }
    /// <summary>
    /// 实体生成
    /// </summary>
    public virtual void OnSpawn(BaseData data) {
        available = false;
        visible = true;
        if (data!=null)
        {
            gameObject.transform.position = data.position;
            gameObject.transform.rotation = data.rotation;
        }
        gameObject.SetActive(visible);
    }
    /// <summary>
    /// 实体回收
    /// </summary>
    public virtual void OnUnSpawn() {
        recycleTime = DateTime.Now;
        available = true;
        visible = false;
        gameObject.SetActive(visible);
        gameObject.transform.position = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.layer = defaultLayer;
    }
    /// <summary>
    /// 物体销毁
    /// </summary>
    public virtual void Destroy()
    {
        onDestroy(this);
        Destroy(gameObject);
    }
    /// <summary>
    /// 获取实体数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetData<T>() where T : BaseData {
        if (entityData==null)
        {
            throw new Exception("null");
        }
        return this.entityData as T;
    }
}
