using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerOfEnemies : MonoBehaviour
{
    public static SpawnerOfEnemies Instance;
    public GameObject nakladacPrefab, spawnPointForNakladac;
    public List<GameObject> spawnPoints, waypoints;
    public List<GameObject> enemiesToSpawn, enemiesOnField;
    public bool canSpawn = true;
    int maxEnemiesOnField;

    private void Awake()
    {
        Instance = this;
        maxEnemiesOnField = waypoints.Count;

        //Spawn Nakladac
        Instantiate(nakladacPrefab, spawnPointForNakladac.transform.position, Quaternion.identity);
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    IEnumerator SpawnEnemy()
    {
        while (!TrainManager.Instance.failed)
        {
            yield return new WaitUntil(() => canSpawn == true);
            GameObject enemyVehicle = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count - 1)], spawnPoints[Random.Range(0, spawnPoints.Count - 1)].transform.position, Quaternion.identity);
            enemyVehicle.GetComponent<BasicEnemyVehicle>().destination = waypoints[enemiesOnField.Count].transform;
            
            // Iterate through each child object
            for (int i = 1; i < enemyVehicle.transform.childCount; i++)
            {
                enemiesOnField.Add(enemyVehicle.transform.GetChild(i).gameObject);
            }

            yield return new WaitForSeconds(0.5f);
            if(enemiesOnField.Count == maxEnemiesOnField)
            {
                canSpawn = false;
            }

        }
    }
}
