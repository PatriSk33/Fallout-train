using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public void Death(bool isEnemy, GameObject shooterObject)
    {
        //Death animation
        Debug.Log("Death");

        if (isEnemy)
        {
            SpawnerOfEnemies.Instance.enemiesOnField.Remove(shooterObject);
            transform.parent.GetComponent<BasicEnemyVehicle>().enemiesOnVehicle--;
        }
        else
        {
            TruckManager.Instance.RemoveDefender(shooterObject);
        }
        Destroy(shooterObject);
    }

    public void Shoot(float damage, Transform hlaven, GameObject bulletPrefab, float criticalChange, float criticalMultiplier, Transform lookAt)
    {
        GameObject bullet = BulletPool.Instance.GetBullet(bulletPrefab);
        bullet.transform.position = hlaven.position;     //Hlaven zbrane
        if (Random.value < 0.9)
        {
            bullet.transform.LookAt(lookAt);     // look at the target
        }
        else
        {
            bullet.transform.LookAt(lookAt.position - new Vector3(0, 1.5f, 0));     // look at the target vehicle
        }

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
