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


    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GridMovement g = other.GetComponentInParent<GridMovement>();
        Rigidbody2D prb = g.GetComponent<Rigidbody2D>();

        g.SetSideScroll(destination_is_side_scroll);
        prb.position = destination.position;
        prb.linearVelocity = Vector2.zero;
        g.transform.position = destination.position;

        Vector3 cam_pos = camera_target.position;

        if (room_bottom_center != null)
        {
            // Bottom edge of an orthographic view is camera.y - orthographicSize,
            // so this puts the room's bottom edge at the bottom of the screen.
            cam_pos = new Vector3(
                room_bottom_center.position.x,
                room_bottom_center.position.y + Camera.main.orthographicSize,
                camera_target.position.z);
        }

        room_transition.SnapCameraTo(cam_pos);
        if (change_room) room_transition.SetCurrentRoom(destination_room);
    }
}