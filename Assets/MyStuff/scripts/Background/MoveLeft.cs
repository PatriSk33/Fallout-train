using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 28;
    [SerializeField]private TrainManager playerCS;
    public Vector3 smer;
    void Update()
    {
        if (playerCS.failed == false)
        {
            transform.Translate(smer * Time.deltaTime * speed);
        }

        /*if (transform.position.x < -10 && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }*/

        /*if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 40;
        }
        else
        {
            speed = 28;
        }*/
    }
}