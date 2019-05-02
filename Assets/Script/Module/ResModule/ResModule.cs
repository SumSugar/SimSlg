// ********************************************************
// 描述：资源管理模块
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 15:28:54
// ********************************************************


using UnityEngine;
public class ResModule : BaseModuleSingleton<ResModule>
{

    public Object LoadRes(string assetPath) {
        return Resources.Load(assetPath);
    }
}
