using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipScreen : MonoBehaviour {
    private GameObject player;
    public GameObject tipPanel;
    private BoxCollider2D box;
    private bool isReading=false;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        box = GetComponent<BoxCollider2D>();
        //tipPanel = GameObject.Find("Canvas/TipPanel");
	}
	
	// Update is called once per frame
	void Update () {
        if (isReading == true && Input.GetKeyDown(KeyCode.Space))
        {
            //Time.timeScale = 1;
            box.enabled = false;
            player.GetComponent<AfterImageEffects>().enabled = true;
            player.GetComponent<RotatePlayer>().enabled = true;
            player.GetComponent<PlayerMove>().enabled = true;
            player.GetComponent<PlayerMove>().isStop = false;
            tipPanel.SetActive(false);
            GameObject.FindWithTag("Player").SendMessage("ChangeIsInteractable");
        }
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            player.GetComponent<AfterImageEffects>().enabled = false;
            player.GetComponent<RotatePlayer>().enabled = false;
            player.GetComponent<PlayerMove>().enabled = false;
            tipPanel.SetActive(true);
            isReading = true;
        }
    }
}
