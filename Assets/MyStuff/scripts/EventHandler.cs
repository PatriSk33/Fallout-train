using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            case 0:  // Slowdown the train -- spawn new enemy
                SpawnerOfEnemies.Instance.canSpawn = true;
                SpawnerOfEnemies.Instance.SpawnEnemy();
                break;
            case 1:  //Speed Up the train -- delete one enemy vehicle
                SpawnerOfEnemies.Instance.vehicleOnField[0].GetComponent<BasicEnemyVehicle>().Death(false);
                break;

        }
    }
}
