// ********************************************************
// 描述：游戏初始状态 负责加载配置文件相关
// 作者：ShadowRabbit 
// 创建时间：2019-04-29 12:27:26
// ********************************************************


using UnityEngine;

public class GameStateLunch : BaseGameState
{
    public override void DoAfterEnter(Fsm<GameEntry> fsm)
    {
        base.DoAfterEnter(fsm);
        Debug.Log("game start");
    }
    public override void DoBeforeLeave(Fsm<GameEntry> fsm)
    {
        base.DoBeforeLeave(fsm);
    }
    public override void OnUpdate(Fsm<GameEntry> fsm)
    {
        base.OnUpdate(fsm);
    }
}
