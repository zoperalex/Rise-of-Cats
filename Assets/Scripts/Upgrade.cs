using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public string titleText;
    public string descriptionText;
    public Sprite upgradeSprite;
    public int level;

    private void Start()
    {
        level = 0;    
    }

    public void levelUp()
    {
        level++;
        if (level == 5) MaxLevel();
    }

    private void MaxLevel()
    {
        //disable this upgrade, check if you can add mk2 upgrade
    }
}
