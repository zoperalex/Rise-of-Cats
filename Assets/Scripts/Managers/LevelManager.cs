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
        GameManager.instance.enemyManager.SpawnWave(10, GameManager.instance.BasicEnemies[0]);
        yield return new WaitForSeconds(27);
        GameManager.instance.enemyManager.SpawnWave(20, GameManager.instance.BasicEnemies[0]);
        yield return new WaitForSeconds(60);
        GameManager.instance.enemyManager.SpawnWave(40, GameManager.instance.BasicEnemies[0]);
        yield return new WaitForSeconds(90);
        GameManager.instance.enemyManager.SpawnWave(10, GameManager.instance.BasicEnemies[1]);
        yield return null;
    }
}
