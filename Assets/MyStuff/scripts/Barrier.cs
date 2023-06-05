using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [Range(0, 1)][SerializeField] float DamageCutterPercentage = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (other.GetComponent<BulletScript>() != null)
            {
                BulletScript script = other.GetComponent<BulletScript>();

                if (script.isEnemyBullet)
                {
                    script.SetDamage(script.damage * DamageCutterPercentage); //If enemy hits the barrier get it into half by default
                }
                else if(!script.isEnemyBullet)
                { 
                    other.gameObject.SetActive(false); //If player hits the barrier deactivate the bullet
                }
            }
        }
    }
}
