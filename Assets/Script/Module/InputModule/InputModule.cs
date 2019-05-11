// ********************************************************
// 描述：输入模块 负责向inputFilter模块发送事件 并且可以控制所有UI的状态
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 15:35:56
// ********************************************************

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputModule : BaseModuleSingleton<InputModule>
{
    private EnumUIState currentState;//当前UI状态 关闭时 所有UI失效
    private readonly float dragThresholdMouse=0f;//规定拖拽多远算作拖拽开始
    private readonly float tapTime = 0.2f;//taptime时间内算作点击
    private readonly float holdTime = 0.8f;//holdtime后算作长按
    private readonly float mouseWheelSensitivity = 1.0f;//滚轮默认值
    private readonly int trackMouseButtons = 2;//鼠标按键数量 默认只有左右键
    private InputCursorInfo cursorInfo;//当前光标信息 
    private InputMouseButtonInfo[] mouseInfos;//鼠标信息
    public event Action<EnumUIState> onUIStateChanged;//UI控制器状态改变事件
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
        currentState = EnumUIState.Enable;
        // 光标是否初始化
        if (Input.mousePresent)
        {
            mouseInfos = new InputMouseButtonInfo[trackMouseButtons];
            cursorInfo = new InputCursorInfo { currentPosition = Input.mousePosition };
            for (int i = 0; i < mouseInfos.Length; ++i)
            {
                mouseInfos[i] = new InputMouseButtonInfo { mouseButtonId = i, currentPosition = Input.mousePosition };
            }
        }
        //UnityInput.simulateMouseWithTouches = false;
    }
    /// <summary>
    /// 在gameEntry中调用
    /// </summary>
    public void Update()
    {
        if (cursorInfo!=null)
        {
            UpdateMouse();
        }
    }
    /// <summary>
    /// 更新鼠标信息
    /// </summary>
    private void UpdateMouse() {
        cursorInfo.previousPosition = cursorInfo.currentPosition;
        cursorInfo.currentPosition = Input.mousePosition;
        cursorInfo.delta = cursorInfo.currentPosition - cursorInfo.previousPosition;//一帧内位移向量
        // 移动事件
        if (cursorInfo.delta.sqrMagnitude > Mathf.Epsilon)
        {
            onMouseMoved?.Invoke(cursorInfo);
        }
        // 鼠标动作事件
        for (int i = 0; i < mouseInfos.Length; ++i)
        {
            mouseInfos[i].delta = cursorInfo.delta;
            mouseInfos[i].previousPosition = cursorInfo.previousPosition;
            mouseInfos[i].currentPosition = cursorInfo.currentPosition;

            if (Input.GetMouseButton(i))
            {
                if (!mouseInfos[i].isDown)
                {
                    // 第一次按下
                    mouseInfos[i].isDown = true;
                    mouseInfos[i].startPosition = Input.mousePosition;
                    mouseInfos[i].startTime = Time.realtimeSinceStartup;
                    mouseInfos[i].startedOverUI = EventSystem.current.IsPointerOverGameObject(-mouseInfos[i].mouseButtonId - 1);
                    // 重置状态
                    mouseInfos[i].totalMovement = 0;
                    mouseInfos[i].isDrag = false;
                    mouseInfos[i].wasHold = false;
                    mouseInfos[i].isHold = false;
                    onPressed?.Invoke(mouseInfos[i]);
                }
                else
                {
                    float moveDist = mouseInfos[i].delta.magnitude;//一帧内的位移
                    mouseInfos[i].totalMovement += moveDist; // 拖拽距离
                    //超过了拖拽触发
                    if (mouseInfos[i].totalMovement > dragThresholdMouse)
                    {
                        //是否已经处于拖拽状态
                        bool wasDrag = mouseInfos[i].isDrag;
                        
                        if (mouseInfos[i].isHold)
                        {
                            mouseInfos[i].wasHold = mouseInfos[i].isHold;
                            mouseInfos[i].isHold = false;
                        }
                        // 如果之前不是拖拽状态 触发开始拖拽事件
                        if (!wasDrag)
                        {
                            onStartedDrag?.Invoke(mouseInfos[i]);
                        }
                        onDragged?.Invoke(mouseInfos[i]);
                        mouseInfos[i].isDrag = true;//设置拖拽状态
                    }
                    else
                    {
                        // 没有拖拽 按住
                        if (!mouseInfos[i].isHold && !mouseInfos[i].isDrag && Time.realtimeSinceStartup - mouseInfos[i].startTime >= holdTime)
                        {
                            mouseInfos[i].isHold = true;
                            onStartedHold?.Invoke(mouseInfos[i]);
                        }
                    }
                }
            }
            else //没有按
            {
                if (mouseInfos[i].isDown) // 释放
                {
                    mouseInfos[i].isDown = false;
                    // 是否满足点击条件
                    if (!mouseInfos[i].isDrag && Time.realtimeSinceStartup - mouseInfos[i].startTime < tapTime)
                    {
                        onTapped?.Invoke(mouseInfos[i]);
                    }
                    onReleased?.Invoke(mouseInfos[i]);
                }
            }
        }
        // 鼠标滚轴
        if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > Mathf.Epsilon)
        {
            onSpunWheel?.Invoke(new InputWheelInfo
            {
                zoomAmount = Input.GetAxis("Mouse ScrollWheel") * mouseWheelSensitivity
            });
        }
    }
    /// <summary>
    /// 设置UI是否可用
    /// </summary>
    /// <param name="state"></param>
    public void setUIState(EnumUIState state) {
        currentState = state;
        onUIStateChanged?.Invoke(currentState);
    }
}
