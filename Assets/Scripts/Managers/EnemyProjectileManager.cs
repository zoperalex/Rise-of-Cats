using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileManager : MonoBehaviour
{
    private int projectileCounter;

    private void Start()
    {
        projectileCounter = 0;
    }

    public void IncrementCounter()
    {
        projectileCounter++;
        if(projectileCounter >= gameObject.transform.childCount) projectileCounter = 0;
    }

    public int GetCounter()
    {
        return projectileCounter;
    }
}
