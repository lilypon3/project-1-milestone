using UnityEngine;

public class StairTrigger : MonoBehaviour
{
    public Transform destination;
    public bool destination_is_side_scroll;
    public RoomTransition room_transition;
    public Transform camera_target;          // supplies the camera's Z, and the fallback position
    public Transform room_bottom_center;     // empty object at the bottom-middle of the bow room for correct camera positioning
    public bool change_room = true;
    public Vector2Int destination_room;   // grid coord of the room the stairs lead to
    public bool use_fade = true;
    public bool save_camera_on_use = false;        
    public bool restore_saved_camera = false;      



    void OnTriggerEnter2D(Collider2D other)
{
    if (!other.CompareTag("Player")) return;
    if (room_transition == null) return;

    GridMovement g = other.GetComponentInParent<GridMovement>();
    Rigidbody2D prb = g.GetComponent<Rigidbody2D>();

    room_transition.RunWithFade(() =>
    {
        g.SetSideScroll(destination_is_side_scroll);
        prb.position = destination.position;
        prb.linearVelocity = Vector2.zero;
        g.transform.position = destination.position;

        if (save_camera_on_use) room_transition.SaveCameraPosition();   // must run BEFORE the camera snaps

        if (restore_saved_camera && room_transition.HasSavedCamera)
        {
            room_transition.RestoreCameraPosition();
        }
        else
        {
            Vector3 cam_pos = camera_target.position;
            if (room_bottom_center != null)
            {
                cam_pos = new Vector3(
                    room_bottom_center.position.x,
                    room_bottom_center.position.y + Camera.main.orthographicSize,
                    camera_target.position.z);
            }
            room_transition.SnapCameraTo(cam_pos);
        }

        if (change_room) room_transition.SetCurrentRoom(destination_room);
    }, use_fade);
}
}