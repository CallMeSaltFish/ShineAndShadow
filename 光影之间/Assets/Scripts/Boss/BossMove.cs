using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour {
    /*Boss移动的速度*/
    [Header("Boss移动速度")]
    [SerializeField]
    private float moveSpeed;
    /*判断主角是否被Boss杀死*/
    [SerializeField]
    [Header("是否被杀死")]
    private bool isEat;

    private Rigidbody2D playerRigidbody;
    private GameObject bossB;
    private GameObject bossW;

    private Transform bossTransform;
    private float std;

	// Use this for initialization
	void Start () {
        playerRigidbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        //bossB = GameObject.Find("BossB");
        //bossW = GameObject.Find("BossW");
        bossTransform = this.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        //if(bossTransform.position.y > 0)
        //{
        //    std = 1;
        //}else
        //{
        //    std = -1;
        //}
        //RaycastHit2D ray = Physics2D.Linecast(bossTransform.position + new Vector3(1.5f, -2f * std, 0), bossTransform.position + new Vector3(7.5f, -2 * std, 0));
        //Debug.DrawLine(bossTransform.position + new Vector3(1.5f, -2f * std, 0), bossTransform.position + new Vector3(7.5f, -2 * std, 0), Color.red);

        //if()
        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * moveSpeed);

        if (playerRigidbody.gravityScale > 0)
        {
            if(bossTransform.name == "BossB")
            {
                moveSpeed = Mathf.Lerp(3 , 4.5f, 0.4f);
            }
            else if(bossTransform.name == "BossW")
            {
                moveSpeed = Mathf.Lerp(moveSpeed, 1.5f, 0.4f);
            }
        }
        else if(playerRigidbody.gravityScale < 0)
        {
            if (bossTransform.name == "BossB")
            {
                moveSpeed = Mathf.Lerp(moveSpeed, 1.5f, 0.4f);
            }
            else if (bossTransform.name == "BossW")
            {
                moveSpeed = Mathf.Lerp(3, 4.5f, 0.4f);
            }
        }

        if(Mathf.Abs(bossTransform.position.x - playerRigidbody.transform.position.x) >= 7)
        {
            moveSpeed = 3f;
        }
    }
    /*isEat的属性 供外界调用*/
    public bool IsEat
    {
        get
        {
            return isEat;
        }
        set
        {
            isEat = value;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isEat = true;
        }
    }
}
