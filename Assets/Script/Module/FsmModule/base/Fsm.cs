// ********************************************************
// 描述：状态机
// 作者：ShadowRabbit 
// 创建时间：2019-04-27 17:12:18
// ********************************************************
using System;
using System.Collections.Generic;

public class Fsm<T>: BaseFsm  where T : class
{
    public T owner;//状态机的持有者
    private Dictionary<string, BaseState<T>> states;//状态机中的全部状态
    public BaseState<T> currentState;//当前的状态
    public event Action<string> onDestroy;//销毁事件
    public Fsm(T owner,string name,params BaseState<T>[] baseStates) {
        states = new Dictionary<string, BaseState<T>>();
        if (owner==null)
        {
            throw new Exception("null");
        }
        this.owner = owner;
        if (baseStates==null || baseStates.Length==0)
        {
            throw new Exception("error");
        }
        this.name = name;
        for (int i = 0; i < baseStates.Length; i++)
        {
            if (baseStates[i]==null)
            {
                throw new Exception("null");
            }
            states.Add(baseStates[i].GetType().FullName, baseStates[i]);
        }
        currentState = null;
    }
    /// <summary>
    /// 改变当前状态
    /// </summary>
    /// <typeparam name="Tstate"></typeparam>
    public virtual void ChangeState<Tstate>() where Tstate : BaseState<T> {
        if (currentState==null)
        {
            throw new Exception("null");
        }
        currentState.DoBeforeLeave(this);//离开前执行
        currentState = GetState(typeof(Tstate).FullName);
        if (currentState==null)
        {
            throw new Exception("null");
        }
        currentState.DoAfterEnter(this);//进入后执行
    }
    /// <summary>
    /// 获取状态
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private BaseState<T> GetState(string name) {
        BaseState<T> state ;
        if (states.TryGetValue(name, out state))
        {
            return state;
        }
        else {
            throw new Exception("null");
        }
    }
    /// <summary>
    /// 开始状态机
    /// </summary>
    /// <param name="state"></param>
    public void Start<Tstate>() where Tstate : BaseState<T>
    {
        currentState = GetState(typeof(Tstate).FullName);
        if (currentState==null)
	    {
            throw new Exception("null");
	    }
        currentState.DoAfterEnter(this);
    }
    /// <summary>
    /// 每帧执行
    /// </summary>
    public override void Update()
    {
        if (currentState == null)
        {
            return;
        }
        currentState.OnUpdate(this);
    }
    /// <summary>
    /// 状态机的销毁
    /// </summary>
    public virtual void Destroy() {
        onDestroy(name);
    }
}
