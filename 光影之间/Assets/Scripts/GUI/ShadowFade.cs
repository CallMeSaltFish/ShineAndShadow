using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFade : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r,
                gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b,
                gameObject.GetComponent<SpriteRenderer>().color.a * (1 - Time.deltaTime / 0.3f));
    }
}
