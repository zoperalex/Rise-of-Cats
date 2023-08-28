using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Upgrades/Attack Speed")]
public class AttackSpeedUpgrade : Upgrade
{
    public override void Choose()
    {
        LevelUp();
        GameManager.instance.playerController.SetAttackSpeedMultiplier(level * 0.1f + 1);
        if (level == 1) GameManager.instance.playerController.IncreaseStatUpgrades(this);
    }

    protected override void LevelUp()
    {
        level++;
        if (level == 5)
        {
            //do something;
        }
    }
}
