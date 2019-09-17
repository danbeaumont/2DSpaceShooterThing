using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float scrollSpeed;
    public Vector2 scrollDirection;

    GameManagerScript gm;
    public Rigidbody cameraRigidBody;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>();
        cameraRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        cameraRigidBody.velocity = gm.cameraScrollDirection * gm.cameraScrollSpeed * Time.deltaTime;
    }
    
}
