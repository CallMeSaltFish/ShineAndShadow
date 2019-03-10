using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour {
    private Text scoreText;
    /*PlayerMove脚本*/
    private PlayerMove playerMove;
	// Use this for initialization
	void Start () {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        scoreText.text = "得分：0";
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "得分："+(playerMove.Scores).ToString();
	}
}
