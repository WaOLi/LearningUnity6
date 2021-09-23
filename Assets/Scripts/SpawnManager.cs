using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject boss;
    public int lvl = 1;
    public GameObject[] powerup;
    public GameObject enemyPrefab;
    public GameObject enemy2prefab;
    public float spawnRange = 9.0f;
    public int enemyCount;
    void Start()
    {
        
        SpawnEnemyWave();
        Instantiate(powerup[Random.Range(0,2)], GenerateSpawnPos(), transform.rotation);
        InvokeRepeating("SpawnEarthShakerCharge", 2, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (!(FindObjectsOfType<EnemyBehavior>().Length > 0))
        {
            SpawnEnemyWave();
            if(lvl < 3)
            Instantiate(powerup[Random.Range(0,2)], GenerateSpawnPos(), transform.rotation);
        }
    }
    private Vector3 GenerateSpawnPos()
    {
        float randomZ = Random.Range(-spawnRange, spawnRange);
        float randomX = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(randomX, 0f, randomZ);
        return randomPos;
    }
    void SpawnEnemyWave()
    {
        if (lvl < 3)
        {
            for (int i = 0; i <= lvl; i++)
            {
                int percent = Random.Range(1, 11);
                if (percent < 8)
                {
                    Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
                }
                else
                {
                    Instantiate(enemy2prefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
                }
            }
        }
        else
        {
            boss.SetActive(true);
        }
        lvl++;
    }
    void SpawnEarthShakerCharge()
    {
        Instantiate(powerup[2], GenerateSpawnPos(), powerup[2].transform.rotation);
    }
}
