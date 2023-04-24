using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    Rigidbody rootRb;

    Rigidbody rb;

    Vector3 pushDirection = Vector3.zero;
    bool  wasHit = false;
    bool dead = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rootRb.AddForce(pushDirection * 60, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
        if (collision.transform.tag == "Player" && !wasHit)
        {
            wasHit = true;

            StartCoroutine(RagdollThrow(collision.transform.position));
        }
    }

    IEnumerator RagdollThrow (Vector3 target)
    {
        pushDirection = transform.position - target;
        pushDirection.Normalize();

        animator.enabled = false;

        yield return new WaitForFixedUpdate();

        rootRb.AddForce(pushDirection * 1500, ForceMode.Impulse);
    }

    void ResetVariables ()
    {
        wasHit = false;
        dead = false;
        animator.enabled = true;
        pushDirection = Vector3.zero;
    }
}
