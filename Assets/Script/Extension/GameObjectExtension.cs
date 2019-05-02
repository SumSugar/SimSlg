// ********************************************************
// 描述：gameobject的dlc扩展包
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 21:21:04
// ********************************************************

using System;
using UnityEngine;

public static class GameObjectExtension 
{
    /// <summary>
    /// 获取物体上的组件 没有的话就添加一个
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component {
        T component = gameObject.GetComponent<T>();
        if (component==null)
        {
            component = gameObject.AddComponent<T>();
        }
        return component;
    }
    /// <summary>
    /// 获取或增加组件。
    /// </summary>
    /// <param name="gameObject">目标对象。</param>
    /// <param name="type">要获取或增加的组件类型。</param>
    /// <returns>获取或增加的组件。</returns>
    public static Component GetOrAddComponent(this GameObject gameObject, Type type)
    {
        Component component = gameObject.GetComponent(type);
        if (component == null)
        {
            component = gameObject.AddComponent(type);
        }
        return component;
    }
}
