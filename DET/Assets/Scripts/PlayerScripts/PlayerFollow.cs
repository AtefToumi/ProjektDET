using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{

    // public Transform target;
    Transform target;

    public float smoothSpeed = 0.5f;
    public Vector3 offset;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }
    
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition; 
        target = player.transform;
    }
}
