using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    [SerializeField] public int max_health = 6; //in half heart units, so 6 = 3 hearts
    public int current_health;
    public float invincibility_time = 1.0f;

    bool is_invincible = false;

    void Awake()
    {
        current_health = max_health;
    }

    public void TakeDamage (int amount)
    {
        if (is_invincible) return;

        current_health -= amount;
        current_health = Mathf.Clamp(current_health, 0, max_health);

        StartCoroutine(InvincibilityCoroutine());

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
        Destroy(gameObject);
        //TODO: add game over logic
    }


}
