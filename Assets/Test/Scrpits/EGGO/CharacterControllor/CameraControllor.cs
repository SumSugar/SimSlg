using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllor : MonoBehaviour
{
    public Transform player;
    Vector3 distance;
    void Start()
    {
        distance = transform.position - player.position;
    }
    void LateUpdate()
    {
        transform.position = player.position + distance;
    }
}