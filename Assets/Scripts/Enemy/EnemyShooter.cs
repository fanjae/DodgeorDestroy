using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private EnemyBullet bulletPrefab;
    [SerializeField] private float fireInterval = 1.5f;

    private float fireTimer;

    private void Update()
    {
        fireTimer += Time.deltaTime;
        if(fireTimer >= fireInterval)
        {
            Fire();
            fireTimer = 0.0f;
        }
    }
    private void Fire()
    {
        EnemyBullet bullet = Managers.Pool.GetPool(bulletPrefab);
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.identity;
    }
}
