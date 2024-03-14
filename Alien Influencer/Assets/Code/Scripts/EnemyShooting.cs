using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootingInterval = 2f; // Time between shots
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootingInterval)
        {
            Shoot();
            timer = 0;
        }
    }

    void Shoot()
    {
        // Adjust bulletSpawnPosition based on your bullet's starting position
        Vector3 bulletSpawnPosition = transform.position + new Vector3(0, 5, 0);
        Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);
    }
}
