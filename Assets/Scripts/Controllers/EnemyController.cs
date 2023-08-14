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
    AttackType attackType;
    float projectileSpeed;
    Sprite projectileSprite;

    private bool attacking = false;
    private bool attackReady = true;
    private float attackSpeedCD = 0;

    [SerializeField]
    private GameObject enemyAttacks;
    [SerializeField]
    private CircleCollider2D circleCollider;

    int attackCounter;

    public void Setup(float health, 
                      float speed, 
                      float attackDamage, 
                      float attackSpeed, 
                      AttackType attackType, 
                      float projectileSpeed = 0f, 
                      Sprite projectileSprite = null, 
                      Sprite sprite = null,
                      float attackRange = 0f)
    {
        this.health = health;
        this.speed = speed;
        this.attackDamage = attackDamage;
        this.attackSpeed = attackSpeed;
        if(sprite!=null) gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.attackType = attackType;
        if (attackType.Equals(AttackType.MELEE))
        {
            circleCollider.radius = 0.5f;
        }
        else
        {
            this.projectileSpeed = projectileSpeed;
            this.projectileSprite = projectileSprite;
            circleCollider.radius = attackRange;
            attackCounter = 0;
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
        else
        {
            GameObject attack = enemyAttacks.transform.GetChild(attackCounter).gameObject;
            attackCounter = attackCounter + 1 > enemyAttacks.transform.childCount ? 0 : attackCounter + 1;
            attack.GetComponent<EnemyProjectileController>().Setup(projectileSpeed, attackDamage, -(transform.position - player.transform.position), projectileSprite);
            attack.transform.position = transform.position;
            attack.SetActive(true);
        }
        attackReady = false;
    }

    public void TakeDamage(float dmg)
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
        if(collision.tag.Equals("Player"))
        {
            attacking = true;
        }
        else if (collision.tag.Equals("Despawner"))
        {
            collision.gameObject.GetComponent<DespawnerManager>().RespawnEnemy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            attacking = false;
        }
    }
}
