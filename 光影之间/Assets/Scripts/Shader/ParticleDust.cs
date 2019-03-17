using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDust : MonoBehaviour {
    private ParticleSystem particleSystem;
    private PlayerMove player;
    // Use this for initialization
    void Start () {
        particleSystem = this.gameObject.GetComponent<ParticleSystem>();
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
		if(player.IsGrounded == false || player.moveSpeed == 0) 
        {
            Debug.Log("11");
            particleSystem.enableEmission = false;
        }
        if (player.IsGrounded == true && player.moveSpeed != 0) 
        {
            particleSystem.enableEmission = true;
        }
	}
}
