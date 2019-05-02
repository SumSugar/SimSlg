// ********************************************************
// 描述：状态机模块 （不包含主控制器的状态机）
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 15:28:23
// ********************************************************

using System;
using System.Collections.Generic;
public class FsmModule : BaseModuleSingleton<FsmModule> 
{
    private Dictionary<string, BaseFsm> fsms= new Dictionary<string, BaseFsm>();//全部的状态机
    /// <summary>
    /// 创建状态机
    /// </summary>
    /// <param name="fsm"></param>
    /// <returns></returns>
    public Fsm<T> CreateFsm<T>(T owner,string name, params BaseState<T>[] baseStates) where T : class
    {
        if (baseStates==null || baseStates.Length==0)
        {
            throw new Exception("null");
        }
        Fsm<T> fsm = new Fsm<T>(owner, name, baseStates);
        fsm.onDestroy += RemoveFsm;//监听状态机销毁事件
        return fsm;
    }
    /// <summary>
    /// 撤销状态机
    /// </summary>
    /// <param name="name"></param>
    public void RemoveFsm(string name) {
        if (fsms.ContainsKey(name))
        {
            fsms.Remove(name);
        }
    }
    /// <summary>
    /// 获取状态机
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public BaseFsm GetFsm(string name)
    {
        if (fsms.ContainsKey(name))
        {
            return fsms[name];
        }
        throw new Exception("null");
    }
    /// <summary>
    /// 获取全部状态机
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, BaseFsm> GetAllFsms()
    {
        return fsms;
    }
}
