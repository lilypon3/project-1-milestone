using UnityEngine;

public class WallMasterGrab : MonoBehaviour
{
    public Vector3 player_destination = new Vector3(39.5f, 3.1f, 0f);
    public Vector3 camera_destination = new Vector3(39.5f, 6.6f, -10f);

    RoomTransition room_transition;

    void Start()
    {
        room_transition = FindFirstObjectByType<RoomTransition>();
        if (room_transition == null) Debug.LogWarning("WallMaster: no RoomTransition found in scene.");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) GrabPlayer();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) GrabPlayer();
    }

    void GrabPlayer()
    {
        if (room_transition == null) return;
        room_transition.TeleportToRoom(room_transition.StartingRoom, player_destination, camera_destination);
        Debug.Log("Player grabbed by Wall Master!");
    }
}