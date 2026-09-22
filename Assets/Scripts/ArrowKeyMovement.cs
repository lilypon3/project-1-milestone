using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ArrowKeyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float movement_speed = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 current_input = GetInput ();
        rb.linearVelocity = current_input * movement_speed;
    }

    Vector2 GetInput()
    {
        float horizontal_input = 0;
        float vertical_input = 0;

        if (Keyboard.current.leftArrowKey.isPressed)
            horizontal_input = -1;
        if (Keyboard.current.rightArrowKey.isPressed)
            horizontal_input = 1;
        if (Keyboard.current.upArrowKey.isPressed && !(Mathf.Abs (horizontal_input) > 0.0f))
            vertical_input = 1;
        if (Keyboard.current.downArrowKey.isPressed && !(Mathf.Abs (horizontal_input) > 0.0f))
            vertical_input = -1;
        return new Vector2 (horizontal_input, vertical_input);
    }
}
