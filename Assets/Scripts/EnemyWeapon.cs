using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : GameBehaviour
{
    public int damage = 30;

    //Checks to see if item has collided with player.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().Hit(damage);
        }
    }
}
