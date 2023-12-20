using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Axe, Axe2H, Mace, Mace2H, Scythe, Spear, Spear2H
}
public class PlayerWeapon : GameBehaviour
{
    public int damage;
    public WeaponType myWeapon;

    void Start()
    {
        DamageCal();
    }

    //Checks to see if item has collided with enemy.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            print ("hit Enemy"); 
            other.GetComponent<Enemy>().Hit(damage);
        }
    }

    //Holds the damage vaules for wepons.
    private void DamageCal()
    {
        switch (myWeapon)
        {
            case WeaponType.Axe:
                damage = 30;
                break;
            case WeaponType.Axe2H:
                damage = 50;
                break;
            case WeaponType.Mace:
                damage = 25;
                break;
            case WeaponType.Mace2H:
                damage = 40;
                break;
            case WeaponType.Scythe:
                damage = 60;
                break;
            case WeaponType.Spear:
                damage = 20;
                break;
            case WeaponType.Spear2H:
                damage = 35;
                break;
        }
    }
}
