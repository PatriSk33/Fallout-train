using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    public void HasChildObjects()
    {
        if(transform.childCount > 0)
        {
            transform.GetChild(0).position = transform.position + new Vector3(0,1,0);
        }
    }
}
