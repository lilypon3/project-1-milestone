using UnityEngine;
using System.Collections;

public class PushableBlock : MonoBehaviour
{
    public float push_delay = 0.5f;
    public float tile_size = 1.0f;
    public float move_time = 0.25f;

    public LayerMask blocking_layers;

    public bool one_use = true;

    public System.Action onPush;

    Rigidbody2D rb;
    Collider2D col;
    float push_timer;
    bool is_moving;
    bool is_used;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (is_moving || (is_used && one_use)) return;

        GridMovement player = collision.collider.GetComponent<GridMovement>();

        if(player == null) return;

        if(!player.enabled || player.moveSpeed <= 0f || !player.IsMoving)
        {
            push_timer = 0f;
            return;
        }

        Vector2 dir = player.LastMoveDir;

        Vector2 to_block = (Vector2)transform.position - (Vector2)player.transform.position;
        to_block.Normalize();
        if(Vector2.Dot(dir, to_block) < 0.9f)
        {
            push_timer = 0f;
            return;

        }

        push_timer += Time.fixedDeltaTime;
        if(push_timer >= push_delay)
        {
            push_timer = 0f;
            TryMove(dir);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        push_timer = 0f;
    }

    void TryMove(Vector2 dir)
    {
        Vector2 target_pos = rb.position + dir * tile_size;
        Collider2D hit = Physics2D.OverlapBox(target_pos, col.bounds.size * 0.8f, 0f, blocking_layers);
        if (hit != null && hit != col)
        {
            return;
        }
        StartCoroutine(MoveToPosition(target_pos));
        Debug.Log("Block moving from " + rb.position + " to " + target_pos);
    }

    IEnumerator MoveToPosition(Vector2 target_pos)
    {
        is_moving = true;
        Vector2 start_pos = rb.position;
        float elapsed = 0f;

        while (elapsed < move_time)
        {
            elapsed += Time.fixedDeltaTime;
            rb.MovePosition(Vector2.Lerp(start_pos, target_pos, elapsed / move_time));
            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(target_pos);
        is_moving = false;
        is_used = true;

        onPush?.Invoke();
    }
}
