using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    CameraScroll cameraScroller;

    float camVert;
    float camHor;

    float minX;
    float minY;
    float maxX;
    float maxY;

    private void Start()
    {
        cameraScroller = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScroll>();

        camVert = Camera.main.orthographicSize;
        camHor = camVert * Screen.width / Screen.height;

        minX = camHor - 100f / 2.0f;
        maxX = 100f / 2.0f - camHor;
        minY = camVert - 100f / 2.0f;
        maxY = 100f / 2.0f - camVert;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get input
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        // apply movement
        Vector2 movementDirection = new Vector2(hor, vert);
        Vector3 newVelocity = CheckBounds(movementDirection);
        // keep the player moving at the same speed as the camera
        GetComponent<Rigidbody>().velocity = newVelocity + cameraScroller.cameraRigidBody.velocity;

    }

    Vector3 CheckBounds(Vector2 moveDir)
    {
        float camVert = Camera.main.orthographicSize;
        float camHor = camVert * Screen.width / Screen.height;

        if (transform.position.x <= minX || transform.position.x >= maxX)
        {
            Vector3 newVel = new Vector3(0.0f, moveDir.y, -10f);
            return newVel;
        }
        else if (transform.position.y <= minY || transform.position.y >= maxY)
        {
            Vector3 newVel = new Vector3(moveDir.x, 0.0f, -10f);
            return newVel;
        }
        else
        {
            Vector3 newVel = moveDir * moveSpeed;
            return newVel;
        }
    }
}
