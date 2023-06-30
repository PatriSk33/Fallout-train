using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform cam;

    private void Awake()
    {
        if (cam == null) cam = Camera.main.transform; 
    }

    void LateUpdate()
    {
        transform.LookAt(cam);
    }
}
