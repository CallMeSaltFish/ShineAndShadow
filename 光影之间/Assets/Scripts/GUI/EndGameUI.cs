using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameUI : MonoBehaviour {
    public Animator animator;
    private MapManager mapManager;
    // Use this for initialization
    void Start () {
        mapManager = GameObject.Find("Manager").GetComponent<MapManager>();
        if(mapManager.chapter == 0||(mapManager.chapter == 1 && mapManager.chapterPortalTimes == 1)||mapManager.chapter == 2)
        {
            animator.Play("EndGame Animation");
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
