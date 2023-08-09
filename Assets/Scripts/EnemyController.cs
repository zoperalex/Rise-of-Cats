using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float speed = 1.5f;
    float health = 2;
    float attackDamage = 1;
    float attackSpeed = 1f;
    public PlayerController player;
    private bool attacking = false;
    private bool attackReady = true;
    private float attackSpeedCD = 0;
    private AttackType attackType=AttackType.MELEE;

    void Update()
    {
        if(!attacking) transform.position = Vector3.MoveTowards(transform.position, player.gameObject.transform.position, speed*Time.deltaTime);
        else
        {
            if(attackReady)
            {
                Attack();
                attackReady = false;
            }
        }

        if (!attackReady)
        {
            attackSpeedCD += Time.deltaTime;
            if(attackSpeedCD > attackSpeed)
            {
                attackReady = true;
                attackSpeedCD = 0;
            }
        }
    }

    private void Attack()
    {
        if (attackType.Equals(AttackType.MELEE))
        {
            player.health -= attackDamage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name==player.name)
        {
            attacking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == player.name)
        {
            attacking = false;
        }
    }
}
