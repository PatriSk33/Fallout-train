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
    public List<GameObject> enemiesToSpawn, enemiesOnField, vehicleOnField;
    public bool canSpawn = true;
    int maxEnemiesOnField;

    private void Awake()
    {
        Instance = this;
        maxEnemiesOnField = waypoints.Count;

        //Spawn Nakladac
        SpawnNakladac();
    }


    private void Start()
    {
        SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        StartCoroutine(SpawnEnemyVehicle());
    }

    IEnumerator SpawnEnemyVehicle()
    {
        while (!TrainManager.Instance.failed)
        {
            yield return new WaitUntil(() => canSpawn == true);
            GameObject enemyVehicle = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)], spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position, Quaternion.identity);
            enemyVehicle.GetComponent<BasicEnemyVehicle>().destination = waypoints[enemiesOnField.Count].transform;
            vehicleOnField.Add(enemyVehicle);

            // Iterate through each child object
            for (int i = 1; i < enemyVehicle.transform.childCount; i++)
            {
                enemiesOnField.Add(enemyVehicle.transform.GetChild(i).gameObject);
            }

            yield return new WaitForSeconds(1);
            if(vehicleOnField.Count == maxEnemiesOnField)
            {
                canSpawn = false;
            }

        }
    }

    public void SpawnNakladac()
    {
        Instantiate(nakladacPrefab, spawnPointForNakladac.transform.position, Quaternion.identity);
    }
}
