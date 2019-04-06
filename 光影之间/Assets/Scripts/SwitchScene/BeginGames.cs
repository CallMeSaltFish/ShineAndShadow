using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginGames : MonoBehaviour {

    public string sceneName;
    private int index1;

	// Use this for initialization
	void Start () {
        index1 = PlayerPrefs.GetInt("Star");
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
