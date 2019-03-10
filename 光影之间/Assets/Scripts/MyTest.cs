using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTest : MonoBehaviour {

    Rigidbody2D rb;
    [Range(0,10)]
    public float moveSpeed;
    [Range(5, 15)]
    public float force;
    Transform playerTransform;
    BoxCollider2D playerBox;
    float std_y;
    float std_y_;
    LayerMask mask;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        playerBox = GetComponent<BoxCollider2D>();
        mask = ~(1<<1);
        std_y = playerTransform.position.y;
	}

    // Update is called once per frame
    void Update () {
        float h = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(h, 0, 0) * Time.deltaTime * moveSpeed);
        RaycastHit2D hit = Physics2D.Linecast(playerTransform.position + new Vector3(0, 0.715f, 0), playerTransform.position + new Vector3(0, 1.5f, 0));
        Debug.DrawLine(playerTransform.position + new Vector3(0, 0.715f, 0), playerTransform.position + new Vector3(0, 1.5f, 0), Color.green);
        RaycastHit2D hit1 = Physics2D.Linecast(playerTransform.position + new Vector3(0, -0.715f, 0), playerTransform.position + new Vector3(0, -0.75f, 0));
        Debug.DrawLine(playerTransform.position + new Vector3(0, -0.715f, 0), playerTransform.position + new Vector3(0, -0.75f, 0), Color.blue);
        RaycastHit2D hit2 = Physics2D.Linecast(playerTransform.position + new Vector3(0, -50, 0), playerTransform.position, mask.value);
        //Debug.Log(hit2.transform.name);
        Debug.Log(hit.collider.GetType().ToString());
        //上跳
        if(Input.GetMouseButtonDown(1))
        {
            rb.AddForce(new Vector2(0, 1) * force, ForceMode2D.Impulse);
            if (hit && hit.collider.GetType().ToString() == "UnityEngine.EdgeCollider2D")
            {
                std_y = hit.transform.position.y;
                playerBox.enabled = false;
                //Debug.Log(1);
            }

        }
        if (playerTransform.position.y - std_y > GetComponent<BoxCollider2D>().size.y * Mathf.Abs(transform.localScale.y) * 0.5f)
        {
            playerBox.enabled = true;
            std_y = playerTransform.position.y;
           // Debug.Log(2);
            //Debug.Log(GetComponent<BoxCollider2D>().size.y * Mathf.Abs(transform.localScale.y) * 0.5f);
        }

        ////下穿
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    if (hit1 && hit1.collider.GetType().ToString() == "UnityEngine.EdgeCollider2D")
        //    {
        //        std_y_ = hit1.transform.position.y;
        //        playerBox.enabled = false;
        //        Debug.Log(2);
        //    }
        //}
        //if (std_y_ - playerTransform.position.y > 0.713)
        //{
        //    playerBox.enabled = true;
        //    std_y_ = playerTransform.position.y;
        //}

    }

}
