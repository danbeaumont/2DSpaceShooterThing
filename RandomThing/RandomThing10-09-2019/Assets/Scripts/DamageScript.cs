using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        // find the health script for this object
        Health hp = gameObject.GetComponent<Health>();

        // check that there is a health script, otherwise do nothing
        if (hp != null)
        {
            // if object that has collided is a player shot and this object is an enemy
            if (collision.gameObject.tag == "PlayerShot" && gameObject.tag == "EnemyShip")
            {
                // damage the enemy by the amount specified in the shot script
                hp.TakeDamage(collision.gameObject.GetComponent<ShotScript>().shotDamage);

                // destroy bullet
                Destroy(collision.gameObject);

            }
            // if the object that has collided is an enemy shot and this object is a player ship
            else if (collision.gameObject.tag == "EnemyShot" && gameObject.tag == "PlayerShip")
            {
                // damage the enemy by the amount specified in the shot script
                hp.TakeDamage(collision.gameObject.GetComponent<ShotScript>().shotDamage);

                // destroy bullet
                Destroy(collision.gameObject);
            }
        }

    }
}
