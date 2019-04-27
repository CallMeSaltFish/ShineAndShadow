using UnityEngine;
using System.Collections;
 
//淡入淡出跳转场景
public class SceneLoad : MonoBehaviour
{
    const float a = 0.1f;
    const float b = 11.0f;
    public int startChapter;
    //载入图的绘制深度
    public int guiDepth = 0;
    //要加载的场景名
    public string levelToLoad;
    //载入界面图片
    public Texture2D[] splashLogo;
    //淡入淡出速度
    public float fadeSpeed = 0.8f;
    //等待时间
    public float waitTime = 0.2f;
    //是否等待任意操作跳转
    public bool waitForInput = false;
    //是否自动跳转
    public bool startAutomatically = true;
    //淡出的停留时间
    private float timeFadingInFinished = 0.0f;
    //淡出方式(分为先加载后淡出和先淡出后加载两种)
    public enum SplashType
    {
        LoadNextLevelThenFadeOut,
        FadeOutThenLoadNextLevel
    }
    public SplashType splashType;
    //透明度
    private float alpha = 0.0f;
    //淡入淡出的状态
    private enum FadeStatus
    {
        Paused,
        FadeIn,
        FadeWaiting,
        FadeOut
    }
    private FadeStatus status = FadeStatus.FadeIn;
    //摄像机
    private Camera oldCam;
    private GameObject oldCamGO;
    //载入图绘制范围
    private Rect splashLogoPos = new Rect();
    //private Rect splashLogoPos1 = new Rect();
    //载入图位置
    public enum LogoPositioning
    {
        Centered,
        Stretched
    }
    public LogoPositioning logoPositioning;
    //是否绘制下个场景
    private bool loadingNextLevel = false;

    void Start()
    {

        //是否自动淡入淡出
        if (startAutomatically)
        {
            status = FadeStatus.FadeIn;
        }
        else
        {
            status = FadeStatus.Paused;
        }
        //指定摄像机
        oldCam = Camera.main;
        oldCamGO = Camera.main.gameObject;
        //载入图位置大小判断
        if (logoPositioning == LogoPositioning.Centered)
        {
            for (int i = 0; i < 8; i++) { 
                splashLogoPos.x = (Screen.width * 0.5f) - (splashLogo[i].width * 0.5f) + 10 *b;
                splashLogoPos.y = (Screen.height * 0.5f) - (splashLogo[i].height * 0.5f) + 80 * b;

                splashLogoPos.width = splashLogo[i].width * a;
                splashLogoPos.height = splashLogo[i].height * a;

            }
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                splashLogoPos.x = (Screen.width * 0.5f) - (splashLogo[i].width * 0.5f);
                splashLogoPos.y = (Screen.height * 0.5f) - (splashLogo[i].height * 0.5f);

                splashLogoPos.width = splashLogo[i].width;
                splashLogoPos.height = splashLogo[i].height;

            }
        }


        if (splashType == SplashType.LoadNextLevelThenFadeOut)
        {
            DontDestroyOnLoad(this);
            //DontDestroyOnLoad(Camera.main);
        }
        GameObject[] goes = GameObject.FindGameObjectsWithTag("SceneSwitchManager");
        if(goes.Length == 2)
        {
            DestroyImmediate(goes[0]);
        }
        PlayerPrefs.DeleteKey("Chapter");
        //判断待加载场景是否为空
        if ((Application.levelCount <= 1) || (levelToLoad == ""))
        {
            Debug.LogWarning("Invalid levelToLoad value.");
        }
    }

    //外部调用接口执行淡入淡出转场景
    public void StartSplash(int i)
    {
        //splashLogo.height = 1000;
        //splashLogo.width = 1000;
        status = FadeStatus.FadeIn;
        startChapter = i;
        Debug.Log("更换新场景");
    }

    void Update()
    {
        //状态机判断
        switch (status)
        {
            case FadeStatus.FadeIn:
                alpha += fadeSpeed * Time.deltaTime;
                break;
            case FadeStatus.FadeWaiting:
                if ((!waitForInput && Time.time >= timeFadingInFinished + waitTime) || (waitForInput && Input.anyKey))
                {
                    status = FadeStatus.FadeOut;
                }
                break;
            case FadeStatus.FadeOut:
                alpha += -fadeSpeed * 3 * Time.deltaTime;
                break;
        }
    }

    void OnGUI()
    {
        //图片Alpha控制
        GUI.depth = guiDepth;
        if (splashLogo[0] != null)
        {
            //if (alpha > (0 + 1) * 0.15f)
                //splashLogo[i + 1] = null;
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, Mathf.Clamp01(alpha));
            GUI.DrawTexture(splashLogoPos, splashLogo[0]);
        }
        for (int i = 0; i < 8; i++)
        {
            if (splashLogo[i] != null)
            { 
                GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, Mathf.Clamp01(alpha));
                GUI.DrawTexture(splashLogoPos, splashLogo[i]);
            }
            if (alpha > (i + 1) * 0.14f)
                splashLogo[7 - i] = null;
        }
        
        if (alpha > 1.0f)
        {
            status = FadeStatus.FadeWaiting;
            timeFadingInFinished = Time.time;
            alpha = 1.0f;
            if (splashType == SplashType.LoadNextLevelThenFadeOut)
            {
                oldCam.depth = -1000;
                loadingNextLevel = true;
                if ((Application.levelCount) >= 1 && (levelToLoad != ""))
                {
                    Application.LoadLevel(levelToLoad);
                    //
                    //MapManager mm = GameObject.FindWithTag("GameController").GetComponent<MapManager>();
                    

                }
            }
        }
        if (alpha < 0.0f)
        {
            if (splashType == SplashType.FadeOutThenLoadNextLevel)
            {
                if ((Application.levelCount >= 1) || (levelToLoad != ""))
                {
                    //Application.LoadLevel(levelToLoad);
                    Debug.Log(1);
 
                }
            }
            else
            {
                Destroy(oldCamGO);
                Destroy(oldCam);
            }
        }
    }

    //场景加载完毕后销毁摄像机和摄像机物体
    void OnLevelWasLoaded(int lvlIdx)
    {
        if (loadingNextLevel)
        {
            if (alpha == 0.0f)
            {
                Destroy(oldCam);
                Destroy(oldCamGO);
            }
        }
    }

    //绘制Gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }
}