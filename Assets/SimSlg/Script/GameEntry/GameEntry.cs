// ********************************************************
// 描述：游戏入口 总控制器
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 15:52:54
// ********************************************************

using System.Collections.Generic;
using UnityEngine;

public class GameEntry : MonoBehaviour
{
    private void Awake()
    {
        //防止场景切换时销毁主控制器
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        //监听事件传递
        EventModule.Instance.Update();
        //监听输入
        InputModule.Instance.Update();
        //对象池自动回收
        ObjPoolModule.Instance.Update();
    }
    private void Start()
    {
    }
}
