using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [HideInInspector]public Transform target = null;
    [Header("Sniper")]
    public bool isSniper;

    private TruckManager truck;
    private SpawnerOfEnemies spawner;

    private float startTime;
    [Header("Basic Enemy")]
    public bool isEnemy;
    public float minRange = 0;  // Minimum range for selecting the target
    public float maxRange = 18; // Maximum range for selecting the target

    public GameObject parent;

    private void Start()
    {
        truck = TruckManager.Instance;
        spawner = SpawnerOfEnemies.Instance;
        startTime = Time.time + 4;
    }
    private void Update()
    {
        if (startTime < Time.time)
        {
            if (!isSniper)
            {
                if (target != null && Vector3.Distance(parent.transform.position, target.position) < maxRange + 1)
                {
                    transform.position = target.position;
                }
                else if (target != null && Vector3.Distance(parent.transform.position, target.position) > maxRange + 1)
                {
                    target = null;
                }
                else
                {
                    NewTarget();
                }
            }
            else
            {
                if (target != null)
                {
                    transform.position = target.position;
                }
                else
                {
                    NewTarget();
                }
            }
        }
    }

    public void NewTarget()
    {
        if (target == null)
        {
            if (isEnemy && truck.defensers.Count > 0)
            {
                target = truck.defensers[Random.Range(0, truck.defensers.Count)].transform;
                if (Vector3.Distance(transform.position, target.position) > maxRange)
                {
                    target = null;
                }
            }
            else if (!isEnemy && spawner.enemiesOnField.Count > 0)
            {
                if (!isSniper)
                {
                    // Filter the enemies within the specified range
                    List<Transform> enemiesInRange = spawner.enemiesOnField
                        .Where(enemy => Vector3.Distance(transform.position, enemy.transform.position) >= minRange && Vector3.Distance(transform.position, enemy.transform.position) <= maxRange)
                        .Select(enemy => enemy.transform)
                        .ToList();

                    if (enemiesInRange.Count > 0)
                    {
                        int randomIndex = Random.Range(0, enemiesInRange.Count);
                        target = enemiesInRange[randomIndex];
                    }
                    else
                    {
                        target = null;
                    }
                }
                else
                {
                    if (spawner.driversOnField.Count > 0)
                    {
                        // Filter the enemies within the specified range
                        List<Transform> driversInRange = spawner.driversOnField
                            .OrderByDescending(enemy => Vector3.Distance(transform.position, enemy.transform.position))
                            .Select(enemy => enemy.transform)
                            .ToList();

                        if (driversInRange.Count > 0)
                        {
                            target = driversInRange[0];
                        }
                        else
                        {
                            target = null;
                        }
                    }
                }
            }
        }
    }
}
