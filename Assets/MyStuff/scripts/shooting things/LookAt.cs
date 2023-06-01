using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [HideInInspector]public Transform target = null;
    public bool isEnemy;
    public bool isSniper;

    private TrainManager train;
    private SpawnerOfEnemies spawner;

    private float startTime;
    public float minRange = 0;  // Minimum range for selecting the target
    public float maxRange = 18; // Maximum range for selecting the target

    private void Start()
    {
        train = TrainManager.Instance;
        spawner = SpawnerOfEnemies.Instance;
        startTime = Time.time + 4;
    }
    private void Update()
    {
        if (startTime < Time.time)
        {
            if (!isSniper)
            {
                if (target != null && Vector3.Distance(transform.position, target.position) < maxRange + 1)
                {
                    transform.LookAt(target);
                }
                else if (target != null && Vector3.Distance(transform.position, target.position) > maxRange + 1)
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
                    transform.LookAt(target);
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
            if (isEnemy && train.defensers.Count > 0)
            {
                target = train.defensers[Random.Range(0, train.defensers.Count)].transform;
                if(Vector3.Distance(transform.position, target.position) > maxRange)
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
                    target = spawner.driversOnField[Random.Range(0, spawner.driversOnField.Count)].transform;
                }
            }
        }
    }
}
