using UnityEngine;
using System.Collections;

public class EnemyHitReaction : MonoBehaviour
{
    public float knockback_force = 3f;
    public float flash_interval = 0.08f;
    public float flash_duration = 0.3f;

    Health health;
    Rigidbody2D rb;
    StalfosMovement stalfos_movement;
    SpriteRenderer sprite_renderer;

    void Start()
    {
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        stalfos_movement = GetComponent<StalfosMovement>();
        sprite_renderer = GetComponentInChildren<SpriteRenderer>();
        if (health != null) health.onDamaged += HandleDamaged;
    }

    void HandleDamaged(Vector2 knockback_direction)
    {
        StartCoroutine(FlashAndKnockback(knockback_direction));
    }

    IEnumerator FlashAndKnockback(Vector2 direction)
{
    if (stalfos_movement != null) stalfos_movement.SetKnockedBack(true);
    if (rb != null) rb.AddForce(direction * knockback_force, ForceMode2D.Impulse);

    float elapsed = 0f;
    while (elapsed < flash_duration)
    {
        if (sprite_renderer != null) sprite_renderer.enabled = !sprite_renderer.enabled;
        yield return new WaitForSeconds(flash_interval);
        elapsed += flash_interval;
    }
    if (sprite_renderer != null) sprite_renderer.enabled = true;

    if (rb != null) rb.linearVelocity = Vector2.zero; // <-- stop the coasting

    if (stalfos_movement != null)
    {
        stalfos_movement.SetKnockedBack(false);
        stalfos_movement.ResyncAfterKnockback();
    }
}
}