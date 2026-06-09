using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // 플레이어 이동 입력값
    public static Vector2 Movement { get; private set; } = Vector2.zero;

    // 공격 버튼 입력 여부
    public static bool IsFire { get; private set; } = false;

    private InputAction moveAction;
    private InputAction fireAction;

    private void Awake()
    {
        // Input System의 액션 가져오기
        moveAction = InputSystem.actions.FindAction("Move");
        fireAction = InputSystem.actions.FindAction("Attack");
    }
    void Update()
    {
        Movement = moveAction.ReadValue<Vector2>();
        IsFire = fireAction.WasPressedThisFrame();
    }
}
