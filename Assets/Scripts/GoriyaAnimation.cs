using UnityEngine;
using System.Collections;
public class GoriyaAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite moveDown; // assign this in the Inspector
    public Sprite moveLeft;
    public Sprite moveRight;
    public Sprite moveUp;

    public Sprite moveDownAnim; // assign this in the Inspector
    public Sprite moveLeftAnim;
    public Sprite moveRightAnim;
    public Sprite moveUpAnim;


    private StalfosMovement gridMovement;
    private Sprite defaultSprite;


    void Start()
    {
        StartCoroutine(Animate());
    }

    void Awake()
    {
        
        gridMovement = GetComponent<StalfosMovement>();
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

    public void Update()
    {

        Animate();
            
        //spriteRenderer.sprite = defaultSprite;

    }

    IEnumerator Animate()
    {
        while (true) {
            Vector2 direction = gridMovement.LastMoveDir;
            if (direction == Vector2.right) // right
            {
                spriteRenderer.sprite = moveRightAnim;
                yield return new WaitForSeconds(0.2f);
                spriteRenderer.sprite = moveRight;
                yield return new WaitForSeconds(0.2f);
            }
            else if (direction == Vector2.left) // left
            {
                spriteRenderer.sprite = moveLeftAnim;
                yield return new WaitForSeconds(0.2f);
                spriteRenderer.sprite = moveLeft;
                yield return new WaitForSeconds(0.2f);
            }
            else if (direction == Vector2.up) // up
            {
                spriteRenderer.sprite = moveUpAnim;
                yield return new WaitForSeconds(0.2f);
                spriteRenderer.sprite = moveUp;
                yield return new WaitForSeconds(0.2f);
            }
            else if (direction == Vector2.down) // down
            {
                spriteRenderer.sprite = moveDownAnim;
                yield return new WaitForSeconds(0.2f);
                spriteRenderer.sprite = moveDown;
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                spriteRenderer.sprite = moveDown; // default to down if direction is unknown
            }
        }
    }
}