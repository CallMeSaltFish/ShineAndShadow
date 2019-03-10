using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWithPlayer : MonoBehaviour {

    [SerializeField]
    private Transform playerTransform;
    [Range(0,1)]
    public float cameraSpeed;
    private Vector3 cameraVelocity = Vector3.zero;
    private Vector3 offset;
    private Vector3 endPosition;
    private PlayerMove playerMove;
    private MapManager mapManager;

    public bool isStd;

	// Use this for initialization
	void Start () {
        offset = transform.position - playerTransform.position;
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        mapManager = GameObject.Find("Manager").GetComponent<MapManager>();
    }

    // Update is called once per frame
    void Update () {
        endPosition = playerTransform.position + offset;
        if(mapManager.chapter == 2)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(endPosition.x,playerTransform.position.y,transform.position.z), ref cameraVelocity, cameraSpeed);
            if(transform.position.y > 7.2)
            {
                transform.position = new Vector3(transform.position.x, 7.2f, transform.position.z);
            }
            if(transform.position.y < 0)
            {
                transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            }
        }
        else
        {
            if (!isStd)
            {
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(endPosition.x, transform.position.y, endPosition.z), ref cameraVelocity, cameraSpeed);
                //Debug.Log(2);
                if (transform.position.x - playerTransform.position.x >= offset.x - 0.1f && Input.GetMouseButtonDown(2))
                {
                    GameObject.Find("BossB").GetComponent<BossMove>().enabled = true;
                    GameObject.Find("BossW").GetComponent<BossMove>().enabled = true;
                    playerMove.enabled = true;
                    isStd = true;
                    //Debug.Log(3);
                }
            }
            if (isStd)
            {
                transform.position = new Vector3(endPosition.x, transform.position.y, endPosition.z);
                //Debug.Log(1);
            }
        }
    }
}
