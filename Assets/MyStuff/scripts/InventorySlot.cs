using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    private void Start()
    {
        HasChildObjects();
    }
    public void HasChildObjects()
    {
        if(transform.childCount > 0)
        {
            transform.GetChild(0).position = transform.position + new Vector3(0,1.7f,0);
        }
    }
}
