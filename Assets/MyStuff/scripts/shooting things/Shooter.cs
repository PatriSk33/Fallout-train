using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public void Death()
    {
        //Death animation
        Debug.Log("Death");
    }

    public void Shoot(float damage, float fireRate, Transform t, GameObject bulletPrefab)
    {
        GameObject bullet = BulletPool.Instance.GetBullet(bulletPrefab);
        bullet.transform.position = t.position;
        bullet.transform.rotation = t.rotation;

        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        bulletScript.SetDamage(damage);
        bulletScript.Fire();
    }
}
