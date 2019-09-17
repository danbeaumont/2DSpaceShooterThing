using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject shotObject;
    public GameObject[] shotPoints;
    public float shotOffsetX;
    public float shotOffsetY;
    public float fireRate = 0.5f;
    float fireRateTimer = 0;

    bool isPlayer = false;
    bool isEnemy = false;

    public GameObject currentTarget;                    // only used by enemies

    // Start is called before the first frame update
    void Start()
    {
        // check if this is a player ship or enemy ship and set appropriate boolean (so we can use the same script for all shooting)
        if (gameObject.tag == "PlayerShip")
        {
            isPlayer = true;
            isEnemy = false;

            // store all of the shot points on the ship (doing it here in start means easy modification of ship attributes)
            shotPoints = GameObject.FindGameObjectsWithTag("PlayerShotPoint");
        }
        else
        {
            isPlayer = false;
            isEnemy = true;

            // store all of the shot points on the ship (doing it here in start means easy modification of ship attributes)
            shotPoints = GameObject.FindGameObjectsWithTag("EnemyShotPoint");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check if this object is the player
        if (isPlayer)
        {
            // ...and if it is check if the fire button has been pressed
            if (Input.GetButtonDown("Fire1"))
            {
                FireShot();
            }
        }
        // if its not the player, just fire anyways if its time to
        else
        {
            // set the current target to the player ship
            currentTarget = GameObject.FindGameObjectWithTag("PlayerShip");

            FireShot();
        }

        // update fire rate timer
        fireRateTimer -= Time.deltaTime;

    }

    void FireShot()
    {
        // check if button is pressed and its time to fire
        if (fireRateTimer <= 0)
        {
            // for each shot point on the player ship
            for (int i = 0; i < shotPoints.Length; i++)
            {
                // fire a shot from the shot point in the direction it is facing
                GameObject newShot = Instantiate(shotObject, new Vector3(shotPoints[i].transform.position.x + shotOffsetX, shotPoints[i].transform.position.y + shotOffsetY, shotPoints[i].transform.position.z), shotPoints[i].transform.rotation);

                // check that there is a current target set (there wont be one if this is the player ship)
                if (currentTarget != null)
                {
                    // work out the shot direction
                    Vector2 shotDir = currentTarget.transform.position - shotPoints[i].transform.position;
                    // normalize the vector
                    shotDir.Normalize();
                    // update the shot direction in the shot script of the newly instantiated shot
                    newShot.GetComponent<ShotScript>().shotDirection = shotDir;
                }
            }

            // reset timer
            fireRateTimer = fireRate;
        }

    }
}
