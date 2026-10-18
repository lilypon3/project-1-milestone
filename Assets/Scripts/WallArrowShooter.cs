using System.Collections;
using UnityEngine;

public class WallArrowShooter : MonoBehaviour
{
    public ProjectileBehavior arrow_prefab;   // HostileArrow
    public Vector2 direction = Vector2.down;  // down for top wall, up for bottom wall
    public float interval = 2f;
    public float start_delay = 0f;            // stagger shooters with this
    public RoomController room;               // only fire while Link is in this room

    IEnumerator Start()
    {
        yield return new WaitForSeconds(start_delay);
        while (true)
        {
            if (room == null || room.IsActiveRoom) Fire();
            yield return new WaitForSeconds(interval);
        }
    }

    void Fire()
    {
        ProjectileBehavior a = Instantiate(arrow_prefab, transform.position, Quaternion.identity);
        a.hostile = true;
        a.Init(direction, null);
    }
}