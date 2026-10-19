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

    public int rupeesCollectedDuringGodMode = 0;
    public int keysCollectedDuringGodMode = 0;
    public int bombsCollectedDuringGodMode = 0;

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

        // the shield link is holding is not a pickup
        if (coll.GetComponentInParent<ShieldBehavior>() != null) return;

        if (object_collided_with.tag == "rupee")
        {
            if (inventory != null)
            {
                Debug.Log("Collected rupee!");
                inventory.AddRupee(1);

                if (CheatManager.god_mode)
                    rupeesCollectedDuringGodMode++;
            }

            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(rupee_collection_sound_clip,Camera.main.transform.position);
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
            if (inventory != null)
            {
                Debug.Log("Collected key!");
                inventory.AddKey(1);

                if (CheatManager.god_mode)
                    keysCollectedDuringGodMode++;
            }

            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(key_collection_sound_clip,Camera.main.transform.position);
        }

          else if (object_collided_with.tag == "bow")
        {
            GetComponent<BowAttack>().IsUnlocked = true;
            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(bow_collection_sound_clip, Camera.main.transform.position);
        }

        else if (object_collided_with.tag == "bomb")
        {
            if (inventory != null)
            {
                Debug.Log("Collected bomb!");
                inventory.AddBomb(1);

                if (CheatManager.god_mode)
                    bombsCollectedDuringGodMode++;
            }

            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(bomb_collection_sound_clip,Camera.main.transform.position);
        }
    }
}
