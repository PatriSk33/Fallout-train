using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WaypointList
{
    public List<GameObject> waypoints;
}

public class SpawnerOfEnemies : MonoBehaviour
{
    public static SpawnerOfEnemies Instance;
    public GameObject nakladacPrefab, spawnPointForNakladac;
    public List<GameObject> spawnPoints;
    public List<WaypointList> allWaypoints;
    public List<GameObject> enemiesToSpawn, enemiesOnField, vehicleOnField, driversOnField;
    public bool canSpawn = true;
    int maxEnemiesOnField;

    // Keep track of used waypoints for each spawn point
    private List<List<Transform>> usedWaypoints;

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        maxEnemiesOnField = TrainManager.Instance.vagonsToAttack.Count; // Max enemies == amount of vagons

        //Spawn Nakladac and EnemyVehicle
        SpawnNakladac();
        SpawnEnemy();

        // Initialize usedWaypoints list
        usedWaypoints = new List<List<Transform>>();
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            usedWaypoints.Add(new List<Transform>());
        }
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

            int spawnpointIndex = Random.Range(0, spawnPoints.Count);
            GameObject enemyVehicle = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)], spawnPoints[spawnpointIndex].transform.position - new Vector3(0,1.12f,0), Quaternion.identity);

            // Get the random destination waypoint from the line it spawns in
            Transform randomWaypoint = GetRandomUnusedWaypoint(spawnpointIndex);
            if (randomWaypoint != null)
            {
                enemyVehicle.GetComponent<BasicEnemyVehicle>().destination = randomWaypoint;
                enemyVehicle.GetComponent<BasicEnemyVehicle>().spawnpointIndex = spawnpointIndex;
                usedWaypoints[spawnpointIndex].Add(randomWaypoint);
            }
            else
            {
                // No available waypoints, destroy the enemy vehicle
                Destroy(enemyVehicle);
                continue;
            }

            vehicleOnField.Add(enemyVehicle);

            // Get all attackers from the vehicle into the list of all attackers
            for (int i = 1; i < enemyVehicle.transform.childCount; i++)
            {
                if (i == 1)
                {
                    driversOnField.Add(enemyVehicle.transform.GetChild(i).gameObject);
                }
                else
                {
                    enemiesOnField.Add(enemyVehicle.transform.GetChild(i).gameObject);
                    enemyVehicle.GetComponent<BasicEnemyVehicle>().enemiesOnVehicle++;
                }
            }

            yield return new WaitForSeconds(1);
            if (vehicleOnField.Count == maxEnemiesOnField) // End spawning
            {
                canSpawn = false;
            }
        }
    }
    
    public void RemoveVehicle(GameObject vehicle)
    {
        int spawnpointIndex = vehicle.GetComponent<BasicEnemyVehicle>().spawnpointIndex;
        Transform waypoint = vehicle.GetComponent<BasicEnemyVehicle>().destination;

        // Release the waypoint
        ReleaseWaypoint(spawnpointIndex, waypoint);

        // Remove the vehicle passagers from the lists
        for (int i = 1; i < vehicle.transform.childCount; i++)
        {
            enemiesOnField.Remove(vehicle.transform.GetChild(i).gameObject);
        }

        vehicleOnField.Remove(vehicle);

        if (vehicleOnField.Count == 0)
        {
            canSpawn = true;
        }
    }

    #region Getting Waypoint
    void ReleaseWaypoint(int spawnpointIndex, Transform waypoint)
    {
        usedWaypoints[spawnpointIndex].Remove(waypoint);
    }

    Transform GetRandomUnusedWaypoint(int spawnpointIndex)
    {
        List<Transform> unusedWaypoints = new List<Transform>();
        foreach (var waypoint in allWaypoints[spawnpointIndex].waypoints)
        {
            if (!usedWaypoints[spawnpointIndex].Contains(waypoint.transform))
            {
                unusedWaypoints.Add(waypoint.transform);
            }
        }

        if (unusedWaypoints.Count > 0)
        {
            // Check if any waypoint was previously selected
            bool waypointSelected = usedWaypoints[spawnpointIndex].Count > 0;

            // Sort the unused waypoints based on their distance to the spawn point in ascending order
            unusedWaypoints.Sort((w1, w2) =>
            {
                float distance1 = Vector3.Distance(spawnPoints[spawnpointIndex].transform.position, w1.position);
                float distance2 = Vector3.Distance(spawnPoints[spawnpointIndex].transform.position, w2.position);
                return distance1.CompareTo(distance2); // Sort in ascending order
            });

            int randomIndex;
            if (waypointSelected)
            {
                // Get the last selected waypoint
                Transform lastWaypoint = usedWaypoints[spawnpointIndex][usedWaypoints[spawnpointIndex].Count - 1];

                // Calculate the range for waypoints that are closer than the last selected waypoint
                float distanceThreshold = Vector3.Distance(spawnPoints[spawnpointIndex].transform.position, lastWaypoint.position);
                List<Transform> closerWaypoints = unusedWaypoints.FindAll(w =>
                    Vector3.Distance(spawnPoints[spawnpointIndex].transform.position, w.position) < distanceThreshold);

                if (closerWaypoints.Count > 0)
                {
                    // Choose a random waypoint from the closer waypoints
                    randomIndex = Mathf.FloorToInt(Random.Range(0f, closerWaypoints.Count));
                    return closerWaypoints[randomIndex];
                }
            }

            if (usedWaypoints[spawnpointIndex].Count == 0)
            {
                // Choose a random waypoint from all unused waypoints
                randomIndex = Mathf.FloorToInt(Random.Range(0f, unusedWaypoints.Count));
                return unusedWaypoints[randomIndex];
            }
        }

        return null;
    }

    #endregion

    public void SpawnNakladac()
    {
        Instantiate(nakladacPrefab, spawnPointForNakladac.transform.position - new Vector3(0, 1.12f, 0), Quaternion.identity);
    }
}
