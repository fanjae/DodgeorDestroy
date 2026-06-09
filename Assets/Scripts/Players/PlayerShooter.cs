using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("발사 위치")]
    [SerializeField] private Transform firePoint;
    [Header("총알 프리팹")]
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float fireDelay = 0.2f;
    private float fireTimer = 0.0f;

    private void Update()
    {
        // 경과시간 누적
        fireTimer += Time.deltaTime;

        if(InputManager.IsFire)
        {
            // 발사
            Fire();
        }
    }
    private void Fire()
    {
        // 발사 대기 시간이 지나지 않았으면 하지 말자
        if(fireTimer < fireDelay)
        {
            return;
        }
        fireTimer = 0.0f;

        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }
}
