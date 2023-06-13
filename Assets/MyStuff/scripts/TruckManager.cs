using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    public static TruckManager Instance;

    //Defensers
    public List<GameObject> defensers;

    //Stats
    public float speedOfTime;
    [Tooltip("Amount of items in the truck")]public int inventory;

    private void Awake()
    {
       Instance = this;
    }

    private void Start()
    {
        //Get defensers
    }

    private void Update()
    {
        if (defensers.Count == 0)
        {
            GameplayManager.instance.Lost();
        }
    }
}
