using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGround : MonoBehaviour {
    private float angle = 0;
    private GameObject[] backGrounds;
	// Update is called once per frame
	void Update () {
        backGrounds = GameObject.FindGameObjectsWithTag("BackGround");
        for(int i = 0;i< backGrounds.Length; i++)
        {
            backGrounds[i].GetComponent<Transform>().rotation = Quaternion.Euler(angle + 180, 0, 0);
        }
	}
}
