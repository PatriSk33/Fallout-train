using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public List<GameObject> bulletPrefabs; // List of bullet prefabs
    public List<int> poolSizes; // List of pool sizes corresponding to each bullet prefab

    private Dictionary<GameObject, List<GameObject>> bulletPools;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        bulletPools = new Dictionary<GameObject, List<GameObject>>();

        for (int i = 0; i < bulletPrefabs.Count; i++)
        {
            GameObject bulletPrefab = bulletPrefabs[i];
            int poolSize = poolSizes[i];

            List<GameObject> bulletPool = new List<GameObject>();

            for (int j = 0; j < poolSize; j++)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform);
                bullet.SetActive(false);
                bulletPool.Add(bullet);
            }

            bulletPools.Add(bulletPrefab, bulletPool);
        }
    }

    public GameObject GetBullet(GameObject bulletPrefab)
    {
        List<GameObject> bulletPool = bulletPools[bulletPrefab];

        foreach (GameObject bullet in bulletPool)
        {
            if (bullet != null && !bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }

        // If no inactive bullet is found, create a new one
        GameObject newBullet = Instantiate(bulletPrefab, transform);
        bulletPool.Add(newBullet);
        return newBullet;
    }
}
