using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private int damage = 1;

    private float lifeTimer; 
    public int Damage => damage; // => 읽기 전용 프로퍼티.
    private void OnEnable()
    {
        lifeTimer = 0f;
    }
    void Update()
    {
        lifeTimer += Time.deltaTime;

        if(lifeTimer >= lifeTime) // 3초 이후 오브젝트 풀이 회수
        {
            Managers.Pool.ReturnPool(this);
            return ;
        }
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}
