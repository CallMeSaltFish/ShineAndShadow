using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForText : MonoBehaviour {
    /*协程*/
    private IEnumerator enumerator;          

    /*飞刀怪*/
    public GameObject flyMonster;
    // Use this for initialization
    void Start () {
        enumerator = MakeMonster(2);
        StartCoroutine(enumerator);
	}
	
	// Update is called once per frame
	void Update () {
        //开启协程
       

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 6f * gameObject.GetComponent<Rigidbody2D>().gravityScale, 0), ForceMode2D.Impulse);
        }
	}

    private IEnumerator MakeMonster(float waitTime)
    {
        while (true)
        {
            GameObject a = Instantiate(flyMonster, new Vector3(transform.position.x + 10,
                transform.position.y + Random.Range(-1.5f, 4.5f), 0), Quaternion.identity);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
