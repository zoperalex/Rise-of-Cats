using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;
    public float speed = 3;
    public FloatingJoystick floatingJoystick;
    public float health = 100;
    public float attack = 1;
    public float attackSpeed = 1;
    public AttackType attackType = AttackType.NONE;

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

    }
}