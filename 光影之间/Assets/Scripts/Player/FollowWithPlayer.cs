﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWithPlayer : MonoBehaviour {

    [SerializeField]
    private Transform playerTransform;
    [Range(0,1)]
    public float cameraSpeed;
    private Vector3 cameraVelocity = Vector3.zero;
    private Vector3 offset;
    private Vector3 endPosition;
    private PlayerMove playerMove;
    private MapManager mapManager;
    private BossMove BossB;
    private BossMove BossW;
    [HideInInspector]
    public bool isStd;
    [HideInInspector]
    public bool isSpecial;
    [HideInInspector]
    public bool chapter3Special;

    private void Awake()
    {
        mapManager = GameObject.FindWithTag("GameController").GetComponent<MapManager>();
    }
    // Use this for initialization
    void Start () {
        offset = transform.position - playerTransform.position;
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        BossB = GameObject.Find("BossB").GetComponent<BossMove>();
        BossW = GameObject.Find("BossW").GetComponent<BossMove>();
        chapter3Special = false;
    }

    // Update is called once per frame
    void LateUpdate () {
        endPosition = playerTransform.position + offset;
        if(mapManager.chapter == 2 && isSpecial)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(endPosition.x, Mathf.Clamp(playerTransform.position.y, 0f, 7.2f), transform.position.z), ref cameraVelocity, cameraSpeed);
        }
        if(mapManager.chapter == 3)
        {
            if(chapter3Special)
            {
                transform.Translate(Vector3.right * Time.deltaTime * 2);
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, playerTransform.position.y, transform.position.z), ref cameraVelocity, cameraSpeed);
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(endPosition.x, playerTransform.position.y, transform.position.z), ref cameraVelocity, cameraSpeed);
            }
        }
        else
        {
            if (!isStd)
            {
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(endPosition.x, transform.position.y, endPosition.z), ref cameraVelocity, cameraSpeed);
                if (transform.position.x - playerTransform.position.x >= offset.x - 0.1f && Input.GetMouseButtonDown(2))
                {
                    playerMove.enabled = true;
                    isStd = true;
                }
            }
            if (isStd)
            {
                transform.position = new Vector3(endPosition.x, transform.position.y, endPosition.z);
            }
        }
    }

    private void OnEnable()
    {
        if (mapManager.chapter == 4)
        {
            offset = transform.position - playerTransform.position;
        }
    }
}
