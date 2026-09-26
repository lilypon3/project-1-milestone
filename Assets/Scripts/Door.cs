using UnityEngine;

public enum DoorDirection { Right, Left, Up, Down }

public class Door : MonoBehaviour
{
    public DoorDirection direction;
    public bool is_locked = false;
    public RoomTransition room_transition; 

    public BoxCollider2D solid_blocker; // Collider that blocks the player when the door is locked
    public BoxCollider2D trigger_collider; // Collider that triggers the room transition when the player enters

    void Start()
    {
        UpdateColliders();
    }

    void UpdateColliders()
    {
        solid_blocker.enabled = is_locked;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (is_locked)
        {
            TryUnlock(other.gameObject);
            return;
        }

        room_transition.TriggerTransition(direction, other.gameObject);
    }

    void TryUnlock(GameObject player)
    {
        Inventory inventory = player.GetComponent<Inventory>();
        if (inventory != null && inventory.GetKeys() > 0)
        {
            inventory.UseKey();
            is_locked = false;
            UpdateColliders();
            Debug.Log("Door unlocked!");
            room_transition.TriggerTransition(direction, player);
        }
        else
        {
            Debug.Log("Door is locked. Need a key.");
        }
    }
}