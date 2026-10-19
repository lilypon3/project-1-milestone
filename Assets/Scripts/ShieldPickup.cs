using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    public AudioClip pickup_sound;

    void OnTriggerEnter2D(Collider2D other)
    {
        ShieldBlock shield = other.GetComponentInParent<ShieldBlock>();
        if (shield == null) return;          // only Link (who owns ShieldBlock) can pick it up

        shield.IsUnlocked = true;
        if (pickup_sound != null && Camera.main != null)
            AudioSource.PlayClipAtPoint(pickup_sound, Camera.main.transform.position);

        Destroy(gameObject);
    }
}