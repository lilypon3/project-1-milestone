using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InputToAnimator : MonoBehaviour
{
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
{
    float horizontal = 0;
    float vertical = 0;

    if (Keyboard.current.leftArrowKey.isPressed)
        horizontal = -1;
    if (Keyboard.current.rightArrowKey.isPressed)
        horizontal = 1;

    if (Keyboard.current.downArrowKey.isPressed && !(Mathf.Abs (horizontal) > 0.0f))
        vertical = -1;
    if (Keyboard.current.upArrowKey.isPressed && !(Mathf.Abs (horizontal) > 0.0f))
        vertical = 1;

    animator.SetFloat("horizontal_input", horizontal);
    animator.SetFloat("vertical_input", vertical);

    if (Mathf.Abs(horizontal) == 0.0f && Mathf.Abs(vertical) == 0.0f)
    {
        animator.speed = 0.0f;
    }
    else
    {
        animator.speed = 1.0f;
    }
}
}
