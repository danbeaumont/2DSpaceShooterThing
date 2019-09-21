using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    GameManagerScript gm;

    public float horScrollSpeed = 50f;
    public float vertScrollSpeed = 100f;
    public Vector3 scrollDirection;

    public Rigidbody cameraRigidBody;
    public GameObject camTarget;

    public float scrollBuffer = 0.2f;

    public float vertMinPosition;
    public float vertMaxPosition;
    public float horMinPosition;
    public float horMaxPosition;

    Vector3 posOnScreen;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>();
        cameraRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (camTarget != null)
        {
            // find target position on screen
            posOnScreen = Camera.main.WorldToViewportPoint(camTarget.transform.position);
        }


        // stop screen scrolling at edges of play area (vertical)
        if (transform.position.y > vertMinPosition || transform.position.y < vertMaxPosition)
        {
            // scroll camera up/down if target reaches the scroll buffer
            if (posOnScreen.y > (1 - scrollBuffer))
            {
                scrollDirection = new Vector3(scrollDirection.x, 1);
            }
            else if (posOnScreen.y < (0 + scrollBuffer))
            {
                scrollDirection = new Vector3(scrollDirection.x, -1);
            }
            else
            {
                scrollDirection = new Vector3(scrollDirection.x, 0);
            }
        }

        // update actual camera velocity
        cameraRigidBody.velocity = new Vector3(scrollDirection.x * horScrollSpeed * Time.deltaTime, scrollDirection.y * vertScrollSpeed * Time.deltaTime, 0);
    }
    
}
