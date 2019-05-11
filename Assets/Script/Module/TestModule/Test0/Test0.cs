// ********************************************************
// 描述：演示如何召唤与回收实体
// 作者：ShadowRabbit 
// 创建时间：2019-05-11 14:59:14
// ********************************************************
using UnityEngine;

//调用Entity模块创建与回收实体

//以下原理内部调用 不需要控制
//Entity模块内部调用objPool模块根据group名字创建默认对象池
//objPool模块根据group找到相应的对象池 调用对象池创建实体
//默认对象池 储存对象超过99个后 或对象被回收超过60秒没有使用 不再回收直接销毁
//可以手动创建对象池 并设置内部的过期时间与储存量,没有手动创建的情况下 模块自动按默认配置创建
//对象池模块内部自动管理
public class Test0 : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    void Update()
    {
        //按下Q键 在鼠标位置召唤cube实体
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //1.构建数据类
                BaseData baseData = new BaseData("Cube", hit.point, Quaternion.identity);
                //2.继承Entity（如果是UI则继承UIEntity），实现回收 创建等回调方法
                //3.为该实体起一个组名称，默认为data中的实体名字
                //4.创建一个预设物 并记录预设物的资源地址
                //5.使用实体模块召唤物体
                EntityModule.Instance.SpawnEntity<Entity>(baseData, "PrefabAsset/Object/Test/Cube");
            }
        }
        //按下W键 在鼠标位置回收某个实体
        if (Input.GetKeyDown(KeyCode.W))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name+"被回收");
                BaseEntity objentity = hit.collider.gameObject.GetComponent<BaseEntity>();
                EntityModule.Instance.UnSpawnEntity(objentity);
            }
        }

    }
}
