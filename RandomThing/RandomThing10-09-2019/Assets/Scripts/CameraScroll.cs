using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    GameManagerScript gm;

    public float slowHorSpeed = 50f;
    public float slowVertSpeed = 100f;
    public Vector3 scrollDirection;

    public Rigidbody cameraRigidBody;
    public GameObject camTarget;

    public float slowScrollBuffer = 0.4f;
    public float fastScrollBuffer = 0.2f;

    public float vertMinPosition;
    public float vertMaxPosition;
    public float horMinPosition;
    public float horMaxPosition;

    public Vector3 posOnScreen;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>();
        cameraRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float currentHorSpeed = slowHorSpeed;
        float currentVertSpeed = slowVertSpeed;
        Vector3 currentVelocity = new Vector3(scrollDirection.x * currentHorSpeed * Time.deltaTime, scrollDirection.y * currentVertSpeed * Time.deltaTime, 0);

        if (camTarget != null)
        {
            // find target position on screen
            posOnScreen = Camera.main.WorldToViewportPoint(camTarget.transform.position);
        }

        // scroll camera up if target reaches the scroll buffer
        if (posOnScreen.y > (1 - slowScrollBuffer))
        {
            scrollDirection = new Vector3(scrollDirection.x, 1);

            // scroll faster as player reaches edge
            if (posOnScreen.y > (1 - fastScrollBuffer))
            {
                currentVertSpeed = camTarget.GetComponent<PlayerMovement>().moveSpeed;
            }

            // clamp to top of play area (vertical)
            if (transform.position.y >= vertMaxPosition)
            {
                scrollDirection = new Vector3(scrollDirection.x, 0);
            }

        }
        // scroll camera down as target reaches buffer
        else if (posOnScreen.y < (0 + slowScrollBuffer))
        {
            scrollDirection = new Vector3(scrollDirection.x, -1);

            // scroll faster as player reaches edge
            if (posOnScreen.y < (0 + fastScrollBuffer))
            {
                currentVertSpeed = camTarget.GetComponent<PlayerMovement>().moveSpeed;
            }

            // clamp to bottom of play area (vertical)
            if (transform.position.y <= vertMinPosition)
            {
                scrollDirection = new Vector3(scrollDirection.x, 0);
            }

        }
        // stop camera scrolling if no buffer is hit
        else
        {
            scrollDirection = new Vector3(scrollDirection.x, 0);
        }

        // update actual camera velocity
        currentVelocity = new Vector3(scrollDirection.x * currentHorSpeed * Time.deltaTime, scrollDirection.y * currentVertSpeed * Time.deltaTime, 0);
        cameraRigidBody.velocity = currentVelocity;
    }
    
}
