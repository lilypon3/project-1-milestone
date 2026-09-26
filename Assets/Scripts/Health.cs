using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    [SerializeField] public int max_health; //in half heart units, so 6 = 3 hearts
    public int current_health;
    public float invincibility_time = 1.0f;

    public System.Action onDeath;
    public System.Action<Vector2> onDamaged;

    bool is_invincible = false;

    void Awake()
    {
        current_health = max_health;
    }

    public void TakeDamage (int amount, Vector2 knockback_direction)
    {
        if (is_invincible || CheatManager.god_mode) return;

        current_health -= amount;
        current_health = Mathf.Clamp(current_health, 0, max_health);
        //MakeSemiTransparent();

        StartCoroutine(InvincibilityCoroutine());
        onDamaged?.Invoke(knockback_direction);

        if (current_health <= 0)
        {
            current_health = 0;
            Die();
        }
    }

    public void addHealth (int amount)
    {
        current_health += amount;
        current_health = Mathf.Clamp(current_health, 0, max_health);
    }

    IEnumerator InvincibilityCoroutine()
    {
        is_invincible = true;
        //TODO: add visual feedback for invincibility (like flashing)
        yield return new WaitForSeconds(invincibility_time);
        is_invincible = false;
    }

    void Die()
    {
        Debug.Log("Player has died!");
        if (onDeath != null)
        {
            onDeath.Invoke();
            return; // let the listener (enemy) decide what happens instead of reloading scene
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }


}
