using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private int enemyPoolCounter;
    private bool spawnedAll = false;

    private void Start()
    {
        GameManager.instance.enemyManager = this;
        enemyPoolCounter = 0;
    }

    public void SpawnWave(int amount, EnemyConfig ec)
    {
        StartCoroutine(SpawnWaveCoroutine(amount, ec));
    }

    IEnumerator SpawnWaveCoroutine(int amount, EnemyConfig ec)
    {
        System.Random rnd = new System.Random();
        for(int i=0; i < amount; i++)
        {
            switch (rnd.Next(0,4))
            {
                case 0:
                    SetSpawnTop(ec);
                    break;
                case 1:
                    SetSpawnBot(ec);
                    break;
                case 2:
                    SetSpawnLeft(ec);
                    break;
                case 3:
                    SetSpawnRight(ec);
                    break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void SetSpawnTop(EnemyConfig ec)
    {
        Spawn(ec, player.position.x - 9.5f, player.position.x + 9.5f, player.position.y + 4f, player.position.y + 5.5f);
    }

    private void SetSpawnBot(EnemyConfig ec)
    {
        Spawn(ec, player.position.x - 9.5f, player.position.x + 9.5f, player.position.y - 4f, player.position.y - 5.5f);
    }

    private void SetSpawnLeft(EnemyConfig ec)
    {
        Spawn(ec, player.position.x - 8f, player.position.x - 9.5f, player.position.y - 5.5f, player.position.y + 5.5f);
    }

    private void SetSpawnRight(EnemyConfig ec)
    {
        Spawn(ec, player.position.x + 8f, player.position.x + 9.5f, player.position.y - 5.5f, player.position.y + 5.5f);
    }

    private void Spawn(EnemyConfig ec, float minX, float maxX, float minY, float maxY)
    {
        GameObject enemy = null;
        if (!spawnedAll)
        {
            enemy = gameObject.transform.GetChild(enemyPoolCounter).gameObject;
            enemyPoolCounter++;
            if (enemyPoolCounter >= gameObject.transform.childCount) spawnedAll = true;
        }
        else
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform tempEnemy = gameObject.transform.GetChild(i);
                if (!tempEnemy.gameObject.activeInHierarchy)
                {
                    enemy = tempEnemy.gameObject;
                    break;
                }
            }
            if (enemy == null) enemy = Instantiate(gameObject.transform.GetChild(0), transform.position, Quaternion.identity, gameObject.transform).gameObject;
        }
        enemy.transform.position = new Vector3(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY), 0);
        enemy.GetComponent<EnemyController>().Setup(ec.health, ec.speed, ec.attackDamage, ec.attackSpeed, ec.attackType, ec.projectileSpeed, ec.projectileSprite, ec.sprite, ec.attackRange);
        enemy.SetActive(true);
    }

    public void RespawnTop(GameObject e)
    {
        e.transform.position = new Vector3(UnityEngine.Random.Range(player.position.x - 9.5f, player.position.x + 9.5f), UnityEngine.Random.Range(player.position.y + 4f, player.position.y + 5.5f), 0);
    }

    public void RespawnBot(GameObject e)
    {
        e.transform.position = new Vector3(UnityEngine.Random.Range(player.position.x - 9.5f, player.position.x + 9.5f), UnityEngine.Random.Range(player.position.y - 4f, player.position.y - 5.5f), 0);
    }

    public void RespawnLeft(GameObject e)
    {
        e.transform.position = new Vector3(UnityEngine.Random.Range(player.position.x - 8f, player.position.x - 9.5f), UnityEngine.Random.Range(player.position.y - 5.5f, player.position.y + 5.5f), 0);
    }

    public void RespawnRight(GameObject e)
    {
        e.transform.position = new Vector3(UnityEngine.Random.Range(player.position.x + 8f, player.position.x + 9.5f), UnityEngine.Random.Range(player.position.y - 5.5f, player.position.y + 5.5f), 0);
    }
}
