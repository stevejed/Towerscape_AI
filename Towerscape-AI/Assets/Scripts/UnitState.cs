using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState : MonoBehaviour {

    // Variables //


    // variables for: combat and movement //
    float ammo;
    float attackDamage;
    float hp;
    float range;
    float speed;

    // variables for: drops //
    int goldDropped;
    GameObject itemDropped;

    // variables for: player interaction and spawning //
    bool grabbable;
    int unitCost;

    // variables for: animation triggers //
    /*TO-DO
     * * Populate w/ animation triggers
     */


    // Methods //

    // Method for: Taking damage and modifying the unit's health.  If it drops below 0, the unit dies.
    public void takeDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0f)
        {
            this.die();
        }
    }

    // Method for: killing the unit //
    public void die()
    {
        Destroy(gameObject);
    }

    // Methods for: getting UnitState components //
    public float getAmmo() { return ammo; }
    public float getAttackDamage(){ return attackDamage; }
    public float getHP() { return hp; }
    public float getRange() { return range; }
    public float getSpeed() { return speed; }
    public bool getGrabbable() { return grabbable; }
    public int getUnitCost() { return unitCost; }
}
