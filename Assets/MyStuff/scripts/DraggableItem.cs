using UnityEngine;
using UnityEngine.EventSystems;
public class DraggableItem : MonoBehaviour
{
    public LayerMask targetLayer;
    float distance = 13.5f;
    [HideInInspector]public bool isDragging = false;
    GameObject targetObject;

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (targetObject != null)
        {
            transform.SetParent(targetObject.transform);
            targetObject.GetComponent<InventorySlot>().HasChildObjects();
        }
        else
        {
            transform.position = transform.parent.position + new Vector3(0,1,0);
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            transform.position = objPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDragging && targetLayer == (targetLayer | (1 << other.gameObject.layer)))
        {
            targetObject = null;
            targetObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isDragging && targetLayer == (targetLayer | (1 << other.gameObject.layer)))
        {
            targetObject = null;
        }
    }
}

