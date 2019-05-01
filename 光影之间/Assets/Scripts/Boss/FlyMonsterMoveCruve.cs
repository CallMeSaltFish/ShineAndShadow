using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMonsterMoveCruve : MonoBehaviour
{
    public float time = 1;          // 代表从A点出发到B经过的时长
    private Transform pointA;        // 点A
    private Transform pointB;        // 点B
    public float g = -5;           // 重力加速度

    private Vector3 speed;          // 初速度向量
    private Vector3 Gravity;        // 重力向量


    private float dTime = 0;        // 时间线 (一直在增长)


    void Start()
    {

        pointA = GameObject.FindGameObjectWithTag("StartPoint").transform;
        pointB = GameObject.FindGameObjectWithTag("EndPoint").transform;
        // 将物体置于A点
        transform.position = pointA.position;

        // 通过一个式子计算初速度
        speed = new Vector3(
            (pointB.position.x - pointA.position.x) / time,
            (pointB.position.y - pointA.position.y) / time - 0.5f * g * time,
            (pointB.position.z - pointA.position.z) / time);

        // 重力初始速度为0
        Gravity = Vector3.zero;
    }

    void Update()
    {

        // 重力模拟
        Gravity.y = g * (dTime += Time.deltaTime);  //v=gt
        // 模拟位移
        transform.Translate(speed * Time.deltaTime);
        transform.Translate(Gravity * Time.deltaTime);
        if (Vector3.Distance(gameObject.transform.position, pointB.position) < 0.01f)
        {
            DestroyImmediate(gameObject);
        }
    }
}
