using UnityEngine;
using UnityEngine.InputSystem;

public class SwordAttack : MonoBehaviour
{
    private GameObject AttackArea = default;
    private bool isAttacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;
    private GridMovement gridMovement;
    private SwordAnimation swordAnimation;
    private float startingMoveSpeed;
    public ProjectileBehavior projectilePrefab; 
    public Transform launchOffset;
    public Health health;

    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //rb = GetComponent<Rigidbody2D> ();
        AttackArea = transform.GetChild(0).gameObject;
        gridMovement = GetComponent<GridMovement>();
        swordAnimation = GetComponent<SwordAnimation>();
        health = GetComponent<Health>();
        startingMoveSpeed = gridMovement.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            Attack();
            //rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }
        if (isAttacking) {
            timer += Time.deltaTime;
            if (timer >= timeToAttack)
            {
                isAttacking = false;
                gridMovement.moveSpeed = startingMoveSpeed;
                if (swordAnimation != null) swordAnimation.SetAttacking(false);
                AttackArea.SetActive(false);
                timer = 0f;
            }
        }

    
    }

    private void Attack()
    {
        if (isAttacking) return;
        if (health.current_health == 3)
        {
            Instantiate(projectilePrefab, launchOffset.position, transform.rotation);
        }
        isAttacking = true;
        timer = 0f;
        gridMovement.moveSpeed = 0f;
        if (swordAnimation != null) swordAnimation.SetAttacking(true);
        AttackArea.SetActive(isAttacking);
    }

    
}
