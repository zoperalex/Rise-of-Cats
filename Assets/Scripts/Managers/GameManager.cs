using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;
    [HideInInspector] public PlayerConfig playerConfig;
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public EnemyManager enemyManager;

    public List<EnemyConfig> BasicEnemies;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
