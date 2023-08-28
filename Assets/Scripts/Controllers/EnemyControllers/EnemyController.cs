using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private EnemyProjectileManager epManager;

    public float health;
    float speed;
    float attackDamage;
    float attackSpeed;
    AttackType attackType;
    float projectileSpeed;
    Sprite projectileSprite;
    ExpDrop expDrop;

    [HideInInspector]
    public bool attacking = false;
    private bool attackReady = true;
    private float attackSpeedCD = 0;

    [SerializeField]
    private GameObject enemyAttacks;
    [SerializeField]
    private CircleCollider2D circleCollider;

    public void Setup(float health,
                      float speed,
                      float attackDamage,
                      float attackSpeed,
                      AttackType attackType,
                      ExpDrop expDrop,
                      float projectileSpeed = 0f, 
                      Sprite projectileSprite = null, 
                      Sprite sprite = null,
                      float attackRange = 0f)
    {
        this.health = health;
        this.speed = speed;
        this.attackDamage = attackDamage;
        this.attackSpeed = attackSpeed;
        this.expDrop = expDrop;
        if(sprite!=null) gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.attackType = attackType;
        if(!attackType.Equals(AttackType.MELEE))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.projectileSpeed = projectileSpeed;
            this.projectileSprite = projectileSprite;
            circleCollider.radius = attackRange;
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
            GameObject attack = enemyAttacks.transform.GetChild(epManager.GetCounter()).gameObject;
            epManager.IncrementCounter();
            attack.GetComponent<EnemyProjectileController>().Setup(projectileSpeed, attackDamage, -(transform.position - player.transform.position), projectileSprite);
            attack.transform.position = transform.position;
            attack.SetActive(true);
        }
        attackReady = false;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0) Die();
    }

    private void Die()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        if (Random.Range(0f, 1f) > expDrop.dropChance)
        {
            return;
        }
        GameObject exp = GameManager.instance.expManager.gameObject.transform.GetChild(GameManager.instance.expManager.GetExpCounter()).gameObject;
        GameManager.instance.expManager.IncrementExpCount();
        int expAmount = Random.Range(expDrop.amountMin, expDrop.amountMax);
        exp.GetComponent<ExperienceController>().Setup(expAmount, GameManager.instance.expManager.GetSprite(expAmount));
        exp.transform.position = transform.position;
        exp.SetActive(true);

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
