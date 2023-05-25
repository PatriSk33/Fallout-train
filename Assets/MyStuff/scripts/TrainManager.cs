using PathCreation.Examples;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainManager : MonoBehaviour
{
    public static TrainManager Instance;

    //Vagons
    public List<vagon> vagonsToAttack;

    //Fail
    public bool failed;
    public Text failText;

    //Defensers
    public List<GameObject> defensers;

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
        if (vagonsToAttack.Count == 0) {
            failed = true;
            failText.text = "Failed!";
        }
    }
}
