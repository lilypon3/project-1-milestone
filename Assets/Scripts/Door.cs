using UnityEngine;

public enum DoorDirection { Right, Left, Up, Down }
public enum DoorType { Open, Locked, ShutUntilCleared }

public class Door : MonoBehaviour
{
    public DoorDirection direction;

    public DoorType type = DoorType.Open;

    public RoomTransition room_transition; 

    public BoxCollider2D solid_blocker; // Collider that blocks the player when the door is locked
    public BoxCollider2D trigger_collider; // Collider that triggers the room transition when the player enters

    void Start()
    {
        UpdateColliders();
    }

    void UpdateColliders()
    {
        bool closed = type == DoorType.Locked || type == DoorType.ShutUntilCleared;
        if(solid_blocker != null) solid_blocker.enabled = closed;
    }

    public void SetShut(bool shut)
    {
        if (type == DoorType.Locked) return; // don't change locked doors
        type = shut ? DoorType.ShutUntilCleared : DoorType.Open;
        UpdateColliders();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (type == DoorType.Locked)
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
            type = DoorType.Open;
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