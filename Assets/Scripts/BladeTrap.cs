using UnityEngine;

public class BladeTrap : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float detectionRange;

    private static readonly Vector2[] RayDirections =
    {
        Vector2.down,
        Vector2.up,
        Vector2.left,
        Vector2.right
    };

    private Vector3 startingPosition;
    private Vector2 moveDirection;
    private LayerMask playerMask;
    private Collider2D trapCollider;
    private readonly RaycastHit2D[] movementHits = new RaycastHit2D[8];
    private ContactFilter2D movementFilter;
    private TrapState state;
    private bool playerWasDetected;

    private enum TrapState
    {
        Idle,
        Charging,
        Returning
    }

    private void Awake()
    {
        startingPosition = transform.position;
        playerMask = LayerMask.GetMask("Player");
        trapCollider = GetComponent<Collider2D>();
        movementFilter = new ContactFilter2D
        {
            useLayerMask = true,
            layerMask = Physics2D.AllLayers,
            useTriggers = true
        };
    }

    private void Update()
    {
        bool playerDetected = TryDetectPlayer(out Vector2 detectedDirection);

        if (state == TrapState.Idle && playerDetected && !playerWasDetected)
        {
            moveDirection = detectedDirection;
            state = TrapState.Charging;
        }

        playerWasDetected = playerDetected;
    }

    private void FixedUpdate()
    {
        if (state == TrapState.Charging)
        {
            ChargeForward();
        }
        else if (state == TrapState.Returning)
        {
            ReturnToStartingPosition();
        }
    }

    private void ChargeForward()
    {
        float step = speed * Time.fixedDeltaTime;

        if (trapCollider != null)
        {
            int hitCount = trapCollider.Cast(moveDirection, movementFilter, movementHits, step);
            for (int i = 0; i < hitCount; i++)
            {
                BladeTrap otherTrap = movementHits[i].collider.GetComponentInParent<BladeTrap>();
                if (otherTrap != null && otherTrap != this)
                {
                    BeginReturn();
                    otherTrap.BeginReturn();
                    return;
                }
            }
        }

        transform.position += (Vector3)(moveDirection * step);
    }

    private void ReturnToStartingPosition()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            startingPosition,
            speed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, startingPosition) < 0.001f)
        {
            transform.position = startingPosition;
            moveDirection = Vector2.zero;
            state = TrapState.Idle;
        }
    }

    private bool TryDetectPlayer(out Vector2 detectedDirection)
    {
        detectedDirection = Vector2.zero;
        RaycastHit2D closestHit = default;
        bool foundPlayer = false;

        foreach (Vector2 rayDirection in RayDirections)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                rayDirection,
                detectionRange,
                playerMask);

            Debug.DrawRay(
                transform.position,
                rayDirection * detectionRange,
                Color.red);

            if (hit.collider != null && (!foundPlayer || hit.distance < closestHit.distance))
            {
                closestHit = hit;
                detectedDirection = rayDirection;
                foundPlayer = true;
            }
        }

        return foundPlayer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleContact(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleContact(other);
    }

    private void HandleContact(Collider2D other)
    {
        BladeTrap otherTrap = other.GetComponentInParent<BladeTrap>();
        if (otherTrap != null && otherTrap != this)
        {
            BeginReturn();
            otherTrap.BeginReturn();
            return;
        }

        Health health = other.GetComponentInParent<Health>();
        if (health != null)
        {
            health.TakeDamage(1, moveDirection);
        }
    }

    private void BeginReturn()
    {
        if (state != TrapState.Returning)
        {
            state = TrapState.Returning;
        }
    }
}
