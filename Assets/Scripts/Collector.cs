using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Collector : MonoBehaviour
{
    public AudioClip rupee_collection_sound_clip;
    public AudioClip heart_collection_sound_clip;
    public AudioClip key_collection_sound_clip;
    public AudioClip bow_collection_sound_clip;
    public AudioClip bomb_collection_sound_clip;
    Inventory inventory;
    Health health;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        health = GetComponent<Health>();
        if(inventory == null)
        {
            Debug.LogWarning("WARNING: Gameobject with a collector has no inventory to store things in!");
        }
        if (health == null)
        {
            Debug.LogWarning("WARNING: No health component found on gameobject with collector. This means that the collector will not be able to collect hearts.");
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject object_collided_with = coll.gameObject;

        if (object_collided_with.tag == "rupee")
        {
            Debug.Log("Collected rupee!");
            if (inventory != null)
            {
                inventory.AddRupee(1);
            }

            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(rupee_collection_sound_clip, Camera.main.transform.position);
        }

        else if (object_collided_with.tag == "heart")
        {
            Debug.Log("Collected heart!");
            if (health != null)
            {
                health.addHealth(2); //restore 1 heart = 2 half-hearts
            }

            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(heart_collection_sound_clip, Camera.main.transform.position);
        }

        else if (object_collided_with.tag == "key")
        {
            Debug.Log("Collected key!");
            if (inventory != null) inventory.AddKey(1);
            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(key_collection_sound_clip, Camera.main.transform.position);
        }

          else if (object_collided_with.tag == "bow")
        {
            GetComponent<BowAttack>().IsUnlocked = true;
            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(bow_collection_sound_clip, Camera.main.transform.position);
        }

        else if (object_collided_with.tag == "bomb")
        {
            Debug.Log("Collected bomb!");
            if (inventory != null) inventory.AddBomb(1);
            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(bomb_collection_sound_clip, Camera.main.transform.position);
        }
    }
}
