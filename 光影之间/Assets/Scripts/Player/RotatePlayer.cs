using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class RotatePlayer : MonoBehaviour {
    /*物体本身的高度*/
    public float playerHeight=1.0f;
    /*以供实例化的爆炸粒子特效*/
    public GameObject explosion;
    private GameObject explosions;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerMove playerMove;
    private GameObject forRotate;
    //目标正下（上）方的地面上的一点
    private Vector3 point;
    //点击左键旋转CD
    private float time = 0;

    //当前人物的颜色（用播的动画表示）
    bool whiteAnim;
    bool blackAnim;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        playerMove = this.GetComponent<PlayerMove>();
        forRotate = GameObject.Find("ForRotate");
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        whiteAnim = animator.GetCurrentAnimatorStateInfo(0).IsName("RunWhite");
        blackAnim = animator.GetCurrentAnimatorStateInfo(0).IsName("Run");
        /*用此射线检测来取旋转点*/
        RaycastHit2D hit = Physics2D.Linecast(transform.position + new Vector3(0, -0.7f, 0) * rb.gravityScale, transform.position);
        //Debug.DrawLine(transform.position + new Vector3(0, -0.7f, 0) * rb.gravityScale, transform.position, Color.yellow);
        if (hit)
        {
            if (hit.transform.tag == "BackGround")
            {
                point = hit.point;
            }
        }

        getTurnAroundGJ();

        if (Input.GetMouseButtonDown(0) && time > 1 && hit.transform.tag == "BackGround" && hit.transform.name != "第三关-1")
        {
            //Debug.Log(1);
            /*主角旋转*/
            transform.RotateAround(new Vector3(transform.position.x, point.y, transform.position.z), Vector3.right, 180.0f);           
            rb.gravityScale *= -1;

            //forRotate.transform.rotation = Quaternion.Euler(270,90,90);
            /*实例化切换人物后的粒子特效*/
            explosions = Instantiate(explosion,
                new Vector3(transform.position.x,transform.position.y + 0.80f * playerHeight,transform.position.z),
               Quaternion.identity);//旋转改为坡度

            //Debug.Log(ps.startRotation3D);
            Destroy(explosions, 1.0f);
            playerHeight = -playerHeight;
            animator.SetFloat("playerHeight", playerHeight);
            time = 0;
        }
	}

    void getTurnAroundGJ()
    {
        //人物翻转时粒子特效的四种情况
        if (playerHeight < 0 && blackAnim)
        {
            Debug.Log("白上");
            ParticleSystem ps = explosion.GetComponentInChildren<ParticleSystem>();
            /*改变颜色*/
            ps.startColor = new Color(0, 0, 0);
            //Debug.Log(ps.startColor);
            /*改变旋转方向*/
            ps.startRotation3D = new Vector3(180, ps.startRotation3D.y, ps.startRotation3D.z);
        }
        if (playerHeight < 0 && whiteAnim)
        {
            Debug.Log("黑上");
            ParticleSystem ps = explosion.GetComponentInChildren<ParticleSystem>();
            /*改变颜色*/
            ps.startColor = new Color(255, 255, 255);
            //Debug.Log(ps.startColor);
            /*改变旋转方向*/
            ps.startRotation3D = new Vector3(180, ps.startRotation3D.y, ps.startRotation3D.z);
        }
        if (playerHeight > 0 && blackAnim)
        {
            Debug.Log("白下");
            ParticleSystem ps = explosion.GetComponentInChildren<ParticleSystem>();
            /*改变颜色*/
            ps.startColor = new Color(0, 0, 0);
            //Debug.Log(ps.startColor);
            /*改变旋转方向*/
            ps.startRotation3D = new Vector3(0, ps.startRotation3D.y, ps.startRotation3D.z);
        }
        if (playerHeight > 0 && whiteAnim)
        {
            Debug.Log("黑下");
            ParticleSystem ps = explosion.GetComponentInChildren<ParticleSystem>();
            /*改变颜色*/
            ps.startColor = new Color(255, 255, 255);
            //Debug.Log(ps.startColor);
            /*改变旋转方向*/
            ps.startRotation3D = new Vector3(0, ps.startRotation3D.y, ps.startRotation3D.z);
        }
    }
}
