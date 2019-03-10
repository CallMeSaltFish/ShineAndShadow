
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 2D人物残影特效
/// </summary>
public class AfterImageEffects : MonoBehaviour
{

    //开启残影
    public bool _OpenAfterImage;
    //残影颜色
    //public Color _AfterImageColor = Color.black;
    //残影的生存时间
    [HeaderAttribute("残影的生存时间")]
    public float _SurvivalTime = 0.3f;
    //生成残影的间隔时间
    [HeaderAttribute("生成残影的间隔时间")]
    public float _IntervalTime = 0.2f;
    private float _Time = 0;
    //残影物体
    //private List<GameObject> gameObjects;
    public GameObject playerShadow;
    private SpriteRenderer _SpriteRenderer;
    //残影物体相关属性
    private Sprite sprite;
    //player刚体
    private Rigidbody2D playRigidbody;
    //残影初始旋转
    private Quaternion shadowRotation;

    void Awake()
    {
        //gameObjects = new List<GameObject>();
        playRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        _SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (_OpenAfterImage)
        {
            if (_SpriteRenderer == null)
            {
                _OpenAfterImage = false;
                return;
            }

            _Time += Time.deltaTime;
            //生成残影
            CreateAfterImage();
        }
    }
    /// <summary>
    /// 生成残影
    /// </summary>
    void CreateAfterImage()
    {
        //生成残影
        if (_Time >= _IntervalTime)
        {
            _Time = 0;

            Sprite sprite = _SpriteRenderer.sprite;
            if (playRigidbody.gravityScale > 0)
            {
                shadowRotation = Quaternion.Euler(0, 0, 0);
            }
            if (playRigidbody.gravityScale < 0)
            {
                shadowRotation = Quaternion.Euler(180, 0, 0);
            }
            GameObject newPlayerShadow = Instantiate(playerShadow, transform.position, shadowRotation);
            newPlayerShadow.GetComponent<SpriteRenderer>().sprite = sprite;
            Destroy(newPlayerShadow, _SurvivalTime);
        }
    }
}