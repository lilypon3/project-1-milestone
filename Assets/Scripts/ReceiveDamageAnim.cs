using UnityEngine;

public class ReceiveDamageAnim : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    public float targetAlpha = 0.5f; // 0.5f means 50% semi-transparent

    void Start()
    {
        // Cache the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // This triggers when another 2D Collider enters this object's trigger zone
    private void MakeSemiTransparent()
    {
        if (spriteRenderer != null)
        {
            Color currentColor = spriteRenderer.color;
            currentColor.a = targetAlpha;
            spriteRenderer.color = currentColor;
        }
    }
}
