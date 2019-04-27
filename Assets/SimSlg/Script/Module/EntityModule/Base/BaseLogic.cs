using System;
// ********************************************************
// 描述：实体的逻辑基类
// 作者：ShadowRabbit 
// 创建时间：2019-04-24 10:41:28
// ********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLogic 
{
    public bool visible;//是否可见
    public bool available;//是否可用
    public int defaultLayer = 0;//默认层
    public Transform cachedTransform;//缓存的一个位置
    /// <summary>
    /// 每一帧调用
    /// </summary>
    protected internal virtual void OnUpdate() { 
    
    }
}
