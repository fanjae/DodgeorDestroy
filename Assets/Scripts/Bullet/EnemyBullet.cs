using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float lifeTime = 3.0f;
    [SerializeField] private int damage = 1;

    private float lifeTimer;
    public int Damage => damage;

    private void OnEnable()
    {
        lifeTimer = 0f;
    }

    void Update()
    {
        lifeTimer += Time.deltaTime;

        if (lifeTimer >= lifeTime) // 3초 이후 오브젝트 풀이 회수
        {
            Managers.Pool.ReturnPool(this);
            return;
        }

        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerHealth>(out PlayerHealth player))
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
