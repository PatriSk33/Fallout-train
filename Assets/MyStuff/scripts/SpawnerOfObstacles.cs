using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOfObstacles : MonoBehaviour
{
    public List<GameObject> smallObstacles, mediumObstacles, bigObstacles;
    public List<GameObject> waypoints;
    private float repeatTime = 20;
    private float startTime = 20;

    private void Start()
    {
        InvokeRepeating("SpawnObstalce", startTime, repeatTime);
    }

    void SpawnObstalce()
    {
        int obsIndex = 0;
        int listIndex = Random.Range(0, 3); // Amount of different types of obstacle lists
        int waypointIndex = Random.Range(0, waypoints.Count);

        switch (listIndex)
        {
            case 0:
                obsIndex = Random.Range(0, smallObstacles.Count);
                Instantiate(smallObstacles[obsIndex], waypoints[waypointIndex].transform.position, Quaternion.identity);
                break;
            case 1:
                obsIndex = Random.Range(0, smallObstacles.Count);
                Instantiate(mediumObstacles[obsIndex], waypoints[waypointIndex].transform.position, Quaternion.identity);
                break;
            case 2:
                obsIndex = Random.Range(0, smallObstacles.Count);
                Instantiate(bigObstacles[obsIndex], waypoints[waypointIndex].transform.position, Quaternion.identity);
                break;
            default:
                Debug.Log("Wrong amount of different types of obstacles!");
                break;
        }
    }
}