using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Wave[] waves;
    public Enemy enemy;

    Wave currentWave;
    int currentWaveNumber;

    int enemyRemainingToSpawn;
    int enemiesRemaingAlive;
    float nextSpawnTime;

    public event System.Action<int> OnNewWave;

    void Start()
    {
        NextWave();//start wave
    }

    void Update()
    {
        if(enemyRemainingToSpawn> 0 && Time.time > nextSpawnTime)
        {
            enemyRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawn;

            Enemy spawnedEnemy = Instantiate(enemy, transform.position, Quaternion.identity) as Enemy;
            spawnedEnemy.OnDeath += OnEnemyDeath;//if enemy dies onDeath wil be called
        }
    }

    void OnEnemyDeath()
    {
        //print("enemy Died");
        enemiesRemaingAlive--;
        if(enemiesRemaingAlive == 0)//if no enemy are left alive ten we can start oour next wave
        {
            NextWave();
        }
    }

    void NextWave()
    {
        currentWaveNumber ++;//current wave number increases
        if (currentWaveNumber -1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];//our current wave is current wave mines 1

            enemyRemainingToSpawn = currentWave.enemyCount;
            enemiesRemaingAlive = enemyRemainingToSpawn;//store the amount of enemy from the inspecter into the event

            if(OnNewWave != null)
            {
                OnNewWave(currentWaveNumber);
            }
        }
    }

    [System.Serializable]

    public class Wave//store information about the wave
    {
        public int enemyCount;
        public float timeBetweenSpawn;
    }
}
