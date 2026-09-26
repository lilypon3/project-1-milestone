using UnityEngine;
using UnityEngine.InputSystem;

public class BowAttack : MonoBehaviour
{
    private bool isAttacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;
    private GridMovement gridMovement;

    private float startingMoveSpeed;
    public ProjectileBehavior projectilePrefab; 
    public Transform launchOffset;
    public Health health;
    public Inventory inventory;

    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //rb = GetComponent<Rigidbody2D> ();
        
        gridMovement = GetComponent<GridMovement>();
      
        health = GetComponent<Health>();
        inventory = GetComponent<Inventory>();
        startingMoveSpeed = gridMovement.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            if (inventory.GetRupees() < 1)
            {
                return;
            }
            inventory.RemoveRupee(1);
            Attack();
            //rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }
        if (isAttacking) {
            timer += Time.deltaTime;
            if (timer >= timeToAttack)
            {
                isAttacking = false;
                gridMovement.moveSpeed = startingMoveSpeed;
                timer = 0f;
            }
        }

    
    }

    private void Attack()
    {
        if (isAttacking) return;

        if (health.current_health == 3)
        {
            ProjectileBehavior projectile = Instantiate(projectilePrefab, launchOffset.position, transform.rotation);
            projectile.Init(gridMovement.LastMoveDir, GetComponent<Collider2D>());
        }
        
        isAttacking = true;
        timer = 0f;
        gridMovement.moveSpeed = 0f;
       
       
    }

    
}
