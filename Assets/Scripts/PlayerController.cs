using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed;
    float health;
    float maxHealth;
    float attackDamage;
    float attackSpeed;
    float attackSpeedCD;
    AttackType attackType;
    float projectileSpeed;
    Sprite projectileSprite;

    public GameObject enemies;
    public Camera mainCamera;
    public FloatingJoystick floatingJoystick;
    public GameObject attacks;
    private int attackCounter = 0;
    private bool attackReady;

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        this.maxHealth = GameManager.instance.playerConfig.health;
        this.health = GameManager.instance.playerConfig.health;
        this.speed = GameManager.instance.playerConfig.speed;
        this.attackDamage = GameManager.instance.playerConfig.attackDamage;
        this.attackSpeed = GameManager.instance.playerConfig.attackSpeed;
        this.attackType = GameManager.instance.playerConfig.attackType;
        this.projectileSpeed = GameManager.instance.playerConfig.projectileSpeed;
        this.projectileSprite = GameManager.instance.playerConfig.projectileSprite;
    }

    void Update()
    {
        if (attackReady)
        {
            Attack();
            attackReady = false;
        }
        if (!attackReady)
        {
            attackSpeedCD += Time.deltaTime;
            if (attackSpeedCD > 1 / attackSpeed)
            {
                attackReady = true;
                attackSpeedCD = 0;
            }
        }
    }

    public void FixedUpdate()
    {
        Vector3 movement = new Vector3(floatingJoystick.Horizontal * Time.deltaTime * speed, floatingJoystick.Vertical * Time.deltaTime * speed, 0);
        transform.position += movement;
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
    }

    private void Attack()
    {
        Vector3 target = new Vector3(float.MaxValue, float.MaxValue, 0);
        for (int i = 0; i < enemies.transform.childCount; i++)
        {
            if (!enemies.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                continue;
            }
            if (Vector3.Distance(transform.position, enemies.transform.GetChild(i).transform.position) < Vector3.Distance(transform.position, target))
            {
                target = enemies.transform.GetChild(i).transform.position;
            }
        }

        if (target.Equals(new Vector3(float.MaxValue, float.MaxValue, 0)))
        {
            return;
        }

        GameObject attack = null;
        Transform pool;

        pool = (attackType == AttackType.NONE || attackType == AttackType.RANGED) ? attacks.transform.GetChild(0) : attacks.transform.GetChild(1);
        attack = pool.GetChild(attackCounter).gameObject;

        attackCounter = attackCounter + 1 >= pool.childCount ? 0 : attackCounter + 1;

        attack.transform.position = transform.position;
        if (attackType == AttackType.NONE || attackType == AttackType.RANGED) attack.GetComponent<ProjectileController>().Setup(projectileSpeed, attackDamage, (target - transform.position).normalized, projectileSprite);
        //else 
        attack.SetActive(true);
    }

    public void ChangeHealth(float amount)
    {
        health += amount;
        if (health <= 0) Die();
    }

    private void Die()
    {

    }
}