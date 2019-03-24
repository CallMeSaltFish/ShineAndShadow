using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    /*计时器*/
    private float timer;
    private int num;

    /*实例化的地面*/
    public GameObject newFloor;
    /*实例化的次数*/
    private int number = 0;

    private GameObject player;
    private MapManager mapManager;

    // Start is called before the first frame update
    void Start()
    {
        mapManager = GameObject.Find("Manager").GetComponent<MapManager>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
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
        if(mapManager.chapter==3)
        Instantiate(newFloor, new Vector3(player.transform.position.x + 28, 0 + number * 2.6f, 0), Quaternion.identity);
    }
}
