using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject enemies;
    public Camera mainCamera;
    public FloatingJoystick floatingJoystick;

    public float speed = 3;
    public float health = 100;
    public float attack = 1;
    public float attackSpeed = 1;
    public float attackSpeedCD = 0;
    public AttackType attackType = AttackType.NONE;
    public float projectileSpeed = 10f;

    public GameObject attacks;
    private int attackCounter = 0;

    private bool attackReady;

    void Start()
    {
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
            if (attackSpeedCD > 1/attackSpeed)
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
        for (int i=0; i<enemies.transform.childCount;i++)
        {
            if (!enemies.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                continue;
            }
            if (Vector3.Distance(transform.position, enemies.transform.GetChild(i).transform.position)< Vector3.Distance(transform.position, target))
            {
                target = enemies.transform.GetChild(i).transform.position;
            }
        }

        if(target.Equals(new Vector3(float.MaxValue, float.MaxValue, 0)))
        {
            return;
        }

        GameObject attack = null;
        Transform pool;

        pool = (attackType == AttackType.NONE || attackType == AttackType.RANGED) ? attacks.transform.GetChild(0) : attacks.transform.GetChild(1);
        attack = pool.GetChild(attackCounter).gameObject;

        attackCounter = attackCounter+1 >= pool.childCount? 0 : attackCounter+1;

        attack.transform.position = transform.position;
        attack.GetComponent<bulletController>().target = target;
        attack.SetActive(true);
    }
}