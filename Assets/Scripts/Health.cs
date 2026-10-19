using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    [SerializeField] public int max_health; //in half heart units, so 6 = 3 hearts
    public int current_health;
    public float invincibility_time = 1.0f;
    public AudioClip damage_sound_clip;
    public AudioClip death_sound_clip;

    public System.Action onDeath;
    public System.Action<Vector2> onDamaged;
    

    bool is_invincible = false;
    bool is_shielding = false;

    void Awake()
    {
        current_health = max_health;
    }

    public void TakeDamage (int amount, Vector2 knockback_direction)
    {
        if (is_invincible || is_shielding) return;
        if (CheatManager.god_mode && CompareTag("Player")) return;

        current_health -= amount;
        if (current_health > 0) PlaySound(damage_sound_clip);        
        Debug.Log("Took damage");
        current_health = Mathf.Clamp(current_health, 0, max_health);

        StartCoroutine(InvincibilityCoroutine());
        onDamaged?.Invoke(knockback_direction);

        if (current_health <= 0)
        {
            current_health = 0;
            Die();
        }
    }

    public void addHealth(int amount)
    {
        current_health += amount;

        // do not clamp health while the player has god mode enabled
        if (CheatManager.god_mode && CompareTag("Player"))
        {
            return;
        }

        current_health = Mathf.Clamp(current_health, 0, max_health);
    }

    public void SetHealth(int amount)
    {
        current_health = Mathf.Clamp(amount, 0, max_health);
    }

    public void SetShielding(bool shielding)
    {
        is_shielding = shielding;
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
        PlaySound(death_sound_clip);
        Debug.Log(name + " died");

        if (onDeath != null)
        {
            onDeath.Invoke();
            return;
        }
        StartCoroutine(ReloadAfterDelay(1.0f));   
    }

    IEnumerator ReloadAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && Camera.main != null)
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }


}
