// ********************************************************
// 描述：状态机
// 作者：ShadowRabbit 
// 创建时间：2019-04-27 17:12:18
// ********************************************************
using System;
using System.Collections.Generic;

public class BaseFsm<T> where T : class
{
    private T owner;//状态机持有者
    private string name;//状态机的名字
    private Dictionary<string, BaseState<T>> states;
    public BaseState<T> currentState;
    public BaseFsm(T owner,string name,params BaseState<T>[] baseStates) {
        this.owner = owner ?? throw new Exception("null");
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
        currentState.DoBeforeLeave();//离开前执行
        currentState = GetState(typeof(Tstate).FullName);
        currentState.DoAfterEnter();//进入后执行
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
    public void Start(BaseState<T> state) {
        currentState = state ?? throw new Exception("null");
    }
    /// <summary>
    /// 每帧执行
    /// </summary>
    public void Update() {
        if (currentState==null)
        {
            throw new Exception("null");
        }
        currentState.OnUpdate();
    }
}
