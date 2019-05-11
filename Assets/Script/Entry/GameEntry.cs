// ********************************************************
// 描述：游戏入口 总控制器
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 15:52:54
// ********************************************************

using UnityEngine;

public class GameEntry : MonoBehaviour
{
    GameFsm gameFsm=null;//游戏控制器状态
    private void Awake()
    {
        InputModule.Instance.Init();
        InputFilterModule.Instance.Init();
        TestModule.Instance.Init();
        //防止场景切换时销毁主控制器
        DontDestroyOnLoad(gameObject);
    }
    private void Start() {
        InitFsm();
    }
    private void Update()
    {
        //监听输入
        InputModule.Instance.Update();
        //监听事件传递
        EventModule.Instance.Update();
        //对象池过期查询
        ObjPoolModule.Instance.Update();
        //自身状态机
        gameFsm.currentState.OnUpdate(gameFsm);
        //测试代码
        TestModule.Instance.Update();
    }
    /// <summary>
    /// 加载状态机
    /// </summary>
    private void InitFsm() {
        GameStateChangeScene cs = new GameStateChangeScene();
        GameStateGame g = new GameStateGame();
        GameStateInitAsset ia = new GameStateInitAsset();
        GameStateInitData id = new GameStateInitData();
        GameStateLunch l = new GameStateLunch();
        GameStateSplash s = new GameStateSplash();
        gameFsm = new GameFsm(this,"GameEntry",new BaseGameState[] {cs,g,ia,id,l,s});
        gameFsm.Start<GameStateLunch>();
    }
}
