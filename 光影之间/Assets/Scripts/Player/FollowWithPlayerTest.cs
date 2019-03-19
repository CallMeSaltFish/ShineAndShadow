using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWithPlayerTest : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [Range(0, 1)]
    public float cameraSpeed;
    private Vector3 cameraVelocity = Vector3.zero;
    private Vector3 offset;
    private Vector3 endPosition;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        offset = transform.position - playerTransform.position;
    }
    // Update is called once per frame
    void Update()
    {
        endPosition = playerTransform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(endPosition.x, playerTransform.position.y, transform.position.z), ref cameraVelocity, cameraSpeed);
    }
}
