using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] AnimationController animationController;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = InputManager.Movement;
        // 애니메이션 상태 갱신
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    private void UpdateAnimation()
    {
        int moveState = 0;
        if (moveInput.x > 0.01f) // 오른쪽
        {
            moveState = 1;
        }
        else if(moveInput.x < -0.01f) // 왼쪽
        {
            moveState = -1;
        }
        // 애니메이터 상태 전달
        animationController.SetMoveState(moveState);
    }
}
