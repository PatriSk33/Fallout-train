using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public void Death(bool isEnemy, GameObject o)
    {
        //Death animation
        Debug.Log("Death");

        if (isEnemy)
        {
            SpawnerOfEnemies.Instance.enemiesOnField.Remove(o);
            transform.parent.GetComponent<BasicEnemyVehicle>().enemiesOnVehicle--;
        }
        else
        {
            TruckManager.Instance.defensers.Remove(o);
        }
        Destroy(o);
    }

    public void Shoot(float damage, float fireRate, Transform t, GameObject bulletPrefab, float criticalChange, float criticalMultiplier)
    {
        GameObject bullet = BulletPool.Instance.GetBullet(bulletPrefab);
        bullet.transform.position = t.position;     //Hlaven zbrane
        bullet.transform.rotation = t.rotation;     //Hlaven zbrane

        BulletScript bulletScript = bullet.GetComponent<BulletScript>();

        // Check if critical hit occurs
        float randomValue = Random.value; // Generate a random number between 0 and 1
        if (randomValue <= criticalChange)
        {
            bulletScript.SetDamage(damage + (damage * criticalMultiplier));
        }
        else
        {
            bulletScript.SetDamage(damage);
        }
        bulletScript.Fire();  //Aditionla effects
    }
}
