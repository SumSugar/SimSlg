// ********************************************************
// 描述：RFUI抽象类
// 作者：ShadowRabbit 
// 创建时间：2019-05-11 14:25:36
// ********************************************************

using UnityEngine;

public abstract class RFUI : MonoBehaviour
{
    [SerializeField]
    protected int id = 0;//UI逻辑中的编号
    protected bool active;//是否激活中
    protected CanvasGroup cg = null;
    protected virtual void Awake()
    {
        active = true;
        cg = gameObject.GetOrAddComponent<CanvasGroup>();
        //监听UI控制器是否可用
        InputModule.Instance.onUIStateChanged += OnUIStateChanged;
    }
    /// <summary>
    /// 销毁时撤销监听
    /// </summary>
    protected virtual void OnDestroy()
    {
        InputModule.Instance.onUIStateChanged -= OnUIStateChanged;
    }
    /// <summary>
    /// 设置是否接收射线
    /// </summary>
    /// <param name="state"></param>
    protected virtual void OnUIStateChanged(EnumUIState state) {
        active = state == EnumUIState.Enable ? true : false;
        if (!active)
        {
            StopAllCoroutines();
            cg.alpha = 1;
            transform.localScale = Vector3.one;
        }
    }
}
