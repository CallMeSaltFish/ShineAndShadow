using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMonsterMove : MonoBehaviour {
    private GameObject player;
    /*计时器*/
    private float timer;
    private int num;
    /*飞行怪物状态*/
    private bool canAttack;
    /*飞行怪物速度*/
    public float moveSpeed = 0.25f;
    public float attackSpeed = 0.4f;
    public float rotateSpeed =0.1f;

    /// <summary>
    /// 枚举出飞行怪物的几种状态
    /// </summary>
    private enum MonsterState
    {
        Far,//玩家远处
        Approach,//向玩家靠近，并逐步进入玩家视野范围内
        Stand,//准备进攻
        Attack//进攻
    }
    private MonsterState state;
    void Awake()
    {
        //得在Awake里面把num置0，否则各个预制体实例里面的num可能会叠加，在start里置0没用
        num = 0;
    }
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        state = MonsterState.Far;
		//transform.position=player.transform.position+new
	}
	
	// Update is called once per frame
	void Update () {
        //通过Time.deltaTime实现简易的计时功能
        timer += Time.deltaTime;
        //Debug.Log(timer);
        //每过0.2s num++,即一个num代表该飞刀实例化后度过一个0.5s
        if (timer > 0.2)
        {
            num++;
            timer = 0;
        }
        JudgeState();
        UpdateMonsterState();
	}

    /// <summary>
    /// 更新怪兽状态，是什么状态，就调用什么方法
    /// </summary>
    void UpdateMonsterState()
    {
        //Debug.Log(num);
        switch (state)
        {
            case MonsterState.Far:Far();
                break;
            case MonsterState.Approach:Approach();
                break;
            case MonsterState.Stand:Stand();
                break;
            case MonsterState.Attack:Attack();
                break;
        }
    }

    /// <summary>
    /// 判断现在该是哪个状态，我设置的是飞刀按照我枚举的顺序切换状态，所以要到状态3必须经过状态2
    /// </summary>
    void JudgeState()
    {
        //如果度过4个0.5s并且状态是Far，就切换到Approach
        if (num == 1 && state == MonsterState.Far)
        {
            state = MonsterState.Approach;
        }
        if (num == 2 && state == MonsterState.Approach)
        {
            state = MonsterState.Stand;
        }
        if (num == 6 && state == MonsterState.Stand)
        {
            state = MonsterState.Attack;
        }
        if (num == 8 && state == MonsterState.Attack)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Far状态
    /// </summary>
    void Far()
    {
        transform.position =new Vector3(player.transform.position.x+12,transform.position.y,0);
    }
    
    /// <summary>
    /// Approach状态
    /// </summary>
    void Approach()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + 9, transform.position.y, 0), moveSpeed);
    }

    /// <summary>
    /// Stand状态
    /// </summary>
    void Stand()
    {
        //transform.position = new Vector3(player.transform.position.x + 7, transform.position.y, 0);
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + 9, transform.position.y, 0), moveSpeed);
        /**/
        //if(transform.localRotation.z<360)
        //    transform.Rotate(Vector3.back/*Time.deltaTime*/ ,12);//后面这个参数越大，转的越快

        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), rotateSpeed);
    }

    /// <summary>
    /// Attack状态
    /// </summary>
    void Attack()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x - 15, transform.position.y, 0), attackSpeed);
    }

}
