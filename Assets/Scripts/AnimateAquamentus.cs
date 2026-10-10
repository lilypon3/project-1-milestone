using UnityEngine;
using System.Collections;

public class AnimateAquamentus : MonoBehaviour
{

    public Sprite defaultSprite;
    public Sprite flap;
    public SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
      
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
    void Start()
    {
        StartCoroutine(Animate());
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localScale = new Vector3(-.08f, .08f, 1f);
        

    }
    IEnumerator Animate()
    {
        while (true) {

        
        spriteRenderer.sprite = flap;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.sprite = defaultSprite;
        yield return new WaitForSeconds(0.2f);

        }

    }
}
