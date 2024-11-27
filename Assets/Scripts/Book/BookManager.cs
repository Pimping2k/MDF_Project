using Containers;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void MoveOut()
    {
        animator.SetBool(AnimationStatesContainer.ISMOVINGOUT, true);
    }

    public void MoveIn()
    {
        animator.SetBool(AnimationStatesContainer.ISMOVINGOUT, false);
    }
}