using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    float deadZoneSize = 30;

    [SerializeField]
    float stickRadius = 130;

    Vector2 touchCenter = Vector2.positiveInfinity;
    Vector2 touchMoveDirection = Vector2.zero;

    [SerializeField]
    Transform center;

    [SerializeField]
    Transform stick;


    public Vector2 TouchCenter { get { return touchCenter; } }
    
    public Vector2 TouchMoveDirection { get { return touchMoveDirection; } }

    TextMeshProUGUI tmpRop;

    private void Awake()
    {
        HideStick();

        // DEBUGGING PURPOSES
        tmpRop = GameObject.FindGameObjectWithTag("DEBUG").GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ON PLAYER TOUCH FOR THE FIRST TIME ON SCREEN
        if (Input.GetMouseButtonDown(0) && touchCenter.Equals(Vector2.positiveInfinity))
        {
            ShowStick();
            touchCenter = Input.mousePosition;
            touchMoveDirection = Vector2.zero;
        }

        // ON PLAYER KEEP TOUCH PRESSED
        if (Input.GetMouseButton(0) && !touchCenter.Equals(Vector2.positiveInfinity))
        {
            float touchMovedDistance = Vector2.Distance(touchCenter, Input.mousePosition);

            // TRUE WHEN THE TOUCH IS OUT OF BOUNDS, OUT OF THE SPECIFIED RADIUS 
            bool isTouchOutOfRadius = touchMovedDistance > stickRadius;

            Vector2 touchMoveBoundedPosition = isTouchOutOfRadius ?  FarestPoint(touchCenter, Input.mousePosition, touchMovedDistance, stickRadius) : Input.mousePosition;

            // CONTROLS THE VELOCITY BETWEEN MAX AND MIN
            float movementMultiplyer = touchMovedDistance / stickRadius;
            
            Vector2 direction = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - touchCenter;
            touchMoveDirection = isTouchOutOfRadius ? direction.normalized : direction.normalized.normalized * movementMultiplyer;

            bool isDeadZone = Vector2.Distance(touchCenter, Input.mousePosition) <= deadZoneSize;

            UpdateStickPosition(isDeadZone ? touchCenter : touchMoveBoundedPosition);
            touchMoveDirection = isDeadZone ? Vector2.zero : touchMoveDirection;
        }

        // ON PLAYER RELEASE THE TOUCH
        if (Input.GetMouseButtonUp(0))
        {
            HideStick();
            touchCenter = Vector2.positiveInfinity;
            touchMoveDirection = Vector2.zero;
        }

        // DEBUG VALUES
        tmpRop.text = "TOUCH DIR: " + touchMoveDirection.ToString() + "\nTOUCH CENTER: "+ touchCenter;
    }

    void UpdateStickPosition(Vector2 newPosition)
    {
        stick.transform.position = newPosition;
    }

    void ShowStick()
    {
        stick.gameObject.SetActive(true);
    }

    void HideStick()
    {
        stick.gameObject.SetActive(false);
    }

    Vector2 FarestPoint (Vector2 center, Vector2 target, float currentDistance, float maxDistance)
    {
        float diff = maxDistance / currentDistance;
        return Vector3.Lerp(center, target, diff);
    }
}
