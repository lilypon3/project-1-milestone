using UnityEngine;
using System.Collections;

public class PlayerHitReaction : MonoBehaviour
{
    public float knockback_force = 4f;
    public float stun_duration = 0.4f;
    public float flash_interval = 0.08f;

    Health health;
    Rigidbody2D rb; 
    GridMovement gridMovement;
    SpriteRenderer sprite_renderer;

    void Start()
    {
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        gridMovement = GetComponent<GridMovement>();
        sprite_renderer = GetComponentInChildren<SpriteRenderer>();
        if (health != null) health.onDamaged += HandleDamaged;
    }

    void HandleDamaged(Vector2 knockback_direction)
    {
        StartCoroutine(HitStunRoutine(knockback_direction));
    }

   IEnumerator HitStunRoutine(Vector2 direction)
    {
        if (gridMovement != null) gridMovement.enabled = false;
        if (rb != null) rb.AddForce(direction * knockback_force, ForceMode2D.Impulse);

        float elapsed = 0f;
        while (elapsed < stun_duration)
        {
            if (sprite_renderer != null) sprite_renderer.enabled = !sprite_renderer.enabled;
            yield return new WaitForSeconds(flash_interval);
            elapsed += flash_interval;
        }
        if (sprite_renderer != null) sprite_renderer.enabled = true;

        if (rb != null) rb.linearVelocity = Vector2.zero; // <-- stop the coasting

        if (gridMovement != null) gridMovement.enabled = true;
    }
}
