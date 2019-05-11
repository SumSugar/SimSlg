// ********************************************************
// 描述：测试地图
// 作者：ShadowRabbit 
// 创建时间：2019-05-11 16:51:14
// ********************************************************
using UnityEngine;

public class TestMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //inputFilter用于发送鼠标事件 到 非ugui的物体
        //监听输入过滤模块的 点击事件
        InputFilterModule.Instance.onTapped += OnTap;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        InputFilterModule.Instance.onTapped -= OnTap;
    }
    void OnTap(InputCursorInfo info) {
        //如果点击到UI上 则这个事件会被inputfilter拦截 可以尝试一下
        Debug.Log("点击了屏幕的("+info.currentPosition.x+","+info.currentPosition.y+")");
    }
}
