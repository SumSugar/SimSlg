// ********************************************************
// 描述：实体的数据基类
// 作者：ShadowRabbit 
// 创建时间：2019-04-24 10:41:40
// ********************************************************
using UnityEngine;

public class BaseData 
{
    public string name;
    public Vector3 position;//实体的位置
    public Quaternion rotation;//实体的朝向  
    public BaseData(string name, Vector3 position, Quaternion rotation) {
        this.name = name;
        this.position = position;
        this.rotation = rotation;
    }
}
