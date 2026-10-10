using UnityEngine;

public class BlockDoorOpen : MonoBehaviour
{
    public RoomController room;     // the room with the enemies
    public PushableBlock block;     // the block to push
    public Door door;               // door to open (type = Sealed)
    public AudioClip open_sound;

    void Start()
    {
        // block stays frozen until the room is cleared
        block.pushing_enabled = (room == null) || room.IsCleared;

        if (room != null) room.onCleared += HandleRoomCleared;
        block.onPush += HandleBlockPushed;
    }

    void OnDestroy()
    {
        if (room != null) room.onCleared -= HandleRoomCleared;
        if (block != null) block.onPush -= HandleBlockPushed;
    }

    void HandleRoomCleared()
    {
        block.pushing_enabled = true;
    }

    void HandleBlockPushed()
    {
        door.Unlock();
       
        if (open_sound != null)
            AudioSource.PlayClipAtPoint(open_sound, Camera.main.transform.position);
    }
}