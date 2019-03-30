﻿using System.Collections;
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
        mapManager = GameObject.Find("Manager").GetComponent<MapManager>();
        if (mapManager.chapter == 3 || mapManager.chapter == 2) 
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
            if ((Vector3.Distance(player.transform.position, transform.position) < 4.0f)&&
                (((rotatePlayer.playerHeight < 0) && (gameObject.tag == "Trap")) || ((rotatePlayer.playerHeight > 0) && (gameObject.tag == "DownTrap")))) 
            {
                //Debug.Log(rotatePlayer.playerHeight);
                //Debug.Log(gameObject.tag);
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPosition.transform.position, luckerSpeed * 0.2f);
                if (Vector3.Distance(gameObject.transform.position, endPosition.transform.position) < 0.02f)
                {
                    collider2D.enabled = true;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -4);
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
}
