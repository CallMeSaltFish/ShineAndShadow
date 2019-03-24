using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    /*判断物体是否接触地面*/
    private bool isGrounded = true;
    /*物体运动的动画曲线*/
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    public float moveSpeed;
    private Rigidbody2D rb;
    private Animator animator;
    /*脚本*/
    private MapManager mapManager;
    private FollowWithPlayer followWith;
    private RotatePlayer rotatePlayer;
    [HideInInspector]
    public bool isStop = false;
    private Vector3 point;
    private float nowSpeed;
    public Vector3 lastHit3 = Vector3.zero;
    private int jumpTimes;
    /*遇到传送门后减速的速度*/
    public float speed;
    /*跳跃的力量*/
    [Header("跳跃高度")]
    public float jumpSpeed;
    /*单个加分道具分数*/
    private int singleScore = 5;
    /*已收集拼图个数*/
    public int jigNum = 0;
    /*吃加分道具后所得总分数*/
    public int Scores = 0;
    public GameObject explosion;

    /*存档点位置*/
    private Vector3 savePos;

    /*切关UI的panel*/
    public GameObject passPanel;

    /*是否在与Tip交互*/
    private bool isInteractable = false;
    /*判断是否死亡*/
    private bool isDead;
    /*判断是否吃到符文触发*/
    private bool isTrigger;
    /*计时器*/
    private float timer;
    private static int num;

    /*飞刀怪*/
    public GameObject flyMonster;

    /*一组飞刀总个数*/
    private int flyNum = 0;
    /*飞刀累计个数*/
    private int currentFlyNum = 0;
    /*允许产生飞刀*/
    private bool canFly = false;

    public Animator anim;
    public FlyMonsterMove flyMonsterMove;
    private SpriteRenderer spriteRenderer;
    private Texture2D whiteKnife;
    private Texture2D blackKnife;

    private int[] randArray = new int[] { -2,-1,0,1,2,3,4,5 };
    private int i = 2;
    private int j = 1;
    private int k = 1;

    public bool IsGrounded
    {
        set
        {
            isGrounded = value;
        }
        get
        {
            return isGrounded;
        }
    }

    public bool IsDead
    {
        set
        {
            isDead = value;
        }
        get
        {
            return isDead;
        }
    }

    public float MoveSpeed
    { 
        set
        {
            moveSpeed = value;
        }
        get
        {
            return moveSpeed;
        }
    }
    void Awake()
    {
        savePos = new Vector3(PlayerPrefs.GetInt("playerPosition"), 0.549f, 0);
    }
    // Use this for initialization
    void Start() {
        spriteRenderer = flyMonsterMove.GetComponent<SpriteRenderer>();
        blackKnife = (Texture2D)Resources.Load("Sprites/障碍-飞刀");
        whiteKnife = (Texture2D)Resources.Load("Sprites/障碍-飞刀0");
        rb = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        mapManager = GameObject.Find("Manager").GetComponent<MapManager>();
        followWith = GameObject.Find("MainCamera").GetComponent<FollowWithPlayer>();
        rotatePlayer = GetComponent<RotatePlayer>();
        jumpTimes = 0;
        mapManager.chapterPortalTimes = 0;
        //transform.position = savePos;
    }

    // Update is called once per frame
    void Update() {
        //准备用于生成飞刀怪
        //timer += Time.deltaTime;
        ////Debug.Log(timer);
        //if (timer > 0.5)
        //{
        //    num++;
        //    timer = 0;
        //}
        //if (num == 1)
        //{
        //    GameObject a = Instantiate(flyMonster, transform.position + new Vector3(10, 0, 0), Quaternion.identity);
        //    num = 0;
        //}
        if (canFly == true && currentFlyNum < flyNum)
        {
            if (mapManager.chapter == 1)
            {
                GameObject a = Instantiate(flyMonster, new Vector3(transform.position.x + 12,
                -0.5f + 0.6f * randArray[i], 0), Quaternion.identity);
            }
            if (mapManager.chapter == 2)
            {
                GameObject a = Instantiate(flyMonster, new Vector3(transform.position.x + 12, transform.position.y +
                -1.0f + 0.6f * randArray[i], 0), Quaternion.identity);
                if (a.transform.position.y < 4.6f)
                {
                    SpriteRenderer spriteRenderer = a.GetComponent<SpriteRenderer>();
                    Sprite sprite = Sprite.Create(whiteKnife, spriteRenderer.sprite.textureRect, new Vector2(0.5f, 0.5f));
                    spriteRenderer.sprite = sprite;
                }
                if (a.transform.position.y > 4.6f|| a.transform.position.y == 4.6f) 
                {
                    SpriteRenderer spriteRenderer = a.GetComponent<SpriteRenderer>();
                    Sprite sprite = Sprite.Create(blackKnife, spriteRenderer.sprite.textureRect, new Vector2(0.5f, 0.5f));
                    spriteRenderer.sprite = sprite;
                }
            }
            currentFlyNum++;
            i++;
            //Debug.Log("currentFlyNum" + currentFlyNum);
        }
        if (currentFlyNum == flyNum)
        {
            canFly = false;
            currentFlyNum = 0;
        }

        if (moveSpeed != 0)
        {
            nowSpeed = moveSpeed;
        }
        //向前 检测传送门
        RaycastHit2D hit_ = Physics2D.Linecast(transform.position + new Vector3(0.16f, 0, 0), transform.position + new Vector3(100.66f, 0, 0), 1 << LayerMask.NameToLayer("Portal"));
        Debug.DrawLine(transform.position + new Vector3(0.16f, 0, 0), transform.position + new Vector3(100.16f, 0, 0), Color.red);
        //检测斜坡
        //向前 短下
        RaycastHit2D hit1 = Physics2D.Linecast(transform.position + new Vector3(0.16f, -0.5f * rb.gravityScale, 0), transform.position + new Vector3(0.31f, -0.5f * rb.gravityScale, 0));
        Debug.DrawLine(transform.position + new Vector3(0.16f, -0.5f * rb.gravityScale, 0), transform.position + new Vector3(0.31f, -0.5f * rb.gravityScale, 0), Color.green);
        //向前 长上
        RaycastHit2D hit2 = Physics2D.Linecast(transform.position + new Vector3(0.16f, -0.4f * rb.gravityScale, 0), transform.position + new Vector3(0.56f, -0.4f * rb.gravityScale, 0));
        Debug.DrawLine(transform.position + new Vector3(0.16f, -0.4f * rb.gravityScale, 0), transform.position + new Vector3(0.56f, -0.4f * rb.gravityScale, 0), Color.blue);
        //向下 检测下坡还是峭壁
        RaycastHit2D hit3 = Physics2D.Linecast(transform.position + new Vector3(0, -0.55f, 0) * rb.gravityScale, transform.position + new Vector3(0, -50f, 0) * rb.gravityScale);
        //Debug.DrawLine(transform.position + new Vector3(0, -0.55f, 0) * rb.gravityScale, transform.position + new Vector3(0, -50f, 0) * rb.gravityScale, Color.cyan);

        if (hit_ && (hit_.transform.tag == "Portal" || hit_.transform.tag == "Tip") && hit_.point.x - transform.position.x <= 0.64f)
        {
            isStop = true;
            isInteractable = true;
            point = hit_.point;
        }

        /*移动方式*/
        //遇到传送门
        if (isStop)
        {
            transform.position = Vector3.Lerp(transform.position, point, Time.deltaTime * speed);
            animator.SetBool("isStop", true);
        }
        //平地
        else
        {
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * moveSpeed);
            animator.SetBool("isStop", false);
        }
        //上坡
        if (hit2 && hit1)
        {
            //上坡
            if (Mathf.Abs(hit2.point.x - hit1.point.x) > 0.01f && hit2.transform.tag == "BackGround" && hit1.transform.tag == "BackGround" && isGrounded)
            {
                float angle = Mathf.Atan(0.1f / (hit2.point.x - hit1.point.x));
                rb.velocity = new Vector3(0, Mathf.Tan(angle) * rb.gravityScale * moveSpeed, 0);
                animator.SetBool("isStop", false);
            }
            //碰墙
            if (Mathf.Abs(hit2.point.x - hit1.point.x) <= 0.01f && hit2.transform.tag == "BackGround" && hit1.transform.tag == "BackGround")
            {
                moveSpeed = 0;
                animator.SetBool("isStop", true);
            }
        }
        else
        {
            moveSpeed = nowSpeed;
        }
        //下坡
        if (hit3)
        {
            if (hit3.transform.tag == "BackGround" || hit3.transform.tag == "Respawn")
            {
                float offset = hit3.point.y - lastHit3.y;
                if (rb.gravityScale > 0)
                {
                    if (offset < -0.01 && offset > -1 && isGrounded)//下滑
                    {
                        animator.SetBool("isSlip", true);
                        float angle = Mathf.Atan(-offset / (hit3.point.x - lastHit3.x)) * rb.gravityScale;
                        rb.velocity = new Vector3(0, Mathf.Tan(angle) * rb.gravityScale * moveSpeed, 0) * (-1);
                    }
                    else
                    {
                        animator.SetBool("isSlip", false);
                    }
                    if (offset < -0.4f && isGrounded)//下落
                    {
                        animator.SetBool("isDrop", true);
                        isGrounded = false;
                        jumpTimes = 1;
                    }
                }
                if (rb.gravityScale < 0)
                {
                    if (offset > 0.01 && offset < 1 && isGrounded)//下滑
                    {
                        animator.SetBool("isSlip", true);
                        float angle = Mathf.Atan(-offset / (hit3.point.x - lastHit3.x)) * rb.gravityScale;
                        rb.velocity = new Vector3(0, Mathf.Tan(angle) * rb.gravityScale * moveSpeed, 0) * (-1);
                    }
                    else
                    {
                        animator.SetBool("isSlip", false);
                    }
                    if (offset > 0.4f && isGrounded)//下落
                    {
                        animator.SetBool("isDrop", true);
                        isGrounded = false;
                        jumpTimes = 1;
                    }
                }
                lastHit3 = hit3.point;
            }
        }
        Debug.Log(lastHit3);

        /*控制摄像机移动的脚本*/
        if (Camera.main.transform.position.x - transform.position.x <= 5.3f && Camera.main.transform.position.x - transform.position.x >= 5.2f)
        {
            followWith.enabled = true;
            followWith.isStd = true;
        }
        if (hit_ && hit_.transform.tag == "Portal")
        {

            if ((mapManager.chapter == 0 && hit_.transform.position.x - transform.position.x < 11.36f) ||
                (mapManager.chapter == 1 && hit_.transform.position.x - transform.position.x < 13.46f) ||
                (mapManager.chapter == 2 && hit_.transform.position.x - transform.position.x < 12.66f))
            {
                followWith.enabled = false;
            }
        }

        /*主角跳*/   /*加一个前面有传送的时候不能跳*/
        if (Input.GetMouseButtonDown(1) && jumpTimes < 2 && !isInteractable)
        {
            animator.SetBool("isGrounded", false);
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(0, jumpSpeed * rb.gravityScale, 0), ForceMode2D.Impulse);
            isGrounded = false;
            jumpTimes++;
        }

    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "BackGround" || col.transform.tag == "Respawn")
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
            animator.SetBool("isDrop", false);
            jumpTimes = 0;
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        //传送门
        if (col.tag == "Portal")
        {
            GameObject.Find("BossB").GetComponent<BossMove>().enabled = false;
            GameObject.Find("BossW").GetComponent<BossMove>().enabled = false;
            transform.GetComponent<PlayerMove>().enabled = false;
            isStop = false;
            ChangeIsInteractable();
            //删除传送门
            Destroy(col.gameObject);
            switch(mapManager.chapter)
            {
                case 0:
                    anim.Play("EndGame Animation");
                    //lastHit3 = new Vector3(-7, -0.8f, 0);
                    break;
                case 1:
                    switch (mapManager.chapterPortalTimes)
                    {
                        case 1:
                            anim.Play("EndGame Animation");
                            //lastHit3 = new Vector3(-7, -2.4f, 0);
                            mapManager.chapterPortalTimes = 0;
                            break;
                        case 0:
                            GoIntoInternal(new Vector3(transform.position.x, transform.position.y, -2));
                            mapManager.chapterPortalTimes = 1;
                            break;
                    }
                    break;
                case 2:
                    switch(mapManager.chapterPortalTimes)
                    {
                        case 1:
                            anim.Play("EndGame Animation");
                            //lastHit3 = new Vector3(-7f, 0f, 0f);
                            mapManager.chapterPortalTimes = 0;
                            break;
                        case 0:
                            if (isTrigger)
                            {
                                mapManager.InstantiateSecondSubMap(0);
                                GoIntoInternal(new Vector3(39.3f, transform.position.y, -2));
                                followWith.isSpecial = true;
                            }
                            else
                            {
                                mapManager.InstantiateSecondSubMap(1);
                                GoIntoInternal(new Vector3(39.3f, 1.2f, -2));
                            }
                            mapManager.chapterPortalTimes = 1;
                            break;
                    }
                    break;
                case 3:

                    break;
            }
        }
        #region
        //碰到了加分道具
        if (col.tag == "Food")
        {
            Scores += singleScore;
        }
        //拼图
        if (col.tag == "Jig")
        {
            jigNum++;
        }
        //飞刀和障碍
        if (col.tag == "Trap" || col.tag=="DownTrap")
        {
            IsDead = true;
        }
        if (col.tag == "FlyMonster1")
        {
            flyMonsterMove.attackMode = 0;
            //i = Random.Range(0, 3);
            i = 2;
            flyNum = 1;
            canFly = true;
        }
        if (col.tag == "FlyMonster3")
        {
            flyMonsterMove.attackMode = 0;
            //i = Random.Range(0, 3);
            i = 2;
            flyNum = 3;
            canFly = true;
        }
        if (col.tag == "FlyMonster5")
        {
            flyMonsterMove.attackMode = 0;
            //i = Random.Range(0, 3);
            i = 2;
            flyNum = 5;
            canFly = true;
        }
        if (col.tag == "FlyMonsterStay")
        {
            flyMonsterMove.attackMode = 1;
            //i = Random.Range(0, 3);
            i = 2;
            flyNum = 1;
            canFly = true;
        }

        if (col.tag == "Tip")
        {
            animator.SetBool("isStop", true);
            isInteractable = true;
        }
        //符文
        if (col.name == "Props")
        {
            isTrigger = true;
            Destroy(col.gameObject);
        }
        //chapter2中的旋转人物触发
        if(col.name == "Trigger")
        {
            rotatePlayer.enabled = true;
        }
        if(col.name == "Trigger1")
        {
            rotatePlayer.enabled = false;
        }
        #endregion
    }

    //进入里场景
    void GoIntoInternal(Vector3 location)
    {
        rotatePlayer.enabled = false;
        followWith.enabled = true;
        followWith.isStd = false;
        transform.position = location;
        rotatePlayer.playerHeight = -rotatePlayer.playerHeight;
        animator.SetFloat("playerHeight", rotatePlayer.playerHeight);
        //rb.gravityScale *= -1;
        if (rb.gravityScale < 0)
        {
            transform.Rotate(new Vector3(0, 0, 180));
            rb.gravityScale *= -1;
        }
    }

    //供TipScreen调用
    void ChangeIsInteractable()
    {
        isInteractable = false;
    }
}