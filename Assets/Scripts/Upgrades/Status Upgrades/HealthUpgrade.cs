using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Scriptables/Upgrades/Health")]
public class HealthUpgrade : Upgrade
{
    public override void Choose()
    {
        LevelUp();
        GameManager.instance.playerController.UpgradeHealth();
        if (level == 1) GameManager.instance.playerController.IncreaseStatUpgrades(this);
    }

    protected override void LevelUp()
    {
        level++;
        if(level==5)
        {
            //do something;
        }
    }
}
