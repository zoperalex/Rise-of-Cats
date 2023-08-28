using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    private int expCounter;
    public List<Sprite> expSprites;

    private void Start()
    {
        GameManager.instance.expManager = this;
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

    public Sprite GetSprite(int amount)
    {
        if(expSprites.Count == 0)
        {
            return null;
        }
        switch(amount)
        {
            case < 5:
                return expSprites[0];
            case < 50:
                return expSprites[1];
            case < 250:
                return expSprites[2];
            case < 500:
                return expSprites[3];
            case < 1000:
                return expSprites[4];
            case 1000:
                return expSprites[5];
            default:
                return null;
        }
    }
}
