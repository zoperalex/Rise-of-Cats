using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed;
    public float health;
    float maxHealth;
    float attackDamage;
    float attackSpeed;
    float attackSpeedCD;
    AttackType attackType;
    float projectileSpeed;
    Sprite projectileSprite;

    public HealthBarController healthBarController;
    public EXPBarController expBarController;
    public TextMeshProUGUI levelText;
    public GameObject enemies;
    public Camera mainCamera;
    public FloatingJoystick floatingJoystick;
    public GameObject attacks;

    private int attackCounter = 0;
    private bool attackReady;

    [SerializeField]
    private int level = 1;
    [SerializeField]
    private int currentExp = 0;
    private int currentExpGoal;
    private float expGoalIncrementPercentage = 0.25f;
    private bool invulnerable = false;

    private Vector3 previousTarget;

    private void Start()
    {
        GameManager.instance.playerController = this;
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
        this.currentExpGoal = GameManager.instance.playerConfig.startingExpGoal;

        healthBarController.SetMaxHealth(this.maxHealth);
        expBarController.SetMaxEXP(this.currentExpGoal);
        levelText.text = level.ToString();

        for(int i=0; i<20; i++)
        {
            GameObject attack = null;
            if (this.attackType.Equals(AttackType.NONE) || this.attackType.Equals(AttackType.RANGED)) attack = Instantiate(GameManager.instance.projectileAttackPrefab, transform.position, Quaternion.identity, attacks.transform);
            else if (this.attackType.Equals(AttackType.MELEE)) attack = Instantiate(GameManager.instance.meleeAttackPrefab, transform.position, Quaternion.identity, attacks.transform);

            attack.SetActive(false);
        }
    }

    void Update()
    {
        if (attackReady)
        {
            Attack();
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
        if(attackType.Equals(AttackType.MELEE) && !movement.Equals(new Vector3(0,0,0)))
        {
            previousTarget = movement.normalized;
        }
    }

    private void Attack()
    {

        GameObject attack;

        attack = attacks.transform.GetChild(attackCounter).gameObject;
        attackCounter = attackCounter + 1 >= attacks.transform.childCount ? 0 : attackCounter + 1;
        attack.SetActive(false);
        attack.transform.position = transform.position;

        if (attackType == AttackType.NONE || attackType == AttackType.RANGED)
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

            if (target.Equals(new Vector3(float.MaxValue, float.MaxValue, 0)) && (attackType.Equals(AttackType.RANGED) || attackType.Equals(AttackType.NONE)))
            {
                attackReady = true;
                return;
            }

            Vector2 direction = new Vector2(target.x - transform.position.x, target.y - transform.position.y);

            attack.transform.up = direction;

            attack.GetComponent<ProjectileController>().Setup(projectileSpeed, attackDamage, direction.normalized, projectileSprite);
        }
        else if (attackType.Equals(AttackType.MELEE)) {
            Vector3 target = new Vector3(floatingJoystick.Horizontal, floatingJoystick.Vertical, 0);
            if (target.Equals(new Vector3(0, 0, 0)))
            {
                if(!previousTarget.Equals(new Vector3(0, 0, 0)))
                {
                    target = previousTarget;
                }
                else
                {
                    target = new Vector3(0,-1,0);
                }
            }

            previousTarget = target;
            attack.transform.up = target;

            attack.GetComponent<MeleeAttackController>().Setup(projectileSpeed, target.normalized, attackDamage ,projectileSprite);
        }

        attack.SetActive(true);
        attackReady = false;
    }

    public void ChangeHealth(float amount)
    {
        if (amount < 0 && invulnerable) return;
        health += amount;
        healthBarController.SetHealth(health);
        if (health <= 0) Die();
    }

    private void Die()
    {

    }

    public void AddExp(int amount)
    {
        currentExp += amount;
        expBarController.SetEXP(currentExp);
        if (currentExp >= currentExpGoal)
        {
            LevelUp();
            int tempExpGoal = currentExpGoal;
            int tempExp = currentExp;
            currentExpGoal += (int)((float)currentExpGoal * expGoalIncrementPercentage);
            expBarController.SetMaxEXP(currentExpGoal);
            currentExp = 0;
            AddExp(tempExp % tempExpGoal);
        }
    }

    private void LevelUp()
    {
        level++;
        levelText.text = level.ToString();
        invulnerable = true;
        StartCoroutine(InvulnerabilityTimer());
        //do whatever and get upgrades
    }

    IEnumerator InvulnerabilityTimer()
    {
        yield return new WaitForSeconds(1f);
        invulnerable = false;
    }
}