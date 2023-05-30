using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vagon : MonoBehaviour
{
    public int inventory = 10;

    private void Update()
    {
        if(inventory <= 0)
        {
            TrainManager.Instance.vagonsToAttack.Remove(this);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
