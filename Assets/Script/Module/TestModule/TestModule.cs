// ********************************************************
// 描述：测试专用模块
// 作者：ShadowRabbit 
// 创建时间：2019-05-02 10:35:15
// ********************************************************


public class TestModule : BaseModuleSingleton<TestModule>
{
    public override void Init()
    {
        base.Init();
        EntityModule.Instance.SpawnEntity<UIEntity>(null, "ui", "PrefabAsset/UI/TestUI");
    }
    public void Update()
    {
        
    }
}
