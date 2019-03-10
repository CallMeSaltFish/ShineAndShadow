using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;//这个很重要
using UnityEngine.UI;

public class Jigsaw : MonoBehaviour
{
    /*四块拼图*/
    private GameObject jigsaw1;
    private GameObject jigsaw2;
    private GameObject jigsaw3;
    private GameObject jigsaw4;
    /*拼图放置的位置*/
    private GameObject button1;
    private GameObject button2;
    private GameObject button3;
    private GameObject button4;
    /*拼图个数*/
    private int starNum;

    private float jigsawSpeed = 1.0f;
    /*是否允许拼图开始运动*/
    private bool isMove = false;
    [SerializeField]
    private AnimationCurve AnimationCurve;
    // Use this for initialization
    void Start()
    {
        jigsaw1 = GameObject.Find("拼图1");
        jigsaw2 = GameObject.Find("拼图2");
        jigsaw3 = GameObject.Find("拼图3");
        jigsaw4 = GameObject.Find("拼图4");
        button1 = GameObject.Find("Canvas/Panel/Button1");
        button2 = GameObject.Find("Canvas/Panel/Button2");
        button3 = GameObject.Find("Canvas/Panel/Button3");
        button4 = GameObject.Find("Canvas/Panel/Button4");
        starNum = PlayerPrefs.GetInt("Star");
        //starNum = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (/*这里用来放判断条件*/Input.GetMouseButtonDown(0))
        {
            isMove = true;
            //Debug.Log(Camera.main.ScreenToViewportPoint(button1.transform.position));
            //Debug.Log(Camera.main.ScreenToWorldPoint(button1.transform.position));
            //Debug.Log(Camera.main.WorldToScreenPoint(button1.transform.position));
            //Debug.Log(Camera.main.WorldToViewportPoint(button1.transform.position));
        }
        if (starNum >= 1)
        {
            ButtonDown(jigsaw1, button1, 1);
        }
        if (starNum >= 2)
        {
            ButtonDown(jigsaw2, button2, 2);
        }
        if (starNum >= 3)
        {
            ButtonDown(jigsaw3, button3, 3);
        }
        if (starNum >= 4)
        {
            ButtonDown(jigsaw4, button4, 4);
        }
    }
    /// <summary>
    /// 将jigsaw移动到button处 隐藏jigsaw 更换button贴图 以实现拼图效果
    /// </summary>
    /// <param name="jigsaw"></param>
    /// <param name="button"></param>
    void ButtonDown(GameObject jigsaw, GameObject button, float num)
    {
        jigsaw.transform.position = Vector3.Lerp(jigsaw.transform.position,
            new Vector3(Camera.main.ScreenToWorldPoint(button.transform.position).x,
            Camera.main.ScreenToWorldPoint(button.transform.position).y,
            Camera.main.ScreenToWorldPoint(button.transform.position).z + 1),
            jigsawSpeed * 0.03f);
        jigsaw.gameObject.transform.localScale = Vector3.Lerp(jigsaw.gameObject.transform.localScale,
            new Vector3(0.65f, 0.65f, 0.70f),
            jigsawSpeed * 0.03f);
        jigsaw.transform.GetComponent<SpriteRenderer>().color = Color.Lerp(
            jigsaw.transform.GetComponent<SpriteRenderer>().color, Color.white,
            jigsawSpeed * 0.05f
            );
        if (Vector3.Distance(jigsaw.transform.position, Camera.main.ScreenToWorldPoint(button.transform.position)) < 1.05f)
        {
            Button button_ = button.GetComponent<Button>();
            Image image = button_.GetComponent<Image>();
            button_.interactable = false;
            if (num == 1)
                button_.image.sprite = Resources.Load("拼图【黑白】/拼图【黑白】_0", typeof(Sprite)) as Sprite;
            if (num == 2)
                button_.image.sprite = Resources.Load("拼图【黑白】/拼图【黑白】_1", typeof(Sprite)) as Sprite;
            if (num == 3)
                button_.image.sprite = Resources.Load("拼图【黑白】/拼图【黑白】_2", typeof(Sprite)) as Sprite;
            if (num == 4)
                button_.image.sprite = Resources.Load("拼图【黑白】/拼图【黑白】_3", typeof(Sprite)) as Sprite;
        }
    }
}
