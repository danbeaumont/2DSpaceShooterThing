using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    public float shotSpeed = 300.0f;
    public float shotRange = 1.0f;
    public float shotDamage = 10.0f;
    public Vector2 shotDirection;

    GameManagerScript gm;
    BoundsManager bounds;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>();
        bounds = GetComponent<BoundsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // destroy self when timer expires
        if (shotRange <= 0)
        {
            Destroy(gameObject);
        }

        // countdown timer
        shotRange -= Time.deltaTime;

        bounds.CalculateBounds(transform.position, 0, 1, 0, 1);

        // destroy if off screen
        if (bounds.posOnScreen.x == 0 || bounds.posOnScreen.x == 1 || bounds.posOnScreen.y == 0 || bounds.posOnScreen.y == 1)
        {
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {
        // move the shot across the screen in the desired direction
        GetComponent<Rigidbody>().velocity = shotDirection * shotSpeed * Time.deltaTime;

    }
}
