// ********************************************************
// 描述：游戏主控制器状态机
// 作者：ShadowRabbit 
// 创建时间：2019-04-28 12:14:17
// ********************************************************


public class GameFsm : Fsm<GameEntry>
{
    private int nextSceneId;//下个场景的id
    public GameFsm(GameEntry owner, string name, params BaseState<GameEntry>[] baseStates) : base(owner, name, baseStates)
    {
    }
    /// <summary>
    /// 设置下个场景
    /// </summary>
    /// <param name="id"></param>
    public void setNextSceneId(int id) { nextSceneId = id; }
}
