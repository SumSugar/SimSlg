// ********************************************************
// 描述：光标信息
// 作者：ShadowRabbit 
// 创建时间：2019-04-29 18:32:20
// ********************************************************

using UnityEngine;

public class InputCursorInfo
{
    public Vector2 currentPosition;// 当前位置
    public Vector2 previousPosition;// 上一个位置
    public Vector2 delta;// 一帧内的位移向量
    public bool startedOverUI;// 当前点是否存在UI
}
