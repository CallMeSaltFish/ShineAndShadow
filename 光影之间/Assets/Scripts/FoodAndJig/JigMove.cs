using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigMove : MonoBehaviour
{
    /*拼图UI图（吃掉的拼图飞往这里）*/
    private GameObject jigUI;
    /*拼图飞行速度*/
    public float foodSpeed = 1.0f;
    /*拼图是否被吃*/
    private bool isEat = false;
    // Use this for initialization
    void Start()
    {
        jigUI = GameObject.Find("Canvas/JigImage");
    }

    // Update is called once per frame
    void Update()
    {
        if (isEat == true)
        {
            //Debug.Log(Camera.main.ScreenToWorldPoint(jig.transform.position));
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, Camera.main.ScreenToWorldPoint(jigUI.transform.position) + new Vector3(0.3f, 0, 0), foodSpeed * 0.3f);
            if (Vector3.Distance(gameObject.transform.position, Camera.main.ScreenToWorldPoint(jigUI.transform.position)) < 0.45f)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isEat = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
