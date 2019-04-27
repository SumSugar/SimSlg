using System;
// ********************************************************
// 描述：实体的基类
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 22:05:21
// ********************************************************
using UnityEngine;

public class BaseEntity : MonoBehaviour,IPoolObject
{
    public int entityId;//实体的id
    public BaseData entityData;//数据
    public BaseLogic entityLogic;//逻辑
    public string entityGroup;//实体所属的组
    public string entityAssetPath;//实体的资源路径
    public string entityName;//实体的名字
    public float expireTime;//回收后的实体 销毁时间
    private DateTime lastUseTime;//上一次使用时间 用于对象池过期释放判断
    private event Action<BaseEntity> onDestroy;//销毁事件
    private void Start()
    {
        onDestroy += Destroy;//添加销毁事件
    }
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
        entityLogic.OnUpdate();
        DateTime timeNow = new DateTime();
        if ((timeNow-lastUseTime).TotalSeconds>expireTime)
        {
            //通知对象池销毁
            onDestroy(this);
        }
    }
    /// <summary>
    /// 实体的初始化方法
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="data"></param>
    /// <param name="logic"></param>
    /// <param name="entityGroup"></param>
    /// <param name="assetPath"></param>
    public void OnInit(int entityId,BaseData data,BaseLogic logic,string entityGroup,string assetPath) {
        this.entityId = entityId;
        this.entityData = data;
        this.entityLogic = logic;
        this.entityGroup = entityGroup;
        this.entityAssetPath = assetPath;
    }
    /// <summary>
    /// 实体生成
    /// </summary>
    public void OnSpawn() {
        entityLogic.OnSpawn();
    }
    /// <summary>
    /// 实体回收
    /// </summary>
    public void OnUnSpawn() {
        entityLogic.OnUnSpawn();
    }
    /// <summary>
    /// 物体销毁
    /// </summary>
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
