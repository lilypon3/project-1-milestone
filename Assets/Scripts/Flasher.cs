using UnityEngine;
using System.Collections;

public class Flasher : MonoBehaviour
{

    public float flash_interval = 0.15f;
    SpriteRenderer sprite_renderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        if (sprite_renderer == null)
        {
            Debug.LogWarning("WARNING: Flasher script attached to gameobject with no sprite renderer!");
        }
        StartCoroutine(FlashLoop());
        
    }

    IEnumerator FlashLoop()
    {
        while (true)
        {
            sprite_renderer.enabled = !sprite_renderer.enabled;
            yield return new WaitForSeconds(flash_interval);
        }
    }
}
