using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Continuous, pixel-snapped movement driven through Rigidbody2D so walls
/// stop Link naturally -- even mid-tile, like the original NES game.
/// Movement speed is frame-rate independent, and no visual jitter comes
/// from sub-pixel positions, thanks to the "remainder" accumulator below.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class GridMovement : MonoBehaviour
{
    [Tooltip("World units per second (1 unit = 1 tile if your tiles are 1x1)")]
    public float moveSpeed = 4f;

    [Tooltip("Your sprite's Pixels Per Unit import setting. Movement is " +
             "snapped to this so art never renders on a half pixel.")]
    public float pixelsPerUnit = 16f;

    private Rigidbody2D rb;

    // Fractional movement left over after rounding to a whole pixel this
    // step. Carried into next FixedUpdate so speed stays accurate over time
    // instead of losing/gaining a bit of distance every frame.
    private Vector2 remainder;

    public Vector2 LastMoveDir { get; private set; } = Vector2.down;
    public bool IsMoving { get; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Dynamic (not Kinematic) is what makes this work: MovePosition on a
        // Dynamic body still gets resolved against wall colliders by the
        // physics engine, so you stop wherever you bump something instead
        // of needing your own collision checks.
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void FixedUpdate()
    {
        Vector2 input = GetInput();
        if (input != Vector2.zero) LastMoveDir = input;
        Step(input);
    }


    // Arrow key movement input
    private Vector2 GetInput()
    {
        var kb = Keyboard.current;
        if (kb == null) return Vector2.zero;

        Vector2 dir = Vector2.zero;

        if (kb.leftArrowKey.isPressed || kb.aKey.isPressed) dir.x = -1f;
        else if (kb.rightArrowKey.isPressed || kb.dKey.isPressed) dir.x = 1f;

        // No diagonal
        if (dir.x == 0f)
        {
            if (kb.upArrowKey.isPressed || kb.wKey.isPressed) dir.y = 1f;
            else if (kb.downArrowKey.isPressed || kb.sKey.isPressed) dir.y = -1f;
        }

        return dir;
    }

    private void Step(Vector2 input)
    {
        if (input == Vector2.zero)
        {
            IsMoving = false;
            remainder = Vector2.zero;
            return;
        }

        IsMoving = true;
        float unit = 1f / pixelsPerUnit;

        // Distance we want to cover this physics step, plus whatever we
        // owed from last step.
        Vector2 distance = input * moveSpeed * Time.fixedDeltaTime + remainder;

        // Only ever actually move in whole-pixel increments...
        Vector2 pixelMove = new Vector2(
            Mathf.Round(distance.x / unit) * unit,
            Mathf.Round(distance.y / unit) * unit
        );

        // ...and bank whatever fraction didn't fit for next step.
        remainder = distance - pixelMove;

        if (pixelMove != Vector2.zero)
            rb.MovePosition(rb.position + pixelMove);
    }

    /// <summary>
    /// True if Link is currently sitting exactly on a tile boundary.
    /// Handy for room-scroll triggers, block pushing, etc. WITHOUT forcing
    /// his actual movement onto a strict tile-by-tile grid.
    /// </summary>
    public bool IsGridAligned(float tileSize = 1f)
    {
        Vector2 pos = rb.position;
        return Mathf.Approximately(Mathf.Repeat(pos.x, tileSize), 0f)
            && Mathf.Approximately(Mathf.Repeat(pos.y, tileSize), 0f);
    }
}