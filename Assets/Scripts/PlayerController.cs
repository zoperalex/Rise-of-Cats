using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject enemies;
    public Camera mainCamera;
    public float speed = 3;
    public FloatingJoystick floatingJoystick;
    public float health = 100;
    public float attack = 1;
    public float attackSpeed = 1;
    public AttackType attackType = AttackType.NONE;

    public GameObject projectiles;
    private int projectileCounter = 0;
    public float projectileSpeed = 10f;

    void Start()
    {
        Attack();

    }

    void Update()
    {
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
            if (Vector3.Distance(transform.position, enemies.transform.GetChild(i).transform.position)< Vector3.Distance(transform.position, target))
            {
                target = enemies.transform.GetChild(i).transform.position;
            }
        }
        GameObject projectile = projectiles.transform.GetChild(projectileCounter).gameObject;
        projectile.transform.rotation = new Quaternion(0, 0, Vector3.Angle(transform.position, target), 1);

        projectile.SetActive(true);
    }
}