using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMonsterMoveCruve : MonoBehaviour
{
    public float time = 1;          // 代表从A点出发到B经过的时长
    private Transform pointA;        // 点A
    private Transform pointB;        // 点B
    private GameObject testTrigger;
    private Vector3 lastFramePlace;    //上一帧的位置
    public float g = -5;           // 重力加速度

    private Vector3 speed;          // 初速度向量
    private Vector3 Gravity;        // 重力向量


    private float dTime = 0;        // 时间线 (一直在增长)

    private SpriteRenderer spriteRenderer;
    private Texture2D UpBird;
    private Texture2D DownBird;
    void Start()
    {
        lastFramePlace = Vector3.zero;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        UpBird = Resources.Load<Texture2D>("Sprites/flybird");
        DownBird = Resources.Load<Texture2D>("Sprites/flybird1");

        // 重力初始速度为0
        Gravity = Vector3.zero;
    }
    void Line()
    {
        testTrigger = GameObject.FindGameObjectWithTag("FlyMonsterCruveOn");
        if (testTrigger != null)
        {
            pointA = testTrigger.transform.GetChild(1);
            pointB = testTrigger.transform.GetChild(0);
            testTrigger.gameObject.tag = "FlyMonsterCruve";
            // 将物体置于A点
            transform.position = pointA.position;

            // 通过一个式子计算初速度
            speed = new Vector3(
                (pointB.position.x - pointA.position.x) / time,
                (pointB.position.y - pointA.position.y) / time + 2.0f * g * time,
                (pointB.position.z - pointA.position.z) / time);
        }
    }
    void Update()
    {
        Line();
        if (lastFramePlace.y > transform.position.y)
        {
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            Sprite sprite = Sprite.Create(DownBird, spriteRenderer.sprite.textureRect, new Vector2(0.1f, 0.1f));
            spriteRenderer.sprite = sprite;
        }
        else if ((lastFramePlace.y < transform.position.y) || (lastFramePlace.y == transform.position.y))
        {
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            Sprite sprite = Sprite.Create(UpBird, spriteRenderer.sprite.textureRect, new Vector2(0.1f, 0.1f));
            spriteRenderer.sprite = sprite;
        }
        lastFramePlace = transform.position;//逐帧比较y坐标
        // 重力模拟
        Gravity.y = -1.5f * g * (dTime += Time.deltaTime);  //v=gt
        // 模拟位移
        transform.Translate(speed * Time.deltaTime);
        transform.Translate(Gravity * Time.deltaTime);
        Destroy(gameObject, 0.8f);
        //if (Vector3.Distance(gameObject.transform.position, pointB.position) < 0.01f)
        //{
        //    DestroyImmediate(gameObject);
        //}
    }
}
