using UnityEngine;

public class PoolPreLoader : MonoBehaviour
{
    [Header("프리팹")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private EnemyBullet enemyBulletPrefab;
    [SerializeField] private Enemy enemyPrefab;

    [Header("미리 생성할 개수")]
    [SerializeField] private int bulletCount = 30;
    [SerializeField] private int enemyBulletCount = 30;
    [SerializeField] private int enemyCount = 10;


    void Start()
    {
        Managers.Pool.PreloadPool(bulletPrefab, bulletCount);
        Managers.Pool.PreloadPool(enemyBulletPrefab, enemyBulletCount);
        Managers.Pool.PreloadPool(enemyPrefab, enemyCount);
    }
}
