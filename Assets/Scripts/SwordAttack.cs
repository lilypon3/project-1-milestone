using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D> ();

    }

    // Update is called once per frame
    void GetInput()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }
    }

    
}
