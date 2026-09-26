using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NES-style enemy movement: walks one full tile at a time in a random
/// cardinal direction, occasionally pausing, and re-rolls direction whenever
/// it reaches a tile boundary (or gets stuck against a wall mid-tile).
/// </summary>
public class StalfosMovement : MonoBehaviour
{
    [Tooltip("World units per second (1 unit = 1 tile if your tiles are 1x1)")]
    public float moveSpeed = 2f;

    [Tooltip("Size of one tile in world units")]
    public float tileSize = 1f;

    [Range(0f, 1f)]
    [Tooltip("Chance to pause instead of immediately picking a new direction after finishing a tile")]
    public float chanceToPause = 0.25f;

    [Tooltip("How long a pause lasts, in seconds")]
    public float pauseDuration = 0.4f;

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private Vector2 moveDir;
    private bool paused;
    private float pauseTimer;

    public Vector2 LastMoveDir { get; private set; } = Vector2.down;
    public bool IsMoving { get; private set; }

    private static readonly Vector2[] CardinalDirs =
    {
        Vector2.up, Vector2.down, Vector2.left, Vector2.right
    };

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Dynamic is what lets MovePosition still resolve against wall
        // colliders via the physics engine.
        rb.bodyType = RigidbodyType2D.Dynamic;

        // Make sure we start exactly on a tile boundary, otherwise every
        // future "tile" is offset and grid checks never line up.
        rb.position = SnapToGrid(rb.position);
        targetPosition = rb.position;

        PickNewDirection();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void FixedUpdate()
    {
        if (paused)
        {
            IsMoving = false;
            pauseTimer -= Time.fixedDeltaTime;
            if (pauseTimer <= 0f) PickNewDirection();
            return;
        }

        Vector2 newPos = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        IsMoving = true;

        if ((newPos - targetPosition).sqrMagnitude < 0.0001f)
        {
            rb.position = targetPosition; // hard snap, avoid float drift
            ChooseNextTileOrPause();
        }
    }

    // Handle the initial impact once. Re-snapping and rerolling every physics
    // step while contact persists fights Rigidbody2D's collision solver.
    void OnCollisionEnter2D(Collision2D collision) => HandleBlocked(collision);

    private void HandleBlocked(Collision2D collision)
    {
        if (paused || collision.contactCount == 0) return;

        Vector2 normal = collision.GetContact(0).normal; // points away from the wall, into us
        rb.position = SnapToGrid(rb.position);
        PickNewDirection(normal);
    }

    private void ChooseNextTileOrPause()
    {
        if (Random.value < chanceToPause)
        {
            paused = true;
            pauseTimer = pauseDuration;
            IsMoving = false;
        }
        else
        {
            PickNewDirection();
        }
    }

    private void PickNewDirection(Vector2? avoidNormal = null)
    {
        paused = false;

        if (avoidNormal.HasValue)
        {
            // Only keep directions that don't walk straight back into
            // whatever we just hit.
            List<Vector2> options = new List<Vector2>();
            foreach (var dir in CardinalDirs)
            {
                if (Vector2.Dot(dir, avoidNormal.Value) > -0.1f)
                    options.Add(dir);
            }
            // Safety net: if geometry somehow rules out every direction
            // (e.g. wedged in a corner), fall back to any direction rather
            // than never moving again.
            moveDir = options.Count > 0
                ? options[Random.Range(0, options.Count)]
                : CardinalDirs[Random.Range(0, CardinalDirs.Length)];
        }
        else
        {
            moveDir = CardinalDirs[Random.Range(0, CardinalDirs.Length)];
        }

        LastMoveDir = moveDir;
        targetPosition = rb.position + moveDir * tileSize;
        IsMoving = true;
    }

    private Vector2 SnapToGrid(Vector2 pos)
    {
        return new Vector2(
            Mathf.Round(pos.x / tileSize) * tileSize,
            Mathf.Round(pos.y / tileSize) * tileSize
        );
    }

    /// <summary>
    /// True if this enemy is currently sitting exactly on a tile boundary.
    /// </summary>
    public bool IsGridAligned()
    {
        Vector2 pos = rb.position;
        return Mathf.Approximately(Mathf.Repeat(pos.x, tileSize), 0f)
            && Mathf.Approximately(Mathf.Repeat(pos.y, tileSize), 0f);
    }

    public void SetKnockedBack(bool knocked_back)
    {
        enabled = !knocked_back; // disabling the script stops FixedUpdate from running at all
    }

    public void ResyncAfterKnockback()
    {
        rb.position = SnapToGrid(rb.position);
        targetPosition = rb.position;
        PickNewDirection();
    }
}