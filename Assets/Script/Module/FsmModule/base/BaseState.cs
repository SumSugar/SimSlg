// ********************************************************
// 描述：状态基类
// 作者：ShadowRabbit 
// 创建时间：2019-04-27 16:56:59
// ********************************************************


public abstract class BaseState<T> where T : class
{
    /// <summary>
    /// 进入后执行
    /// </summary>
    public virtual void DoAfterEnter(Fsm<T> fsm) {

    }
    /// <summary>
    /// 离开前执行
    /// </summary>
    public virtual void DoBeforeLeave(Fsm<T> fsm) {

    }
    /// <summary>
    /// 每帧调用
    /// </summary>
    public virtual void OnUpdate(Fsm<T> fsm) {

    }
}
