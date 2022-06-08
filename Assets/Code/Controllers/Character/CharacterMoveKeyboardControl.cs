using UnityEngine;
using System.Collections;

public class CharacterMoveKeyboardControl : CharacterMoveBaseControl
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
        UpdateAction();
    }

    void UpdateAction()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnActionPressed();
        }
    }

    void UpdateDirection()
    {  
        // character moves relative to WASD
        Vector2 newDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            newDirection.y = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newDirection.x = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newDirection.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newDirection.x = 1;
        }
        SetFacingDirection(newDirection);
        SetDirection(newDirection);
    }
}
