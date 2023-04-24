using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void EnableRagdoll ()
    {
        animator.enabled = false;
    }

    public void DisableRagdoll()
    {
        animator.enabled = true;
    }
}
