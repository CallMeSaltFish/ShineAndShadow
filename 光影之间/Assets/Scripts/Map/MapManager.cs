using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour {
    /*不同的ground数组存不同难度的地图*/  
    /*外场景的地图*/
    private Object[] groundList1;
    /*里场景的地图*/
    private Object[] groundList2;
    [SerializeField]
    private Transform playerTransform;

    private GameObject lastGround;
    private GameObject thisGround;
    private float std_x = 0f;
    private Text distaneText;
    private Text jigText;
    private Rigidbody2D rb;
    private Vector3 cameraPosition;
    //遮罩为ui的camera用来实现游戏结束后场景渐暗效果
    //private Camera UICamera;
    private Animator playerAnimator;
    /*上坡的角度*/
    private float angle = 0;
    private GameObject[] backGrounds;
    /*传送门有没有*/
    private bool hasPortal = false;
    private Vector3 portalPosition = Vector3.zero;

    public List<GameObject> ups = new List<GameObject>();
    public List<GameObject> downs = new List<GameObject>();
    public GameObject panel;
    public bool isInternal = false;
    private bool change = false;
    /*所有地图的父物体，统一放置*/
    private Transform mapTransform;
    /*PlayerMove脚本*/
    private PlayerMove playerMove;
    private RotatePlayer rotatePlayer;
    /*关卡参量*/
    public int chapter;
    public int chapterPortalTimes;
    /*防止地图重复加载*/
    private bool hasMap;
    /*通关动画状态机*/
    public Animator passPanelAnimator;
    /*死亡面板动画状态机*/
    public Animator defeatPanelAnimator;
    // Use this for initialization
    void Awake()
    {
        Time.timeScale = 0;
    }

    void Start()
    {
        playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        rotatePlayer = GameObject.FindWithTag("Player").GetComponent<RotatePlayer>();
        groundList1 = Resources.LoadAll("Background1");
        groundList2 = Resources.LoadAll("Background2");
        mapTransform = transform.Find("Map");
        //
        if(chapter == 0)
        {
            Instantiate(Resources.Load("Prefabs/TeachingMap"), new Vector3(23.6f, 0, 0), Quaternion.identity, mapTransform);
            hasMap = true;
        }
        //
        if(chapter == 1)
        {
            Instantiate(Resources.Load("Prefabs/SecondMap"), new Vector3(10.3f, -0.4f, 0), Quaternion.identity, mapTransform);
            hasMap = true;
        }
        //
        if(chapter == 2)
        {
            Instantiate(Resources.Load("Prefabs/TestMap"), new Vector3(38.5f, 3.65f, 0), Quaternion.identity, mapTransform);
            hasMap = true;
        }

        //if (chapter != 0 && chapter != 1)
        //{
        //    /*改一下来测试*/
        //    lastGround = Instantiate(groundList1[0], cameraPosition, Quaternion.identity, mapTransform) as GameObject;
        //    thisGround = Instantiate(groundList1[0], new Vector3(15.8f, 0, 0), lastGround.transform.rotation, mapTransform) as GameObject;
        //    /*初始化动态数组并取消勾选下栅栏*/
        //    UpdateListOfUp_Down(ups, downs);
        //}

        distaneText = GameObject.Find("Distance").GetComponent<Text>();
        jigText = GameObject.Find("Jig").GetComponent<Text>();
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        //UICamera = GameObject.Find("UI Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //更新拼图ui
        UpdateJigNum();
        //更新距离
        UpdateDistance();
        //lenceSwitch();
        if (Input.anyKey)
        {
            Time.timeScale = 1;
        }

        //if (groundList1 == null || groundList2 == null)
        //{
        //    return;
        //}

        if(chapter == 1 && !hasMap)
        {
            Instantiate(Resources.Load("Prefabs/SecondMap"), new Vector3(10.3f, -0.4f, 0), Quaternion.identity, mapTransform);
            hasMap = true;
        }
        if (chapter == 2 && !hasMap)
        {
            Instantiate(Resources.Load("Prefabs/TestMap"), new Vector3(38.5f, 3.65f, 0), Quaternion.identity, mapTransform);
            hasMap = true;
        }

        if (chapter != 0)
        {
            if (Input.GetMouseButtonDown(2) && Camera.main.transform.position.x - playerTransform.position.x >= 5.2f)
            {
                playerMove.enabled = true;
                //注意这一句
                //rotatePlayer.enabled = true;
                GameObject.Find("BossB").GetComponent<BossMove>().enabled = true;
                GameObject.Find("BossW").GetComponent<BossMove>().enabled = true;
            }
            //if (chapter != 1)
            //{
            //    if (thisGround == null && lastGround == null)
            //    {
            //        /*改一下来测试*/
            //        lastGround = Instantiate(groundList1[0], cameraPosition, Quaternion.identity, mapTransform) as GameObject;
            //        thisGround = Instantiate(groundList1[0], new Vector3(15.8f, 0, 0), lastGround.transform.rotation, mapTransform) as GameObject;
            //        /*初始化动态数组并取消勾选下栅栏*/
            //        UpdateListOfUp_Down(ups, downs);
            //    }
            //    /*主相机的xy坐标*/
            //    cameraPosition = Camera.main.transform.position + new Vector3(0, 0, 10);
            //    /*实例化地图*/
            //    InstantiateMap();
            //    /*里场景鼠标点击反转地图*/
            //    backGrounds = GameObject.FindGameObjectsWithTag("BackGround");
            //    if (isInternal && change)
            //    {
            //        if (Input.GetMouseButtonDown(0))
            //        {
            //            for (int i = 0; i < backGrounds.Length; i++)
            //            {
            //                backGrounds[i].GetComponent<Transform>().rotation = Quaternion.Euler(angle + 180 * playerHight, 0, 0);
            //            }
            //            angle += 180 * playerHight;
            //            playerHight = -playerHight;
            //            playerAnimator.SetFloat("playerHeight", -playerHight);
            //        }
            //    }
            //    /*换栏杆*/
            //    ChangeUp_DownHandrail();
            //    /*生成进入里场景的传送门*/
            //    InstantiatePortal();
            //    GoToInternal();
            //}
        }

        /*当作游戏结束条件 进入排行榜场景*/
        if (GameObject.FindGameObjectWithTag("Boss").GetComponent<BossMove>().IsEat == true||GameObject.Find("Player").GetComponent<PlayerMove>().IsDead==true)
        {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossMove>().IsEat = false;
            GameObject.Find("Player").GetComponent<PlayerMove>().IsDead = false;
            //停住人和怪兽
            GameObject.Find("Player").GetComponent<PlayerMove>().enabled = false;
            GameObject.Find("BossB").GetComponent<BossMove>().enabled = false;
            GameObject.Find("BossW").GetComponent<BossMove>().enabled = false;
            //屏幕逐渐变暗
            GameObject.Find("MainCamera").GetComponent<OVRScreenFade2>().enabled = true;
            //panel.SetActive(true);
            defeatPanelAnimator.Play("EndGame Animation");
            //用于做存档点
            PlayerPrefs.SetInt("playerPosition", (int)playerTransform.position.x + 7);
            //SceneManager.LoadScene("ScoresList");
        }

    }


    /*更新地图时重置动态数组的内容*/
    void UpdateListOfUp_Down(List<GameObject> ups, List<GameObject> downs)
    {
        ups.Clear();
        downs.Clear();
        GameObject[] games1 = GameObject.FindGameObjectsWithTag("UP");
        for (int i = 0; i < games1.Length; i++)
        {
            ups.Add(games1[i]);
        }
        GameObject[] games2 = GameObject.FindGameObjectsWithTag("DOWN");
        for (int i = 0; i < games2.Length; i++)
        {
            downs.Add(games2[i]);
        }
    }

    /*栏杆变化*/
    void ChangeUp_DownHandrail()
    {
        if (isInternal)
        {
            if(rotatePlayer.playerHeight > 0)
            {
                for (int i = 0; i < ups.Count; i++)
                {
                    ups[i].SetActive(true);
                    downs[i].SetActive(false);
                }

            }
            else if(rotatePlayer.playerHeight < 0)
            {
                for (int i = 0; i < ups.Count; i++)
                {
                    ups[i].SetActive(false);
                    downs[i].SetActive(true);
                }
            }
        }
        else
        {
            if (rb.gravityScale > 0)
            {
                for (int i = 0; i < ups.Count; i++)
                {
                    ups[i].SetActive(true);
                    downs[i].SetActive(false);
                }
            }
            else if (rb.gravityScale < 0)
            {
                for (int i = 0; i < ups.Count; i++)
                {
                    ups[i].SetActive(false);
                    downs[i].SetActive(true);
                }
            }
        }
    }

    /*产生随机数，作为地图编号*/
    int randomNumber(Object[] list)
    {
        return Random.Range(0,list.Length);
    }

    public void IntoRuleScene()
    {
        PlayerPrefs.SetInt("index", playerMove.Scores);
        SceneManager.LoadScene("ScoresList");
    }
    public void IntoJigsawScene()
    {
        PlayerPrefs.SetInt("Star", playerMove.jigNum);
        SceneManager.LoadScene("Achieve");
    }
    public void IntoThisScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    private void UpdateJigNum()
    {
        jigText.text = "拼图：" + (playerMove.jigNum).ToString();
    }

    private void UpdateDistance()
    {
        /*计算距离功能可有可无*/
        if ((int)playerTransform.position.x < -7)
        {
            distaneText.text = "距离：0";
        }
        else
        {
            distaneText.text = "距离：" + ((int)playerTransform.position.x + 7).ToString();
        }
    }

    private void InstantiateMap()
    {
        if (cameraPosition.x - std_x >= 15.8f)
        {
            std_x = cameraPosition.x;
            //Destroy(lastGround);
            lastGround.SetActive(false);
            lastGround = thisGround;
            if (!isInternal)
            {
                thisGround = Instantiate(groundList1[randomNumber(groundList1)], cameraPosition + new Vector3(15.8f, 0, 0), lastGround.transform.rotation, mapTransform) as GameObject;
            }
            else
            {
                thisGround = Instantiate(groundList2[randomNumber(groundList2)], cameraPosition + new Vector3(15.8f, 0, 0), lastGround.transform.rotation, mapTransform) as GameObject;
            }

            /*更新栏杆数据*/
            for (int i = 0; i < ups.Count; i++)
            {
                ups[i].SetActive(true);
            }
            for (int i = 0; i < downs.Count; i++)
            {
                downs[i].SetActive(true);
            }
            UpdateListOfUp_Down(ups, downs);
        }
    }

    /*产生传送门*/
    private void InstantiatePortal()
    {
        if (!isInternal && !hasPortal && (int)playerTransform.position.x + 7 >= 15 && thisGround.transform.name == "ground(Clone)")
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Portal") as GameObject, thisGround.transform.position + new Vector3(-1f, 0.8f, 0), Quaternion.identity);
            portalPosition = go.GetComponent<Transform>().position;
            hasPortal = true;
        }
    }

    /*进入里场景的操作*/
    private void GoToInternal()
    {
        if (playerTransform.position.x - portalPosition.x >= 8.8f && playerTransform.position.x - portalPosition.x <= 8.9f && portalPosition != Vector3.zero)
        {
            rotatePlayer.playerHeight = -rotatePlayer.playerHeight;
            playerAnimator.SetFloat("playerHeight", rotatePlayer.playerHeight);
            change = true;
        }
    }

    /*选关*/
    public void ChooseChapter(int n)
    {
        chapter = n;
    }

    /*从教学关到正常*/
    public void ChangeToNextChapter()
    {
        passPanelAnimator.Play("EndGame Animation 0");
        Destroy(GameObject.FindWithTag("Map"));
        hasMap = false;
        if (chapter < 2)
        {
            chapter += 1;
        }
        playerTransform.position = new Vector3(-7, 0.56f, 0);
        playerTransform.rotation = Quaternion.Euler(0, 0, 0);
        rb.gravityScale = 1;
        rotatePlayer.playerHeight = 1;
        playerAnimator.SetFloat("playerHeight", rotatePlayer.playerHeight);
        Camera.main.transform.position = new Vector3(0, 0, -10);
        Destroy(GameObject.Find("Tips"));
        Destroy(GameObject.Find("Foods"));
        Destroy(GameObject.Find("Jigs"));
        GameObject.Find("BossB").GetComponent<Transform>().position = new Vector3(-16.2f, 1.42f, 0);
        GameObject.Find("BossW").GetComponent<Transform>().position = new Vector3(-16.2f, -1.42f, 0);
    }
}