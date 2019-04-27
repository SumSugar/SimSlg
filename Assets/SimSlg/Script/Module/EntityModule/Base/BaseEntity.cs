// ********************************************************
// 描述：实体的基类
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 22:05:21
// ********************************************************
using System;
using UnityEngine;

public class BaseEntity : MonoBehaviour,IPoolObject
{
    public int entityId;//实体的id
    public BaseData entityData;//数据
    public BaseLogic entityLogic;//逻辑
    public string entityGroup;//实体所属的组
    public string entityAssetPath;//实体的资源路径
    public string entityName;//实体的名字
    public int expireTime;//回收后的实体 销毁时间
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
    /// 实体每帧执行的逻辑
    /// </summary>
    private void Update() {
        if (entityLogic==null)
        {
            return;
        }
        entityLogic.OnUpdate();//自身的逻辑
    }
    /// <summary>
    /// 实体的初始化方法
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="data"></param>
    /// <param name="logic"></param>
    /// <param name="entityGroup"></param>
    /// <param name="assetPath"></param>
    /// <param name="expireTime">销毁时间</param>
    public void OnInit(int entityId,BaseData data,BaseLogic logic,string entityGroup,string assetPath, int expireTime) {
        if (data==null || logic==null)
        {
            new Exception("null");
        }
        this.entityId = entityId;
        this.entityData = data;
        this.entityLogic = logic;
        this.entityGroup = entityGroup;
        this.entityAssetPath = assetPath;
        this.entityName = gameObject.name;
        this.expireTime = expireTime;
    }
    /// <summary>
    /// 实体生成
    /// </summary>
    public virtual void OnSpawn() {
        entityLogic.available = false;
        entityLogic.visible = true;
        gameObject.transform.position = entityData.position;
        gameObject.transform.rotation = entityData.rotation;
        gameObject.SetActive(entityLogic.visible);
    }
    /// <summary>
    /// 实体回收
    /// </summary>
    public virtual void OnUnSpawn() {
        recycleTime = DateTime.Now;
        entityLogic.available = true;
        entityLogic.visible = false;
        gameObject.transform.position = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.layer = entityLogic.defaultLayer;
        gameObject.SetActive(entityLogic.visible);
    }
    /// <summary>
    /// 物体销毁
    /// </summary>
    public void Destroy()
    {
        onDestroy(this);
        Destroy(gameObject);
    }
    /// <summary>
    /// 获取实体逻辑
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetLogic<T>() where T:BaseLogic{
        return this.entityLogic as T;
    }
    /// <summary>
    /// 获取实体数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetData<T>() where T : BaseData {
        return this.entityData as T;
    }
}
