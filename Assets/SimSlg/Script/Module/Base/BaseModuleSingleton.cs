// ********************************************************
// 描述：单例模块基类
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 14:12:35
// ********************************************************

/// <summary>
/// 组件单例基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseModuleSingleton<T> where T : class, new()
{
    public static T Instance {
        get {
            return Inner.instance;
        }
    }
    class Inner {
        internal static readonly T instance = new T();
    }
}
