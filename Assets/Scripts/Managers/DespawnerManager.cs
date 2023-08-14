using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnerManager : MonoBehaviour
{
    [SerializeField]
    DespawnerEnum despawnerEnum;

    public void RespawnEnemy(GameObject e)
    {
        switch (despawnerEnum)
        {
            case DespawnerEnum.TOP:
                GameManager.instance.enemyManager.RespawnBot(e); 
                break;
            case DespawnerEnum.BOT:
                GameManager.instance.enemyManager.RespawnTop(e);
                break;
            case DespawnerEnum.LEFT:
                GameManager.instance.enemyManager.RespawnRight(e);
                break;
            case DespawnerEnum.RIGHT:
                GameManager.instance.enemyManager.RespawnLeft(e);
                break;
        }
    }
}
