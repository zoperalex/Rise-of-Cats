using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    float health;
    float speed;
    float attackDamage;
    float attackSpeed;
    private AttackType attackType;
    public float projectileSpeed;
    public Sprite projectileSprite;

    private bool attacking = false;
    private bool attackReady = true;
    private float attackSpeedCD = 0;

    public void Setup(float health, 
                      float speed, 
                      float attackDamage, 
                      float attackSpeed, 
                      AttackType attackType, 
                      float projectileSpeed = 0f, 
                      Sprite projectileSprite = null, 
                      Sprite sprite = null)
    {
        this.health = health;
        this.speed = speed;
        this.attackDamage = attackDamage;
        this.attackSpeed = attackSpeed;
        if(sprite!=null) gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.attackType = attackType;
        if (attackType.Equals(AttackType.RANGED))
        {
            this.projectileSpeed = projectileSpeed;
            this.projectileSprite = projectileSprite;
        }
    }

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
            if(attackSpeedCD > 1/attackSpeed)
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
            player.ChangeHealth(-attackDamage);
        }
    }

    private void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health == 0) Die();
    }

    //Need to add way to get loot and xp.
    private void Die()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name==player.name)
        {
            attacking = true;
        }
        else if (collision.tag.Equals("Projectile"))
        {
            TakeDamage(collision.gameObject.transform.GetComponent<ProjectileController>().damage);
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag.Equals("Despawner"))
        {
            collision.gameObject.GetComponent<DespawnerManager>().RespawnEnemy(gameObject);
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
