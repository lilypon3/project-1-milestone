using UnityEngine;

public class BoomerangAttack : MonoBehaviour, IWeapon
{



    private bool isAttacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;
    
    public Transform launchOffset;
   
   
    public BoomerangBehavior boomerangPrefab;
    public bool IsUnlocked { get; set; } = true;
    private GridMovement gridMovement;
    private float startingMoveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridMovement = GetComponent<GridMovement>();
        startingMoveSpeed = gridMovement.moveSpeed;
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
                gridMovement.moveSpeed = startingMoveSpeed;
                timer = 0f;
            }
    
        }
    }

    public void Attack()
    {
        if (isAttacking) return;

       // GameObject boomerang = Instantiate(boomerangPrefab, transform.position, transform.rotation);
        //BoomerangBehavior boomerangBehavior = boomerang.GetComponent<BoomerangBehavior>();
        isAttacking = true;
        timer = 0f;
        gridMovement.moveSpeed = 0f;
        BoomerangBehavior boomerang = Instantiate(boomerangPrefab, launchOffset.position, transform.rotation);
        boomerang.Init(gridMovement.LastMoveDir, GetComponent<Collider2D>());
        
            //boomerangBehavior.Init(travelDirection, GetComponent<Collider2D>());
        
        
    }
}
