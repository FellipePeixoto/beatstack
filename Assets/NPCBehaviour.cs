using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCStates { 
    // NPC IS TALKING WITH ANOTHER NPC
    idle,
    // NPC IS WALKING TALKING WITH THE PHONE
    walking,
    // NPC IS RUNNING AWAY FROM PLAYER
    running,
    // NPC WAS PUNCHED 
    punched,
    // AFTE BEING PUNCHED NPC IS DEAD AND READY TO BE STACKED
    dead,
    // NPC WAS STACKED BY THE PLAYER
    stacked,
    // NPC IS DISABLED AND READY TO BE RESPAWNED
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

    [SerializeField]
    [Tooltip("How much seconds later the NPC will be avaible to stack")]
    float delayToBeDead = 0.8f;

    Collider capsuleCollider;
    Rigidbody rb;

    NPCStates currentState;
    Vector3 pushDirection = Vector3.zero;

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
        switch (currentState)
        {
            case NPCStates.dead:

                break;

            default:
                if (collision.transform.tag == "Player" && currentState != NPCStates.punched)
                {
                    currentState = NPCStates.punched;

                    capsuleCollider.enabled = false;

                    ragdollController.EnableRagdoll();

                    StartCoroutine(RagdollThrow(collision.transform.position, 2000));
                }

                break;
        }        
    }

    IEnumerator RagdollThrow (Vector3 target, float force)
    {
        pushDirection = transform.position - target;
        pushDirection.Normalize();

        rootRb.AddForce(pushDirection * force, ForceMode.Impulse);

        yield return new WaitForSeconds(delayToBeDead);

        currentState = NPCStates.dead;
    }

    // USEFUL FOR REUSE OF THE GAME OBJECT
    void ResetVariables ()
    {
        currentState = NPCStates.disabled;
        ragdollController.DisableRagdoll();
        pushDirection = Vector3.zero;
    }

    public void Collect(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("TRIGGER!");
        }
    }
}
