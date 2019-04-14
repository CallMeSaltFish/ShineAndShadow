using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour {
    /*当前场景名*/
    private string SceneName;
    private AudioSource audioSource;
    public AudioClip[] songs;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        GameObject[] audioPlayers = GameObject.FindGameObjectsWithTag("AudioPlayer");
        if(audioPlayers.Length == 2)
        {
            Destroy(audioPlayers[1]);
        }
    }
    // Update is called once per frame
    void Update () {
        GetSceneName();
        GetMusic();
    }
    void GetSceneName()
    {
        SceneName = SceneManager.GetActiveScene().name;
    }
    void GetMusic()
    {
        if (SceneName == "SampleScene")
        {
            if (audioSource.clip != songs[0])
            {
                audioSource.enabled = false;
                audioSource.clip = songs[0];
                audioSource.enabled = true;
            }
        }
        else
        {
            if (audioSource.clip != songs[1])
            {
                audioSource.enabled = false;
                audioSource.clip = songs[1];
                audioSource.enabled = true;                
            }
        }
    }
}
