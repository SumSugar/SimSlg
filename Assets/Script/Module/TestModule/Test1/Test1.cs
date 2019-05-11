// ********************************************************
// 描述：如何使用UI
// 作者：ShadowRabbit 
// 创建时间：2019-05-11 15:47:05
// ********************************************************
using UnityEngine;

public class Test1 : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //按下E键 召唤TestUI实体
        if (Input.GetKeyDown(KeyCode.E))
        {
            //通常情况下UIEntity不需要baseData数据，位置信息在prefab里设置好了，也不会移动
            //如果有移动，并且重新召唤时回到某个位置的需求 可以添加baseData存一下初始位置的数据
            //关于UIEntity的特性，打开TestUIEntity查看
            EntityModule.Instance.SpawnEntity<TestUIEntity>(null, "ui", "PrefabAsset/UI/Test/TestUI");
            //回收方法和普通Entity一样
        }
        //按下R键 设置UI控制器状态为开启 UI控制器用于某些情况 比如拖拽建筑物时 建造状态点击UI无效
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("UI可用");
            InputModule.Instance.setUIState(EnumUIState.Enable);
        }
        //按下T键 设置UI控制器状态为关闭
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("UI禁用");
            InputModule.Instance.setUIState(EnumUIState.Disable);
        }
    }
}
