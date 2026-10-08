using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BladeTrap : MonoBehaviour
{

    [SerializeField] private float speed = 5.0f;
    public List<Transform> traps;
    // hi

    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void Update()
    {
        bool hit = Physics2D.Raycast(transform.position, Vector2.down, 5f, LayerMask.GetMask("Player"));
        if (hit)
        {
            Move(direction);
            Debug.Log("BladeTrap hit the player!");
            // You can add logic here to damage the player or trigger an event
        }
        Debug.DrawRay(transform.position, Vector2.down * 5f, Color.red);
    }

    void Move(direction)
    {
        transform.position += direction * speed * Time.deltaTime;
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("BladeTrap"))
        {
            // Reverse the direction of the trap when it hits a wall
            direction = -direction;
        }
        else if (collision.CompareTag("Player"))
        {
            Debug.Log("BladeTrap hit the player!"); 
            Health health = collision.GetComponentInParent<Health>();
            health.TakeDamage(1);
            
            
            
        }
    }

    
}