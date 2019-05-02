// ********************************************************
// 描述：光标动作信息
// 作者：ShadowRabbit 
// 创建时间：2019-04-29 19:00:36
// ********************************************************

using UnityEngine;

public class InputCursorActionInfo : InputCursorInfo
{
    /// <summary>
    /// 起始位置
    /// </summary>
    public Vector2 startPosition;
    /// <summary>
    /// 弹开速度
    /// </summary>
    public Vector2 flickVelocity;
    /// <summary>
    /// 按下后总位移
    /// </summary>
    public float totalMovement;
    /// <summary>
    /// 开始按下的时间
    /// </summary>
    public float startTime;
    /// <summary>
    /// 是否拖拽中
    /// </summary>
    public bool isDrag;
    /// <summary>
    /// 是否长按中
    /// </summary>
    public bool isHold;
    /// <summary>
    /// 是否长按后开始拖拽
    /// </summary>
    public bool wasHold;
}
