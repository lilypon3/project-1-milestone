using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InputToAnimator : MonoBehaviour
{
    Animator animator;
    int direction = 4; //1 right, 2 left, 3 up, 4 down
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
{

/*
    if (Keyboard.current.zKey.isPressed && direction == 1)
    {
        animator.SetTrigger("attackRight");
    }
    if (Keyboard.current.zKey.isPressed && direction == 2)
    {
        animator.SetTrigger("attackLeft");
    }
    if (Keyboard.current.zKey.isPressed && direction == 3)
    {
        animator.SetTrigger("attackUp");
    }
    if (Keyboard.current.zKey.isPressed && direction == 4)
    {
        animator.SetTrigger("attackDown");
    }
    */


    float horizontal = 0;
    float vertical = 0;

    if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed) {
        horizontal = -1;
        direction = 2;
    }
    if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed) {
        horizontal = 1;
        direction = 1;
    }
    if ((Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed) && !(Mathf.Abs (horizontal) > 0.0f)) {
        vertical = -1;
        direction = 4;
    }
    if ((Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed) && !(Mathf.Abs (horizontal) > 0.0f)) {
        vertical = 1;
        direction = 3;
    }

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
