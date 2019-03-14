using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossFloor : MonoBehaviour {
    private bool canDown=false;
	
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            canDown = true;
        }
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Respawn")
        {
           //Debug.Log("进入碰撞区域");
           col.GetComponent<EdgeCollider2D>().enabled = true;  
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Respawn")
        {
            if (canDown == true)
            {
                col.GetComponent<EdgeCollider2D>().enabled = false;
                canDown = false;
            }
        }
    }
}
