// ********************************************************
// 描述：鼠标信息
// 作者：ShadowRabbit 
// 创建时间：2019-04-29 18:43:30
// ********************************************************


public class InputMouseButtonInfo : InputCursorActionInfo
{
    /// <summary>
    /// 是否已经按下
    /// </summary>
    public bool isDown;
    /// <summary>
    /// 鼠标的id  0左键 1右键
    /// </summary>
    public int mouseButtonId;
}
