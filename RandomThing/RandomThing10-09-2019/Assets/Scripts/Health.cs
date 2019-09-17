using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;                            // amount of health for this object

    // function that calculates damage and can be called on collision
    public void TakeDamage(float damageAmount)
    {
        // remove the damage from the current health
        health -= damageAmount;

        // if health is expired
        if (health <= 0)
        {
            // add score
            // play particle effect, etc

            // destroy the object
            Destroy(gameObject);
        }
    }
}
