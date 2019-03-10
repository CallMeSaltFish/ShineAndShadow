using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginGames : MonoBehaviour {
    public string sceneName;
    private int index1;
    private int index2;
    void Awake()
    {
       
    }
	// Use this for initialization
	void Start () {
        Debug.Log(PlayerPrefs.GetInt("Star"));
        index1 = PlayerPrefs.GetInt("Star");

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //public void beginGames()
    //{
    //    SceneManager.LoadScene("SampleScene");
    //}
    //public void historyGames()
    //{
    //    SceneManager
    //}
    public void Switch(string name)
    {
        PlayerPrefs.SetInt("Star", index1);
        this.sceneName = name;
        SceneManager.LoadScene(sceneName);
    }
}
