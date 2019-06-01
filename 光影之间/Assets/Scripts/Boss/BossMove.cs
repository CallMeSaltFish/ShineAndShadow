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

    /*判断Boss本身的血量*/
    [SerializeField]
    [Header("Boss是否被杀死")]
    public int bossBlood = 300;

    private Rigidbody2D playerRigidbody;
    private MapManager mapManager;

    private Transform bossTransform;
    private float std;
    //private bool isChange;
	// Use this for initialization
	void Start () {
        mapManager = GameObject.FindWithTag("GameController").GetComponent<MapManager>();
        playerRigidbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        bossTransform = this.GetComponent<Transform>();
        //isChange = false;
    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * moveSpeed);
        // if(isChange)
        // {
        //     BossHurt1();
        //     isChange = false;
        // }
        /*之前设定的在某半部分待的时间越久，Boss的移动速度就会越来越快*/
        //if (playerRigidbody.gravityScale > 0)
        //{
        //    if(bossTransform.name == "BossB")
        //    {
        //        moveSpeed = Mathf.Lerp(3 , 4.5f, 0.4f);
        //    }
        //    else if(bossTransform.name == "BossW")
        //    {
        //        moveSpeed = Mathf.Lerp(moveSpeed, 1.5f, 0.4f);
        //    }
        //}
        //if(playerRigidbody.gravityScale < 0)
        //{
        //    if (bossTransform.name == "BossB")
        //    {
        //        moveSpeed = Mathf.Lerp(moveSpeed, 1.5f, 0.4f);
        //    }
        //    else if (bossTransform.name == "BossW")
        //    {
        //        moveSpeed = Mathf.Lerp(3, 4.5f, 0.4f);
        //    }
        //}
        //if(Mathf.Abs(bossTransform.position.x - playerRigidbody.transform.position.x) >= 7)
        //{
        //    moveSpeed = 3f;
        //}

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
        if (col.tag == "Player")
        {
            isEat = true;
        }
        if (col.tag == "FlyMonster" && mapManager.chapter == 4) 
        {
            Debug.Log("Boss扣血了");
            StopCoroutine("BossHurt");
            StartCoroutine("BossHurt");
            //isChange = true;
            bossBlood -= 30;
        }
    }
    void BossHurt1()
    {
        SpriteRenderer sp = this.GetComponent<SpriteRenderer>();
        Color color = sp.color;
        float[] hp = { 0.5f, 1 };
        for (int i = 0; i < hp.Length; i++)
        {
            color.a = hp[i];
            while (sp.color.a >= hp[i])
            {
                sp.color = Color.Lerp(sp.color, color, 0.1f);
            }
            Debug.Log(1);
        }
    }
    
    IEnumerator BossHurt()
    {
        SpriteRenderer sp = this.GetComponent<SpriteRenderer>();
        Color color = sp.color;
        for(float i = 1f;i >= 0.5f;i -= 0.01f)
        {
           color.a = i;
           sp.color = color;
           yield return 0;
        }
        for (float i = 0.5f; i <= 1f; i += 0.01f)
        {
           color.a = i;
           sp.color = color;
           yield return 0;
        }
        // float[] hp = { 0.5f, 1 };
        // for(int i = 0;i < hp.Length; i++)
        // {
        //     color.a = hp[i];
        //     while(sp.color.a != hp[i])
        //     {
        //         sp.color = Color.Lerp(sp.color, color, 0.1f);
        //         yield return null;
        //     }
        //     Debug.Log(1);
        // }
    }
}
