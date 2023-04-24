using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    PlayerInput playerInput;

    [SerializeField]
    float speed = 5;

    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionXZ = new Vector3(playerInput.TouchMoveDirection.x, 0, playerInput.TouchMoveDirection.y);
        
        transform.forward = directionXZ;
        
        characterController.SimpleMove(directionXZ * speed);
    }
}
