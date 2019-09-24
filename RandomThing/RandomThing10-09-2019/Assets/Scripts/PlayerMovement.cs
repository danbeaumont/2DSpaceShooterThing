using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    BoundsManager bounds;
    CameraScroll cameraScroller;
    GameManagerScript gm;
    Rigidbody rb;

    public float moveSpeed = 3.0f;
    public Vector2 moveDir;

    //Vector3 posOnScreen;

    private void Start()
    {
        cameraScroller = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScroll>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>();
        rb = GetComponent<Rigidbody>();
        bounds = GetComponent<BoundsManager>();
    }

    private void Update()
    {
        // calculate player position on screen
        bounds.CalculateBounds(transform.position, 0, 1, 0, 1);

        // get desired movement direction
        moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // calculate desired velocity
        Vector3 movement = moveDir * moveSpeed * Time.deltaTime;
        rb.velocity = movement;

        // check bounds
        Vector3 currentVelocity = rb.velocity;

        // stop player from moving off the left/right of screen
        if (bounds.posOnScreen.x == 0 && currentVelocity.x < 0 || bounds.posOnScreen.x == 1 && currentVelocity.x > 0)
        {
            currentVelocity.x = 0;
        }

        // stop player from moving off the top/bottom of screen
        if (bounds.posOnScreen.y == 0 && currentVelocity.y < 0 || bounds.posOnScreen.y == 1 && currentVelocity.y > 0)
        {
            currentVelocity.y = 0;
        }

        // update actual ship velocity (including camera scroll)
        rb.velocity = currentVelocity + new Vector3(cameraScroller.cameraRigidBody.velocity.x, 0, 0);
      
    }

}
