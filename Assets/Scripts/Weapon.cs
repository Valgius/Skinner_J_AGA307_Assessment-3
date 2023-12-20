using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().Hit(damage);
        }
        if (other.CompareTag("Enemy"))
        {
        }
    }
}
