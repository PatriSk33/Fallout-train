using UnityEngine;

public class HealthBarLookAt : MonoBehaviour
{
    private Transform cam;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(cam);
    }
}
