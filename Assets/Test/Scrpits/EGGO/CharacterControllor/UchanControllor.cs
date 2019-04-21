using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UchanControllor : MonoBehaviour {
    private Animator m_ani; //动画
    private Transform m_transform; //人物对象
    private Vector3 inputSpeed;//接受键盘输入的速度参数
    public Transform camera; //相机对象

    private CharacterController controller;

    public float moveSpeed = 100.0f; //移动速度
    public float gravity = 20.0f; //重力
    public float jumpSpeed = 10.0f; //跳跃力
    private Vector3 moveDirection = Vector3.zero;
    public Vector2 rotLimt = new Vector2(-30, 60); //鼠标控制转向的极限角度

    // Use this for initialization
    void Start () {
        m_ani = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }
    void Update()
    {
        //接收输入
        //inputSpeed = (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))).normalized;//速度单位化
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 targetDir = new Vector3(h, 0, v);
        //if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1)
        //{
        //    transform.LookAt(targetDir + transform.position);
        //    //controller.SimpleMove(transform.forward * moveSpeed);
        //}
        moveDirection = transform.TransformDirection(targetDir);

        //m_ani.SetFloat("SpeedY", Mathf.Abs(h)+ Mathf.Abs(v));

        if (controller.isGrounded)
        {
            //m_ani.SetBool("IsJump", false);
            moveDirection = new Vector3(h, 0, v);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
            if (Input.GetButton("Jump"))
            {
                //m_ani.SetBool("IsJump", true);
                moveDirection.y = jumpSpeed;
            }
        }

        //inputSpeed *= moveSpeed;//乘于速度后得出输入的位移
        //ani.SetFloat("SpeedX", inputSpeed.x);
        //inputSpeed.y = rd.velocity.y;
        //rd.velocity = inputSpeed;

        //控制人物转向
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        //rot0 = Mathf.Clamp(rot0 - Input.GetAxis("Mouse Y"), rotLimt.x, rotLimt.y);
        //cameraRoot.localEulerAngles = new Vector3(rot0, 0, 0);
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }


}
