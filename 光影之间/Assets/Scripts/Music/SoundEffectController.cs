using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SoundEffectController : MonoBehaviour
{
    /*当前场景名*/
    private string SceneName;
    private AudioSource audioSource;
    public AudioClip[] songs;
    public PlayerMove playerMove;
    /*吃食物的音乐*/
    private int foodMusic;
    /*吃拼图的音乐*/
    private int jigMusic;
    // Start is called before the first frame update
    void Start()
    {
        GetSceneName();
        //if (SceneName == "SampleScene") playerMove = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerMove>();
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        getFoodMusic();
        getJigMusic();
    }

    void GetSceneName()
    {
        SceneName = SceneManager.GetActiveScene().name;
    }
    /// <summary>
    /// 按钮点击音效
    /// </summary>
    public void buttonClickMusic()
    {
        audioSource.clip = songs[0];
        audioSource.Play();
        Debug.Log("点击了一下");
    }
    /// <summary>
    /// 吃食物的音乐
    /// </summary>
    void getFoodMusic()
    {
        if (playerMove != null)
        {
            if (playerMove.foodMusic > foodMusic)
            {
                audioSource.clip = songs[1];
                audioSource.Play(); foodMusic++;
            }
        }
    }
    /// <summary>
    /// 吃拼图的音乐
    /// </summary>
    void getJigMusic()
    {
        if (playerMove != null)
        {
            if (playerMove.jigMusic > jigMusic)
            {
                audioSource.clip = songs[2];
                audioSource.Play(); jigMusic++;
            }
        }
    }
}
