using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRangeController : MonoBehaviour
{
    public EnemyController enemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            enemy.attacking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            enemy.attacking = false;
        }
    }
}
