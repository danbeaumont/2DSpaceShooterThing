using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    public float shotSpeed = 300.0f;
    public float shotRange = 1.0f;
    public float shotDamage = 10.0f;
    public Vector2 shotDirection;

    // Update is called once per frame
    void Update()
    {
        // destroy self when timer expires
        if (shotRange <= 0)
        {
            Destroy(this.gameObject);
        }

        // countdown timer
        shotRange -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // move the shot across the screen in the desired direction
        GetComponent<Rigidbody>().velocity = shotDirection * shotSpeed * Time.deltaTime;
    }
}
