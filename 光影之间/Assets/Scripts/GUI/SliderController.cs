using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider sliderUp;
    public Slider sliderDown;
    public GameObject player;
    public GameObject BossUp;
    public GameObject BossDown;
   

    // Update is called once per frame
    void Update()
    {
        sliderUp.value = Vector3.Distance(new Vector3(player.transform.position.x, 0, 0),
            new Vector3(BossUp.transform.position.x, 0, 0)) / 5f;
        sliderDown.value = Vector3.Distance(new Vector3(player.transform.position.x, 0, 0),
            new Vector3(BossDown.transform.position.x, 0, 0)) / 5f;
    }
}
