// ********************************************************
// 描述：输入过滤模块 过滤掉在UI上的input事件 防止UI穿层
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 15:36:15
// ********************************************************


using System;

public class InputFilterModule : BaseModuleSingleton<InputFilterModule>
{
    public event Action<InputCursorInfo> onPressed;//按下事件
    public event Action<InputCursorInfo> onReleased;//释放事件
    public event Action<InputCursorInfo> onTapped;//点击事件
    public event Action<InputCursorInfo> onStartedDrag;//拖拽开始事件
    public event Action<InputCursorInfo> onDragged;//拖拽结束事件
    public event Action<InputCursorInfo> onStartedHold;//长按事件
    public event Action<InputCursorInfo> onMouseMoved;//鼠标移动事件
    public event Action<InputWheelInfo> onSpunWheel;//鼠标滑轮事件
    public override void Init()
    {
        base.Init();
        InputModule.Instance.onPressed += OnPressed;
        InputModule.Instance.onReleased += OnReleased;
        InputModule.Instance.onTapped += OnTapped;
        InputModule.Instance.onStartedDrag += OnStartedDrag;
        InputModule.Instance.onDragged += OnDragged;
        InputModule.Instance.onStartedHold += OnStartedHold;
        InputModule.Instance.onMouseMoved += OnMouseMoved;
        InputModule.Instance.onSpunWheel += OnSpunWheel;
    }
    private void OnPressed(InputCursorInfo ici) {
        if (!ici.startedOverUI)
        {
            onPressed?.Invoke(ici);
        }
    }
    private void OnReleased(InputCursorInfo ici) {
        if (!ici.startedOverUI)
        {
            onReleased?.Invoke(ici);
        }
    }
    private void OnTapped(InputCursorInfo ici) {
        if (!ici.startedOverUI)
        {
            onTapped?.Invoke(ici);
        }
    }
    private void OnStartedDrag(InputCursorInfo ici) {
        if (!ici.startedOverUI)
        {
            onStartedDrag?.Invoke(ici);
        }
    }
    private void OnDragged(InputCursorInfo ici) {
        if (!ici.startedOverUI)
        {
            onDragged?.Invoke(ici);
        }
    }
    private void OnStartedHold(InputCursorInfo ici) {
        if (!ici.startedOverUI)
        {
            onStartedHold?.Invoke(ici);
        }
    }
    private void OnMouseMoved(InputCursorInfo ici) {
        if (!ici.startedOverUI)
        {
            onMouseMoved?.Invoke(ici);
        }
    }
    private void OnSpunWheel(InputWheelInfo iwi) {
        onSpunWheel?.Invoke(iwi);
    }
}
