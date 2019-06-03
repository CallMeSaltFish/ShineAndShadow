using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    /*计时器*/
    private float timer;
    private int num;

    /*实例化的地面*/
    public GameObject[] newFloor;
    /*实例化的次数*/
    private int number = 0;

    private GameObject player;
    private PlayerMove playerMove;
    private MapManager mapManager;
    private float newPositionY;
    private float newPositionX;
    private Transform chapter3Child;
    private int scores;

    // Start is called before the first frame update
    void Start()
    {
        newPositionY = -1.20f;
        mapManager = GameObject.Find("Manager").GetComponent<MapManager>();
        player = GameObject.FindWithTag("Player");
        playerMove = player.GetComponent<PlayerMove>();
        chapter3Child = GameObject.FindWithTag("Map").GetComponent<Transform>();
        if(!chapter3Child.name.Equals("3(Clone)"))
        {
            chapter3Child = null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scores = playerMove.scores;
        timer += Time.fixedDeltaTime;
        //Debug.Log(timer);
        if (timer > 3)
        {
            num++;
            timer = 0;
        }
        if (num == 2)
        {
            num = 0;
            MakeMap();
            Debug.Log("生成地图");
            number++;
        }
    }

    /// <summary>
    /// 实例化新的黑色地面
    /// </summary>
    void MakeMap()
    {
        if (mapManager.chapter == 3)
        {
            int a = Random.Range(0, 35);
            if (number == 1)
            {
                if(chapter3Child != null)
                {
                    Instantiate(newFloor[4], new Vector3(newPositionX + 55, newPositionY + 1.5f, -0.1f), Quaternion.identity, chapter3Child);
                }
                newPositionY += 1.5f;
                newPositionX += 79.25f;
                //newPositionX += 42.5f;
            }
            if (number > 1)
            {
                if ((a < 2 && a > 0) || a == 0)
                {
                    if(chapter3Child != null)
                    {
                        Instantiate(newFloor[0], new Vector3(newPositionX, newPositionY + 2.0f, -0.1f), Quaternion.identity, chapter3Child);
                    }
                    newPositionY += 2.8f;
                    newPositionX += 24.25f;
                }
                if (a > 1 && a < 4)
                {
                    if(chapter3Child != null)
                    {
                        Instantiate(newFloor[1], new Vector3(newPositionX, newPositionY + 2.0f, -0.1f), Quaternion.identity, chapter3Child);
                    }
                    newPositionY += 2.8f;
                    newPositionX += 24.25f;
                }
                if (a > 3 && a < 8)
                {
                    if(chapter3Child != null)
                    {
                        Instantiate(newFloor[2], new Vector3(newPositionX, newPositionY + 2.0f, -0.1f), Quaternion.identity, chapter3Child);
                    }
                    newPositionY += 2.8f;
                    newPositionX += 24.25f;
                }
                if (a > 7 && a < 14)
                {
                    if(chapter3Child != null)
                    {
                        Instantiate(newFloor[3], new Vector3(newPositionX, newPositionY + 2.0f, -0.1f), Quaternion.identity,chapter3Child);
                    }
                    newPositionY += 2.8f;
                    newPositionX += 24.25f;
                }
                if (a > 13 && a < 17)
                {
                    if(chapter3Child != null)
                    {
                        Instantiate(newFloor[4], new Vector3(newPositionX, newPositionY + 1.5f, -0.1f), Quaternion.identity,chapter3Child);
                    }
                    newPositionY += 1.5f;
                    newPositionX += 24.25f;
                }
                if (a > 16 && a < 20)
                {
                    if(chapter3Child != null)
                    {
                        Instantiate(newFloor[5], new Vector3(newPositionX, newPositionY + 1.5f, -0.1f), Quaternion.identity,chapter3Child);
                    }
                    newPositionY += 1.5f;
                    newPositionX += 24.25f;
                }
                if (a > 19 && a < 26)
                {
                    if(chapter3Child != null)
                    {
                        Instantiate(newFloor[6], new Vector3(newPositionX, newPositionY + 1.5f, -0.1f), Quaternion.identity,chapter3Child);
                    }
                    newPositionY += 1.5f;
                    newPositionX += 24.25f;
                }
                if (a > 25 && a < 35)
                {
                    if(chapter3Child != null)
                    {
                        Instantiate(newFloor[7], new Vector3(newPositionX, newPositionY + 1.5f, -0.1f), Quaternion.identity,chapter3Child);
                    }
                    newPositionY += 1.5f;
                    newPositionX += 24.25f;
                }
                if (number > 0)
                {
                    if(chapter3Child != null)
                    {
                        Instantiate(newFloor[8], new Vector3(newPositionX - 12.5f, newPositionY + 2.0f, -0.1f), Quaternion.identity,chapter3Child);
                    }
                    //newPositionY += 2.8f;
                    //newPositionX += 24.25f;
                    this.enabled = false;
                }
            }
        }
    }
}
