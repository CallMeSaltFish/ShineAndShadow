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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
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
        Instantiate(newFloor, new Vector3(player.transform.position.x + 10, 0 + number * 1, 0), Quaternion.identity);
    }
}
