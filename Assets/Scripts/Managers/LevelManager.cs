using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SpawnExp());
        StartCoroutine(LevelOne());
    }

    void CallSpawnWave(int amount, EnemyConfig ec)
    {
        GameManager.instance.enemyManager.SpawnWave(amount, ec);
    }

    IEnumerator SpawnExp()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 20; i++)
        {
            GameObject exp = GameManager.instance.expManager.gameObject.transform.GetChild(GameManager.instance.expManager.GetExpCounter()).gameObject;
            GameManager.instance.expManager.IncrementExpCount();
            exp.GetComponent<ExperienceController>().Setup(5, GameManager.instance.expManager.GetSprite(5));
            switch (i)
            {
                case < 5:
                    exp.transform.position = new Vector3(Random.Range(-1f, -2.5f), Random.Range(-2.5f, 2.5f), 0);
                    break;
                case < 10:
                    exp.transform.position = new Vector3(Random.Range(1f, 2.5f), Random.Range(-2.5f, 2.5f), 0);
                    break;
                case < 15:
                    exp.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(1f, 2.5f), 0);
                    break;
                case < 20:
                    exp.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-1f, -2.5f), 0);
                    break;
            }
            yield return new WaitForSeconds(0.01f);
            exp.SetActive(true);
        }
    }

    IEnumerator LevelOne()
    {
        yield return new WaitForSeconds(5);
        CallSpawnWave(10, GameManager.instance.BasicEnemies[0]);
        yield return new WaitForSeconds(2);
        CallSpawnWave(10, GameManager.instance.BasicEnemies[0]);
        yield return new WaitForSeconds(5);
        CallSpawnWave(10, GameManager.instance.BasicEnemies[0]);
        yield return new WaitForSeconds(5);
        CallSpawnWave(5, GameManager.instance.BasicEnemies[0]);
        CallSpawnWave(5, GameManager.instance.BasicRangedEnemies[0]);
        yield return new WaitForSeconds(15);
        CallSpawnWave(20, GameManager.instance.BasicEnemies[0]);
        yield return new WaitForSeconds(5);
        CallSpawnWave(20, GameManager.instance.BasicEnemies[0]);
        CallSpawnWave(5, GameManager.instance.BasicRangedEnemies[0]);
        yield return new WaitForSeconds(10);
        CallSpawnWave(40, GameManager.instance.BasicEnemies[0]);
        CallSpawnWave(10, GameManager.instance.BasicRangedEnemies[0]);
        yield return new WaitForSeconds(15);
        CallSpawnWave(20, GameManager.instance.BasicEnemies[1]);
        CallSpawnWave(5, GameManager.instance.BasicRangedEnemies[1]);
    }
}
