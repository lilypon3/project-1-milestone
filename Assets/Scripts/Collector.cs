using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Collector : MonoBehaviour
{
    Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        if(inventory == null)
        {
            Debug.LogWarning("WARNING: Gameobject with a collector has no inventory to store things in!");
        }
    }
    void OnTriggerEnter(Collider coll)
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
        }
    }
}
