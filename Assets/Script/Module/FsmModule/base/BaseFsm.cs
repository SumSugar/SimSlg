// ********************************************************
// 描述：状态机抽象类
// 作者：ShadowRabbit 
// 创建时间：2019-04-28 11:59:45
// ********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFsm 
{
    public string name;//状态机的名字
    public abstract void Update();
}
