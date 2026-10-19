using UnityEngine;
using UnityEngine.InputSystem;

public class ShieldBlock : MonoBehaviour, IWeapon
{
    private bool isBlocking = false;
    private GridMovement gridMovement;
    public Transform shieldOffset;

    
    private float startingMoveSpeed;
    public ShieldBehavior shieldPrefab;
    private ShieldBehavior shield;
   
    public Health health;

    public bool IsUnlocked { get; set; } = false; // must be picked up in-game, or cheated in

    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //rb = GetComponent<Rigidbody2D> ();
        gridMovement = GetComponent<GridMovement>();
        health = GetComponent<Health>();
        startingMoveSpeed = gridMovement.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlocking && (Keyboard.current == null || !Keyboard.current.zKey.isPressed))
        {
            isBlocking = false;
            health.SetShielding(false);
            gridMovement.moveSpeed = startingMoveSpeed;

            if (shield != null)
            {
                shield.Hide();
                shield = null;
            }
        }
    }

    public void Attack()
    {
        if (isBlocking) return;
        
        isBlocking = true;
        health.SetShielding(true);
        if (shieldPrefab != null)
        {
            shield = Instantiate(shieldPrefab, shieldOffset.position, transform.rotation);
            shield.Show();
        }
        gridMovement.moveSpeed = 0f;
    }

    
}
