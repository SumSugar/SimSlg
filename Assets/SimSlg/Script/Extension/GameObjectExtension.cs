// ********************************************************
// 描述：gameobject的dlc扩展包
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 21:21:04
// ********************************************************

using UnityEngine;

public static class GameObjectExtension 
{
    /// <summary>
    /// 获取物体上的组件 没有的话就添加一个
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(this GameObject obj) where T : Component {
        T component = obj.GetComponent<T>();
        if (component==null)
        {
            component = obj.AddComponent<T>();
        }
        return component;
    }
}
