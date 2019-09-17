using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum MovementType { Straight, BackForth, Diagonal, Chase, EaseInOut }
    public MovementType moveType;

    public float moveSpeed;

    public Vector2 moveDirection;
    Vector2 storedMoveDirection;

    public float changeDirectionRate = 3.0f;
    float changeTimer;

    public float easeInOutTimer = 0.0f;
    public float easeInOutRate = 1.0f;
    public AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerShip");
        changeTimer = changeDirectionRate;
        easeInOutTimer = 0f;
        storedMoveDirection = moveDirection;



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            switch (moveType)
            {
                case MovementType.Straight:
                    // just setup the move direction vector from the inspector to have the ship move in a desired direction
                    break;
                case MovementType.BackForth:

                    // if the timer has expired
                    if (changeTimer <= 0)
                    {
                        // reverse direction
                        moveDirection = -moveDirection;

                        // reset the timer
                        changeTimer = changeDirectionRate;
                    }

                    // count down the change direction timer
                    changeTimer -= Time.deltaTime;

                    break;
                case MovementType.Diagonal:
                    // if the timer has expired
                    if (changeTimer <= 0)
                    {
                        // move left
                        moveDirection.x = -1;
                        // reverse y (up/down) direction
                        moveDirection.y = -moveDirection.y;

                        // reset the timer
                        changeTimer = changeDirectionRate;
                    }

                    // count down the change direction timer
                    changeTimer -= Time.deltaTime;
                    break;
                case MovementType.Chase:
                    // get player direction
                    moveDirection = player.transform.position - transform.position;
                    moveDirection.Normalize();

                    break;

                case MovementType.EaseInOut:

                    if (easeInOutTimer <= easeInOutRate)
                    {
                        easeInOutTimer += Time.deltaTime;
                        float seconds = easeInOutTimer / easeInOutRate;
                        moveDirection = Vector2.Lerp(storedMoveDirection, -storedMoveDirection, curve.Evaluate(seconds));
                    }
                    else
                    {
                        easeInOutTimer = 0f;
                    }



                    break;

            }

            // move the object
            rb.velocity = moveDirection * moveSpeed * Time.deltaTime;

        }
    }
}
