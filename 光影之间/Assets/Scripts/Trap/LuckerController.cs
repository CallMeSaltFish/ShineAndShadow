using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckerController : MonoBehaviour {
    /*地刺触发计时器*/
    private float timer;
    private static int num;
    /*地刺运动速度*/
    public float luckerSpeed=1.0f;
    /*地刺状态*/
    private bool canUp = false;
    /*地刺终点*/
    public GameObject endPosition;

    private Vector3 originPosition;
	// Use this for initialization
	void Start () {
        originPosition = transform.position;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        //Debug.Log(timer);
        if (timer>2)
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
        if (canUp==true)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPosition.transform.position, luckerSpeed*0.2f);
            if (Vector3.Distance(gameObject.transform.position, endPosition.transform.position) < 0.00002f)
            {
                canUp = false;
            }
            //timer = 0;
            //Debug.Log(1);
        }
        if (canUp==false)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, /*endPosition.transform.position - new Vector3(0,
                GetComponent<BoxCollider2D>().size.y * transform.localScale.y, 0)*/originPosition, luckerSpeed*0.2f);
            //timer = 0;
            //Debug.Log(2);
        }
	}
    //这个碰撞检测在正式场景中被其它方式取代
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("你已经被刺死了");
        }
    }
}
