using UnityEngine;
public class SwordAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite attackSprite; // assign this in the Inspector
    public Sprite attackSpriteLeft;
    public Sprite attackSpriteRight;
    public Sprite attackSpriteUp;

    private Animator myAnimator;
    private InputToAnimator inputToAnimator;
    private GridMovement gridMovement;
    private Sprite defaultSprite;
    private bool animatorWasEnabled;
    private bool inputToAnimatorWasEnabled;


    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        inputToAnimator = GetComponent<InputToAnimator>();
        gridMovement = GetComponent<GridMovement>();
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        if (spriteRenderer != null)
        {
            defaultSprite = spriteRenderer.sprite;
        }
    }

    public void SetAttacking(bool attacking)
    {

        if (attacking)
        {

            animatorWasEnabled = myAnimator != null && myAnimator.enabled;
            inputToAnimatorWasEnabled = inputToAnimator != null && inputToAnimator.enabled;

            if (myAnimator != null) myAnimator.enabled = false;
            if (inputToAnimator != null) inputToAnimator.enabled = false;

            if (gridMovement.LastMoveDir == Vector2.right) // right
            {
                spriteRenderer.sprite = attackSpriteRight;
            }
            else if (gridMovement.LastMoveDir == Vector2.left) // left
            {
                spriteRenderer.sprite = attackSpriteLeft;
            }
            else if (gridMovement.LastMoveDir == Vector2.up) // up
            {
                spriteRenderer.sprite = attackSpriteUp;
            }
            else if (gridMovement.LastMoveDir == Vector2.down) // down
            {
                spriteRenderer.sprite = attackSprite;
            }
            else
            {
                spriteRenderer.sprite = attackSprite; // default to down if direction is unknown
            }
            return;
        }

        spriteRenderer.sprite = defaultSprite;
        if (inputToAnimator != null) inputToAnimator.enabled = inputToAnimatorWasEnabled;
        if (myAnimator != null) myAnimator.enabled = animatorWasEnabled;
    }
}