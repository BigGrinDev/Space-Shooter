using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] PowerUps;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private bool isSpawning = true;
    [SerializeField]
    private bool isSpawningPowerUp = true;


    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());

        StartCoroutine(SpawnPowerUpRoutine());
    }

    
    //spawn an enemy every 5 seconds
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (isSpawning)
        {
            float Random_x = Random.Range(-8, 8);
            Vector3 spawnPos = new Vector3(Random_x, 6, 0);
            GameObject enemy = Instantiate(_enemyPrefab,spawnPos,Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5);
        }
        
    }

    //spawn powerUp
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (isSpawningPowerUp)
        {
            float Random_xpos = Random.Range(-8f, 8f);
            int RandomPowerup = Random.Range(0, 3);
            Instantiate(PowerUps[RandomPowerup], new Vector3(Random_xpos, 6, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }

    public void OnPlayerDeath()
    {
        isSpawning = false;
        isSpawningPowerUp = false;
    }


   

    
    
}
