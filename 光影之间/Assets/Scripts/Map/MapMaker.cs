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
    private MapManager mapManager;
    private float newPositionY;

    // Start is called before the first frame update
    void Start()
    {
        newPositionY = -1.15f;
        mapManager = GameObject.Find("Manager").GetComponent<MapManager>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        //Debug.Log(timer);
        if (timer > 3)
        {
            num++;
            timer = 0;
        }
        if (num == 7)
        {
            num = 0;
            MakeMap();
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
                Instantiate(newFloor[4], new Vector3(player.transform.position.x + 26, newPositionY + 1.5f, -0.1f), Quaternion.identity);
                newPositionY += 1.5f;
            }
            if (number > 1)
            {
                if ((a < 2 && a > 0) || a == 0)
                {
                    Instantiate(newFloor[0], new Vector3(player.transform.position.x + 26, newPositionY + 2.0f, -0.1f), Quaternion.identity);
                    newPositionY += 2.8f;
                }
                if (a > 1 && a < 4)
                {
                    Instantiate(newFloor[1], new Vector3(player.transform.position.x + 26, newPositionY + 2.0f, -0.1f), Quaternion.identity);
                    newPositionY += 2.8f;
                }
                if (a > 3 && a < 8)
                {
                    Instantiate(newFloor[2], new Vector3(player.transform.position.x + 26, newPositionY + 2.0f, -0.1f), Quaternion.identity);
                    newPositionY += 2.8f;
                }
                if (a > 7 && a < 14)
                {
                    Instantiate(newFloor[3], new Vector3(player.transform.position.x + 26, newPositionY + 2.0f, -0.1f), Quaternion.identity);
                    newPositionY += 2.8f;
                }
                if (a > 13 && a < 17)
                {
                    Instantiate(newFloor[4], new Vector3(player.transform.position.x + 26, newPositionY + 1.5f, -0.1f), Quaternion.identity);
                    newPositionY += 1.5f;
                }
                if (a > 16 && a < 20)
                {
                    Instantiate(newFloor[5], new Vector3(player.transform.position.x + 26, newPositionY + 1.5f, -0.1f), Quaternion.identity);
                    newPositionY += 1.5f;
                }
                if (a > 19 && a < 26)
                {
                    Instantiate(newFloor[6], new Vector3(player.transform.position.x + 26, newPositionY + 1.5f, -0.1f), Quaternion.identity);
                    newPositionY += 1.5f;
                }
                if (a > 25 && a < 35)
                {
                    Instantiate(newFloor[7], new Vector3(player.transform.position.x + 26, newPositionY + 1.5f, -0.1f), Quaternion.identity);
                    newPositionY += 1.5f;
                }
            }
        }
    }
}
