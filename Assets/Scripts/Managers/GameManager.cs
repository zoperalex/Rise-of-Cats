using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;
    [HideInInspector] public PlayerConfig playerConfig;
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public EnemyManager enemyManager;
    [HideInInspector] public UpgradesManager upgradesManager;

    public List<EnemyConfig> BasicEnemies;
    public List<EnemyConfig> BasicRangedEnemies;

    public GameObject projectileAttackPrefab;
    public GameObject meleeAttackPrefab;

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

    public void ActivateUpgradesMenu()
    {
        upgradesManager.ActivateUpgradesMenu();
    }
}
