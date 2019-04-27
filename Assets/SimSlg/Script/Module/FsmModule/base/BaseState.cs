// ********************************************************
// 描述：状态基类
// 作者：ShadowRabbit 
// 创建时间：2019-04-27 16:56:59
// ********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<T> where T : class
{
    /// <summary>
    /// 进入后执行
    /// </summary>
    public virtual void DoAfterEnter() {

    }
    /// <summary>
    /// 离开前执行
    /// </summary>
    public virtual void DoBeforeLeave() {

    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TState">切换后的状态</typeparam>
    /// <param name="fsm">状态机的持有者</param>
    public virtual void ChangeState<TState>(BaseFsm<T> fsm) where TState : BaseState<T>{

    }
    /// <summary>
    /// 每帧调用
    /// </summary>
    public virtual void OnUpdate() {

    }
}
