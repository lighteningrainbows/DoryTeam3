using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetDirection(Vector2Int dir)
    {
        animator.SetFloat("MoveX", dir.x);
        animator.SetFloat("MoveY", dir.y);
    }

    public void SetMoving(bool moving)
    {
        animator.SetBool("IsMoving", moving);
    }
}