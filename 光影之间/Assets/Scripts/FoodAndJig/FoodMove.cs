using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMove : MonoBehaviour {
    /*食物UI图（吃掉的食物飞往这里）*/
    private GameObject foodUI;
    /*食物飞行速度*/
    public float foodSpeed = 1.0f;
    /*食物是否被吃*/
    private bool isEat=false;
	// Use this for initialization
	void Start () {
        foodUI = GameObject.Find("Canvas/FoodImage");
	}
	
	// Update is called once per frame
	void Update () {
        if (isEat == true)
        {
            //Debug.Log(Camera.main.ScreenToWorldPoint(foodUI.transform.position));
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, Camera.main.ScreenToWorldPoint(foodUI.transform.position)+new Vector3(0.3f,0,0), foodSpeed * 0.3f);
            if(Vector3.Distance(gameObject.transform.position, Camera.main.ScreenToWorldPoint(foodUI.transform.position)) < 0.45f)
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
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
