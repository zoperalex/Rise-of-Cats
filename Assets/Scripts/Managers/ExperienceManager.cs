using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    private int expCounter;

    private void Start()
    {
        expCounter = 0;
    }

    public int GetExpCounter()
    {
        return expCounter;
    }

    public void IncrementExpCount()
    {
        expCounter++;
        if(expCounter >= gameObject.transform.childCount) 
        { 
            expCounter = 0;
        }
    }
}
