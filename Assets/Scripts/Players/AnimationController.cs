using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private static readonly int movehash = Animator.StringToHash("Move");
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void SetMoveState(int moveState)
    {
        animator.SetInteger(movehash, moveState);
    }

    void Update()
    {
        
    }
}
