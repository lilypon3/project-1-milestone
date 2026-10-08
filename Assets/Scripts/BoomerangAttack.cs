using UnityEngine;
using UnityEngine.InputSystem;

public class BoomerangAttack : MonoBehaviour, IWeapon
{



    private bool isAttacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;
    
    public Transform launchOffset;
   
   
    public BoomerangBehavior boomerangPrefab;
    private BoomerangBehavior boomerang;
    public bool IsUnlocked { get; set; } = true;
    private GridMovement gridMovement;
    private float startingMoveSpeed;
    private StalfosMovement goriyaMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridMovement = GetComponent<GridMovement>();
        if (gridMovement == null)
        {
            goriyaMovement = GetComponent<StalfosMovement>();
            if (goriyaMovement != null)
            {
                startingMoveSpeed = goriyaMovement.moveSpeed;
            }
            else
            {
                Debug.LogError("BoomerangAttack requires GridMovement or StalfosMovement.", this);
            }
        } else if (gridMovement != null) {
            startingMoveSpeed = gridMovement.moveSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            timer += Time.deltaTime;
            if (timer >= timeToAttack)
            {
                isAttacking = false;
                if (gridMovement != null) {
                    gridMovement.moveSpeed = startingMoveSpeed;
                } else if (gridMovement == null && goriyaMovement != null)
                {
                    goriyaMovement.moveSpeed = startingMoveSpeed;
                }
                timer = 0f;
            }
    
        }
    }

    public void Attack()
    {
        if (isAttacking) return;
        if (boomerang != null) return; 
       // GameObject boomerang = Instantiate(boomerangPrefab, transform.position, transform.rotation);
        //BoomerangBehavior boomerangBehavior = boomerang.GetComponent<BoomerangBehavior>();
        isAttacking = true;
        timer = 0f;
        if (gridMovement != null) {
            gridMovement.moveSpeed = 0f;
        } else if (gridMovement == null && goriyaMovement != null)
        {
            goriyaMovement.moveSpeed = 0f;
        }
        boomerang = Instantiate(boomerangPrefab, launchOffset.position, transform.rotation);
        Vector2 diagonalDirection = GetDiagonalDirection();
        Vector2 lastMoveDirection = gridMovement != null
            ? gridMovement.LastMoveDir
            : goriyaMovement.LastMoveDir;
        Vector2 travelDirection = diagonalDirection == Vector2.zero
            ? lastMoveDirection
            : diagonalDirection;
        boomerang.Init(travelDirection, GetComponent<Collider2D>());
        
            //boomerangBehavior.Init(travelDirection, GetComponent<Collider2D>());
        
        
    }

    private Vector2 GetDiagonalDirection()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return Vector2.zero;

        float horizontal = 0f;
        if (keyboard.leftArrowKey.isPressed || keyboard.aKey.isPressed)
            horizontal = -1f;
        else if (keyboard.rightArrowKey.isPressed || keyboard.dKey.isPressed)
            horizontal = 1f;

        float vertical = 0f;
        if (keyboard.upArrowKey.isPressed || keyboard.wKey.isPressed)
            vertical = 1f;
        else if (keyboard.downArrowKey.isPressed || keyboard.sKey.isPressed)
            vertical = -1f;

        return horizontal != 0f && vertical != 0f
            ? new Vector2(horizontal, vertical).normalized
            : Vector2.zero;
    }
}
