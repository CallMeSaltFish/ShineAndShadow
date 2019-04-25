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
    [HideInInspector]
    public Vector3 lastHit3 = Vector3.zero;
    /*遇到传送门后减速的速度*/
    public float speed;
    /*跳跃的力量*/
    [Header("跳跃高度")]
    public float jumpSpeed;
    /*单个加分道具分数*/
    //private int singleScore = 5;

    /*落至地面的音乐*/
    public int groundMusic = 0;
    /*吃食物的音乐*/
    public int foodMusic = 0;
    /*吃拼图的音乐*/
    public int jigMusic = 0;


    /*吃到了攻击怪物道具*/
    private bool eatJig = false;
    /*已收集拼图个数*/
    [HideInInspector]
    public int jigNum = 0;
    /*吃加分道具后所得总分数*/
    [HideInInspector]
    public int scores = 0;
    public GameObject explosion;
    /*人物从高处落到地面的粒子特效*/
    public GameObject jumpFallexplosion;
    /*是否允许生成人物从高处落到地面的粒子特效*/
    private bool canMakeJumpFall = false;
    /*高处落下的粒子系统*/
    private ParticleSystem jumpFallPS;

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

    /*飞刀怪*/
    public GameObject flyMonster;
    /*是否启用抛物线运动脚本*/
    //private bool flyCruveState;

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
    private GameObject myBackParticle;
    /*上坡坡度*/
    public float angle1 = 0;
    /*下坡坡度*/
    public float angle2 = 0;

    private int[] randArray = new int[] { -2,-1,0,1,2,3,4,5 };
    private int i = 2;
    /*记录是当前帧的刚体*/
    private float localVelocity;

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
        //flyCruveState = false;
        jumpFallPS = jumpFallexplosion.GetComponentInChildren<ParticleSystem>();
        myBackParticle = GameObject.Find("Player/Particle System");
        spriteRenderer = flyMonsterMove.GetComponent<SpriteRenderer>();
        blackKnife = (Texture2D)Resources.Load("Sprites/障碍-飞刀");
        whiteKnife = (Texture2D)Resources.Load("Sprites/障碍-飞刀0");
        rb = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        mapManager = GameObject.Find("Manager").GetComponent<MapManager>();
        followWith = GameObject.Find("MainCamera").GetComponent<FollowWithPlayer>();
        rotatePlayer = GetComponent<RotatePlayer>();
        mapManager.chapterPortalTimes = 0;
        //transform.position = savePos;
    }
    // Update is called once per frame
    void Update() {
        if (rb.velocity.y != 0)
        {
            localVelocity = Mathf.Abs(2.0f * rb.velocity.y);
        }
        if (!IsGrounded)
        {
            canMakeJumpFall = true;
        }
        if (IsGrounded&&canMakeJumpFall)
        {
            groundMusic++;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            jumpFallPS.startSpeed = 0.1f*localVelocity;
            if (rotatePlayer.playerHeight > 0)
            {
                jumpFallPS.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (rotatePlayer.playerHeight < 0)
            {
                jumpFallPS.transform.rotation = Quaternion.Euler(180, 0, 0);
            }
            GameObject jumpfallexplosion = Instantiate(jumpFallexplosion, transform.position-new Vector3(-0.5f,rb.gravityScale*0.35f,0.0f), Quaternion.identity);//位置要改
            canMakeJumpFall = false;
            Destroy(jumpfallexplosion, 1.0f);
        }
         
        //Debug.Log(rb.velocity.y);
        //Debug.Log(flyMonster.GetComponent<FlyMonsterMoveCruve>().enabled);

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
                //if (flyCruveState)
                //{
                    Debug.Log("启用抛物线");
                    //flyMonster.GetComponent<FlyMonsterMoveCruve>().enabled = true;
                //Debug.Log(flyMonster.GetComponent<FlyMonsterMoveCruve>().enabled);
                    //flyCruveState = false;
                //}
                //Debug.Log(transform.position.y - 0.5f + 0.6f * randArray[i]);
                GameObject a = Instantiate(flyMonster, new Vector3(transform.position.x + 14, transform.position.y-1.0f + 0.6f * randArray[i], 0), Quaternion.identity);
                //a.GetComponent<FlyMonsterMoveCruve>().enabled = true;
                
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
            if (mapManager.chapter == 3 || mapManager.chapter == 4) 
            {
                GameObject a = Instantiate(flyMonster, new Vector3(transform.position.x + 12,
                gameObject.transform.position.y-0.6f + 0.6f * randArray[i], 0), Quaternion.identity);
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
        //Debug.DrawLine(transform.position + new Vector3(0.16f, 0, 0), transform.position + new Vector3(100.16f, 0, 0), Color.red);
        //检测斜坡
        //向前 短下
        RaycastHit2D hit1 = Physics2D.Linecast(transform.position + new Vector3(0.16f, -0.5f * rb.gravityScale, 0), transform.position + new Vector3(0.31f, -0.5f * rb.gravityScale, 0));
        //Debug.DrawLine(transform.position + new Vector3(0.16f, -0.5f * rb.gravityScale, 0), transform.position + new Vector3(0.31f, -0.5f * rb.gravityScale, 0), Color.green);
        //向前 长上
        RaycastHit2D hit2 = Physics2D.Linecast(transform.position + new Vector3(0.16f, -0.4f * rb.gravityScale, 0), transform.position + new Vector3(0.56f, -0.4f * rb.gravityScale, 0));
        //Debug.DrawLine(transform.position + new Vector3(0.16f, -0.4f * rb.gravityScale, 0), transform.position + new Vector3(0.56f, -0.4f * rb.gravityScale, 0), Color.blue);
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
        //遇到传送门或者指示板
        if (isStop)
        {
            transform.position = Vector3.Lerp(transform.position, point, Time.deltaTime * speed);
            animator.SetBool("isStop", true);
        }
        //平地
        else
        {
            if (mapManager.chapter != 3)
            {
                animator.SetBool("isStop", false);
            }
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * moveSpeed);
            angle1 = 0;
        }
        //上坡
        if (hit2 && hit1 && isGrounded)
        {
            //上坡
            if (Mathf.Abs(hit2.point.x - hit1.point.x) > 0.01f && hit2.transform.tag == "BackGround" && hit1.transform.tag == "BackGround" && isGrounded)
            {
                angle1 = Mathf.Atan(0.1f / (hit2.point.x - hit1.point.x));
                rb.velocity = new Vector3(0, Mathf.Tan(angle1) * rb.gravityScale * moveSpeed, 0);
                animator.SetBool("isStop", false);
            }
            //碰墙
            if (Mathf.Abs(hit2.point.x - hit1.point.x) <= 0.01f && hit2.transform.tag == "BackGround" && hit1.transform.tag == "BackGround")
            {
                if(mapManager.chapter != 3)
                {
                    moveSpeed = 0;
                    Debug.Log(1);
                }
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
                    if (offset < -0.01 && offset > -0.5 && isGrounded)//下滑
                    {
                        animator.SetBool("isSlip", true);
                        angle2 = Mathf.Atan(-offset / (hit3.point.x - lastHit3.x)) * rb.gravityScale;
                        Debug.Log("下坡");
                        rb.velocity = new Vector3(0, Mathf.Tan(angle2) * rb.gravityScale * moveSpeed, 0) * (-1);
                        angle1 = -angle2;
                    }
                    else
                    {
                        animator.SetBool("isSlip", false);
                    }
                    if (offset < -0.4f && isGrounded)//下落
                    {
                        animator.SetBool("isDrop", true);
                        isGrounded = false;
                    }
                }
                if (rb.gravityScale < 0)
                {
                    if (offset > 0.01 && offset < 1 && isGrounded)//下滑
                    {
                        animator.SetBool("isSlip", true);
                        angle1 = Mathf.Atan(-offset / (hit3.point.x - lastHit3.x)) * rb.gravityScale;
                        rb.velocity = new Vector3(0, Mathf.Tan(angle1) * rb.gravityScale * moveSpeed, 0) * (-1);
                        angle1 = -angle2;
                    }
                    else
                    {
                        animator.SetBool("isSlip", false);
                    }
                    if (offset > 0.4f && isGrounded)//下落
                    {
                        animator.SetBool("isDrop", true);
                        isGrounded = false;
                    }
                }
                lastHit3 = hit3.point;
            }
        }

        /*控制摄像机移动的脚本*/
        if (Camera.main.transform.position.x - transform.position.x <= 5.3f && Camera.main.transform.position.x - transform.position.x >= 5.2f)
        {
            followWith.enabled = true;
            followWith.isStd = true;
        }
        if (hit_ && hit_.transform.tag == "Portal")
        {

            if ((mapManager.chapter == 0 && hit_.transform.position.x - transform.position.x < 12.66f) ||
                (mapManager.chapter == 1 && hit_.transform.position.x - transform.position.x < 13.46f) ||
                (mapManager.chapter == 2 && hit_.transform.position.x - transform.position.x < 12.66f))
            {
                followWith.enabled = false;
            }
        }

        /*主角跳*/
        if (isGrounded && Input.GetMouseButtonDown(1) && !isInteractable)
        {
            animator.SetBool("isGrounded", false);
            if(mapManager.chapter == 3)
                animator.SetBool("isStop", false);
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(0, jumpSpeed * rb.gravityScale, 0), ForceMode2D.Impulse);
            isGrounded = false;
        }
        if (!isGrounded && Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(0, jumpSpeed * -rb.gravityScale, 0), ForceMode2D.Impulse);
        }

        myBackParticle.transform.rotation = Quaternion.Euler(Mathf.Tan(angle1) * Mathf.Rad2Deg, -90, 0);
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "BackGround" || col.transform.tag == "Respawn")
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
            animator.SetBool("isDrop", false);
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        //传送门
        if (col.tag == "Portal")
        {
            GameObject.Find("BossB").GetComponent<BossMove>().enabled = false;
            GameObject.Find("BossW").GetComponent<BossMove>().enabled = false;
            moveSpeed = 0;
            transform.GetComponent<PlayerMove>().enabled = false;
            isStop = false;
            ChangeIsInteractable();
            //删除传送门
            col.transform.position += new Vector3(0f, 100f, 0f);
            switch (mapManager.chapter)
            {
                case 0:
                    anim.Play("EndGame Animation");
                    break;
                case 1:
                    switch (mapManager.chapterPortalTimes)
                    {
                        case 1:
                            anim.Play("EndGame Animation");
                            mapManager.chapterPortalTimes = 0;
                            break;
                        case 0:
                            GoIntoInternal(new Vector3(transform.position.x, -0.605f, -2f));
                            mapManager.chapterPortalTimes = 1;
                            break;
                    }
                    break;
                case 2:
                    switch(mapManager.chapterPortalTimes)
                    {
                        case 1:
                            anim.Play("EndGame Animation");
                            mapManager.chapterPortalTimes = 0;
                            break;
                        case 0:
                            if (isTrigger)
                            {
                                mapManager.InstantiateSecondSubMap(0);
                                GoIntoInternal(new Vector3(39.3f, -1.88794f, -2f));
                                followWith.isSpecial = true;
                            }
                            else
                            {
                                mapManager.InstantiateSecondSubMap(1);
                                GoIntoInternal(new Vector3(39.9f, 1.02342f, -2f));
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
            col.gameObject.GetComponent<TrailRenderer>().enabled = true;
            scores++;
            foodMusic ++;
        }
        //拼图
        if (col.tag == "Jig")
        {
            jigNum++;
            jigMusic++;
            eatJig = true;
        }
        //飞刀和障碍
        if (col.tag == "Trap" || col.tag=="DownTrap")
        {
            IsDead = true;
            PlayerPrefs.SetInt("Chapter", mapManager.chapter);
        }
        //飞刀和障碍
        if (col.tag == "FlyMonster" && mapManager.chapter != 4) 
        {
            IsDead = true;
            PlayerPrefs.SetInt("Chapter", mapManager.chapter);
        }
        if (mapManager.chapter != 4) { 
            if (col.tag == "FlyMonster1")
            {
                flyMonster.GetComponent<FlyMonsterMoveCruve>().enabled = false;
                flyMonsterMove.attackMode = 0;
                //i = Random.Range(0, 3);
                i = 2;
                flyNum = 1;
                canFly = true;
                eatJig = false;
            }
            if (col.tag == "FlyMonster3")
            {
                flyMonster.GetComponent<FlyMonsterMoveCruve>().enabled = false;
                flyMonsterMove.attackMode = 0;
                //i = Random.Range(0, 3);
                i = 2;
                flyNum = 3;
                canFly = true;
                eatJig = false;
            }
            if (col.tag == "FlyMonster5")
            {
                flyMonster.GetComponent<FlyMonsterMoveCruve>().enabled = false;
                flyMonsterMove.attackMode = 0;
                //i = Random.Range(0, 3);
                i = 2;
                flyNum = 5;
                canFly = true;
                eatJig = false;
            }
        }
        if (mapManager.chapter == 4) { 
            if (col.tag == "FlyMonster1" && eatJig) 
            {
                flyMonster.GetComponent<FlyMonsterMoveCruve>().enabled = false;
                flyMonsterMove.attackMode = 0;
                //i = Random.Range(0, 3);
                i = 2;
                flyNum = 1;
                canFly = true;
                eatJig = false;
            }
            if (col.tag == "FlyMonster3" && eatJig) 
            {
                flyMonster.GetComponent<FlyMonsterMoveCruve>().enabled = false;
                flyMonsterMove.attackMode = 0;
                //i = Random.Range(0, 3);
                i = 2;
                flyNum = 3;
                canFly = true;
                eatJig = false;
            }
            if (col.tag == "FlyMonster5" && eatJig)
            {
                flyMonster.GetComponent<FlyMonsterMoveCruve>().enabled = false;
                flyMonsterMove.attackMode = 0;
                //i = Random.Range(0, 3);
                i = 2;
                flyNum = 5;
                canFly = true;
                eatJig = false;
            }
        }
        if (col.tag == "FlyMonsterStay")
        {
            flyMonster.GetComponent<FlyMonsterMoveCruve>().enabled = false;
            flyMonsterMove.attackMode = 1;
            //i = Random.Range(0, 3);
            i = 2;
            flyNum = 1;
            canFly = true;
        }
        if (col.tag == "FlyMonsterCruve")
        {
            Debug.Log("碰到曲线");
            //flyCruveState = true;
            flyMonster.GetComponent<FlyMonsterMoveCruve>().enabled = true;
            flyMonsterMove.attackMode = -1;
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
        if (rb.gravityScale < 0)
        {
            transform.Rotate(new Vector3(180, 0, 0));
            rb.gravityScale *= -1;
        }
        else
        {
            rotatePlayer.playerHeight = -rotatePlayer.playerHeight;
            animator.SetFloat("playerHeight", rotatePlayer.playerHeight);

        }
    }

    //供TipScreen调用
    void ChangeIsInteractable()
    {
        isInteractable = false;
    }
}