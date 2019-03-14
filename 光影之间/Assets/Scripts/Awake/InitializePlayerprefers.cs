using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePlayerprefers : MonoBehaviour
{
    void Awake()
    {
        PlayerPrefs.DeleteKey("index");
    }
}
