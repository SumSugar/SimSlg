// ********************************************************
// 描述：实体的逻辑基类
// 作者：ShadowRabbit 
// 创建时间：2019-04-24 10:41:28
// ********************************************************

using UnityEngine;

public abstract class BaseLogic :MonoBehaviour
{
    [HideInInspector]
    public bool visible;//是否可见
    [HideInInspector]
    public bool available;//是否可用
    [HideInInspector]
    public int defaultLayer = 0;//默认层
    [HideInInspector]
    public Transform cachedTransform;//缓存的一个位置
    public virtual void Update()
    {
        
    }
}
