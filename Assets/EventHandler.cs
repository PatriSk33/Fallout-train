using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EventHandler : MonoBehaviour
{
    public int index;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartEvent(index);
        }
    }

    void StartEvent(int index)
    {
        switch (index)
        {
            case 0:  // Slowdown aka spawn new enemy
                SpawnerOfEnemies.Instance.SpawnEnemy();
                break;
            case 1:  //Speed Up aka delete one enemy vehicle
                SpawnerOfEnemies.Instance.vehicleOnField[0].GetComponent<BasicEnemyVehicle>().agent.destination = transform.position + new Vector3(0, 0, 30);
                break;

        }
    }
}
