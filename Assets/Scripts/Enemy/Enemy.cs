using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;

    [Header("Move")]
    [SerializeField] private PatternType patternType = PatternType.Straight;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float zigZagPower = 2.0f;
    [SerializeField] private float zigZagFrequency = 3.0f;
    [SerializeField] private float sideMoveSpeed = 1.5f;

    [Header("HP")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float destoryY = -6.0f; // Y값이 이 아래면 제거
    [SerializeField] private EnemyUI hpUI;

    private int currentHealth; 
    private Rigidbody2D rb;
    private float startX;
    private float lifeTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable() // 풀에서 다시 꺼낼때마다 초기화되도록 변경
    {
        currentHealth = maxHealth;
        startX = transform.position.x;
        lifeTimer = 0f;

        if (hpUI != null)
        {
            hpUI.Initialize(maxHealth);
        }
    }

    private void FixedUpdate()
    {
        lifeTimer += Time.fixedDeltaTime;

        Vector2 velocity = Vector2.down * moveSpeed;

        switch (patternType)
        {
            // 아래로 직선 이동 하는 방식
            case PatternType.Straight:
                
                velocity = Vector2.down * moveSpeed;
                break;

            // 좌우로 흔들리며 이동하는 방식
            case PatternType.ZigZag:
                
                float x = Mathf.Sin(Time.time * zigZagFrequency) * zigZagPower;
                velocity = new Vector2(x, -moveSpeed);
                break;

            // 시작 위치에 따라 반대쪽 방향으로 이동
            case PatternType.SideOnly:
                
                float direction = startX < 0 ? 1f : -1f;
                velocity = new Vector2(direction * sideMoveSpeed, -moveSpeed);
                break;

            // 생존 시간을 기준으로 사인파 형태 이동
            case PatternType.SineWave:
                velocity = new Vector2(Mathf.Sin(lifeTimer * zigZagFrequency) * zigZagPower,-moveSpeed);
                break;

            // 일정 간격으로 이동과 정지를 반복
            case PatternType.StopAndGo:
                bool isMoving = Mathf.FloorToInt(lifeTimer * 2f) % 2 == 0;
                velocity = isMoving ? Vector2.down * moveSpeed : Vector2.zero;
                break;

            // 시간이 지날수록 아래 방향 속도 증가
            case PatternType.Accelerate:
                velocity = Vector2.down * (moveSpeed + lifeTimer);
                break;

            // 대각선 방향으로 이동
            case PatternType.Diagonal:
                velocity = new Vector2(sideMoveSpeed, -moveSpeed);
                break;
        }

        // 계산 속도를 Rigidbody에 적용한다.
        rb.linearVelocity = velocity;
    }

    private void Update()
    {
        // 화면 아래로 간 적 제거
        if (transform.position.y < destoryY)
        {
            Managers.Pool.ReturnPool(this);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (hpUI != null)
        {
            hpUI.SetHp(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        scoreManager.AddScore(1);
        Managers.Pool.ReturnPool(this);
    }

    public void SetPattern(PatternType pattern)
    {
        patternType = pattern;
    }
    public void SetScoreManager(ScoreManager manager)
    {
        // 점수 관리자 설정
        scoreManager = manager;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 총알과 부딪히면 데미지 적용
        if (collision.TryGetComponent<Bullet>(out Bullet bullet))
        {
            TakeDamage(bullet.Damage);
            Managers.Pool.ReturnPool(bullet);
        }
    }
}