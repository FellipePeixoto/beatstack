using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    Vector3 targetOffset = new Vector3(0, 12, -12.5f);

    private void LateUpdate()
    {
        transform.position = targetOffset + target.position;
    }
}
