using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMover : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * moveSpeed);
        Destroymyself();
    }

    void Destroymyself()
    {
        if (moveSpeed == 2.5f&&gameObject.name!= "第三关-1")
        {
            Destroy(gameObject, 100);
        }
    }
}
