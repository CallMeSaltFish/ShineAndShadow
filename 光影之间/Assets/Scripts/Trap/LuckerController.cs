using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckerController : MonoBehaviour
{
    /*地刺触发计时器*/
    private float timer;
    private static int num;
    /*地刺运动速度*/
    public float luckerSpeed = 1.0f;
    /*地刺状态*/
    private bool canUp = false;
    /*地刺终点*/
    public GameObject endPosition;

    private Vector3 originPosition;

    private BoxCollider2D collider2D;
    private GameObject player;
    private RotatePlayer rotatePlayer;

    private MapManager mapManager;

    /*0为根据频率出现 1为人过来才出现*/
    private int attackMode = 0;

    public int AttackMode
    {
        get
        {
            return attackMode;
        }
        set
        {
            attackMode = value;
        }
    }
    // Use this for initialization
    void Start()
    {
        originPosition = transform.position;
        collider2D = gameObject.GetComponent<BoxCollider2D>();
        collider2D.enabled = false;
        player = GameObject.FindWithTag("Player");
        rotatePlayer = player.GetComponent<RotatePlayer>();
        mapManager = GameObject.Find("Manager/Map").GetComponent<MapManager>();
        if (mapManager.chapter == 3 || mapManager.chapter == 4)
        {
            attackMode = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attackMode == 0)
        {
            timer += Time.deltaTime;
            //Debug.Log(timer);
            if (timer > 2)
            {
                num++;
                timer = 0;
            }
            if (num == 2)
            {
                canUp = true;
                num = 0;
            }
            //Debug.Log(canUp);
            if (canUp == true)
            {

                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPosition.transform.position, luckerSpeed * 0.2f);
                if (Vector3.Distance(gameObject.transform.position, endPosition.transform.position) < 0.02f)
                {
                    collider2D.enabled = true;
                }
                if (Vector3.Distance(gameObject.transform.position, endPosition.transform.position) < 0.0002f)
                {
                    canUp = false;
                }
                //timer = 0;
                //Debug.Log(1);
            }
            if (canUp == false)
            {
                collider2D.enabled = false;
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, /*endPosition.transform.position - new Vector3(0,
                GetComponent<BoxCollider2D>().size.y * transform.localScale.y, 0)*/originPosition, luckerSpeed * 0.2f);
                //timer = 0;
                //Debug.Log(2);
            }
        }
        if (attackMode == 1)
        {
            if ((Vector3.Distance(player.transform.position, transform.position) < 2.0f)&&
                (((rotatePlayer.playerHeight>0)&&(gameObject.tag=="DownTrap"))||((rotatePlayer.playerHeight<0)&&(gameObject.tag=="Trap"))))
            {

                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPosition.transform.position, luckerSpeed * 0.2f);
                if (Vector3.Distance(gameObject.transform.position, endPosition.transform.position) < 0.02f)
                {
                    collider2D.enabled = true;
                }
                if (Vector3.Distance(gameObject.transform.position, endPosition.transform.position) < 0.0002f)
                {
                    canUp = false;
                }
                //timer = 0;
                //Debug.Log(1);
            }
        }
    }
    //这个碰撞检测在正式场景中被其它方式取代
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {

        }
    }
}
