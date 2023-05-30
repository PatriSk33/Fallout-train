using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{
    public Transform object1;
    public Transform object2;

    void Update()
    {
        if (object1 != null && object2 != null)
        {
            // Calculate the distance between the two objects
            float distance = Vector3.Distance(object1.position, object2.position);

            // Output the distance to the console
            Debug.Log("Distance between object1 and object2: " + distance);
        }
    }
}