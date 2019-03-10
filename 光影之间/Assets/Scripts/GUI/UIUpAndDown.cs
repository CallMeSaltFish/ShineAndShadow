using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpAndDown : MonoBehaviour {
    /*通关UI动画*/
    public GameObject passPanel;
    private MapManager mapManager;
	// Use this for initialization
	void Start () {
        //passPanel = GameObject.Find("Canvas/PassPanel");
        mapManager = GameObject.Find("Manager").GetComponent<MapManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (passPanel != null && mapManager.chapter != 0 /*&& mapManager.chapter != 1*/)
        {
            Debug.Log("到下一关了");
            passPanel.GetComponent<Animator>().Play("EndGame Animation 0");
            //Destroy(passPanel,3);
        }
	}
}
