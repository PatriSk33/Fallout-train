using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOfEvents : MonoBehaviour
{
    public List<GameObject> events;
    private float repeatTime = 30;
    private void Start()
    {
        InvokeRepeating("SpawnEvent", 15, repeatTime);
    }

    void SpawnEvent()
    {
        int index = Random.Range(0, events.Count);
        Instantiate(events[index], transform.position,Quaternion.identity);
    }
}
