using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LevelOne());
    }

    IEnumerator LevelOne()
    {
        yield return new WaitForSeconds(3);
        GameManager.instance.enemyManager.SpawnWave(1, GameManager.instance.BasicRangedEnemies[0]);
        /*
        yield return new WaitForSeconds(5);
        GameManager.instance.enemyManager.SpawnWave(10, GameManager.instance.BasicRangedEnemies[0]);
        yield return new WaitForSeconds(5);
        GameManager.instance.enemyManager.SpawnWave(10, GameManager.instance.BasicEnemies[0]);
        yield return new WaitForSeconds(5);
        GameManager.instance.enemyManager.SpawnWave(5, GameManager.instance.BasicEnemies[0]);
        GameManager.instance.enemyManager.SpawnWave(5, GameManager.instance.BasicRangedEnemies[0]);
        yield return new WaitForSeconds(15);
        GameManager.instance.enemyManager.SpawnWave(20, GameManager.instance.BasicEnemies[0]);
        yield return new WaitForSeconds(30);
        GameManager.instance.enemyManager.SpawnWave(20, GameManager.instance.BasicEnemies[0]);
        GameManager.instance.enemyManager.SpawnWave(5, GameManager.instance.BasicRangedEnemies[0]);
        yield return new WaitForSeconds(30);
        GameManager.instance.enemyManager.SpawnWave(40, GameManager.instance.BasicEnemies[0]);
        GameManager.instance.enemyManager.SpawnWave(10, GameManager.instance.BasicRangedEnemies[0]);
        yield return new WaitForSeconds(60);
        GameManager.instance.enemyManager.SpawnWave(20, GameManager.instance.BasicEnemies[1]);
        GameManager.instance.enemyManager.SpawnWave(5, GameManager.instance.BasicRangedEnemies[1]);
        */
    }
}
