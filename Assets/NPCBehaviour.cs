using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCStates { 
    idle,
    walking,
    running,
    punched,
    dead,
    disabled
}

public class NPCBehaviour : MonoBehaviour
{
    [SerializeField]
    RagdollController ragdollController;

    [SerializeField]
    Animator animator;

    [SerializeField]
    Rigidbody rootRb;

    Collider capsuleCollider;
    Rigidbody rb;

    NPCStates currentState;
    Vector3 pushDirection = Vector3.zero;
    bool  wasHit = false;
    bool dead = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (currentState)
        {
            case NPCStates.idle:
                break;

            case NPCStates.running:
                break;

            case NPCStates.walking:
                break;

            case NPCStates.punched:
                break;

            case NPCStates.dead:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
        if (collision.transform.tag == "Player" && !wasHit)
        {
            wasHit = true;

            capsuleCollider.enabled = false;

            ragdollController.EnableRagdoll();

            RagdollThrow(collision.transform.position);
        }
    }

    void RagdollThrow (Vector3 target)
    {
        pushDirection = transform.position - target;
        pushDirection.Normalize();

        rootRb.AddForce(pushDirection * 1500, ForceMode.Impulse);
    }

    void ResetVariables ()
    {
        wasHit = false;
        dead = false;
        ragdollController.DisableRagdoll();
        pushDirection = Vector3.zero;
    }
}
