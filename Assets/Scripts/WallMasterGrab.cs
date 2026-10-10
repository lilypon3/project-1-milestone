using UnityEngine;

public class WallMasterGrab : MonoBehaviour
{

    private Transform playerTransform;
    private Transform cameraTransform;
    public GameObject camera;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = player.transform;
        cameraTransform = camera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Call the method to handle the player being grabbed
            GrabPlayer(other.gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Call the method to handle the player being grabbed
            GrabPlayer(collision.gameObject);
        }
    }

    void GrabPlayer(GameObject player)
    {
        playerTransform.transform.position = new Vector3(39.5f, 3.1f, 0f);
        cameraTransform.transform.position = new Vector3(39.5f, 6.6f, -10f);
        // Implement the logic for grabbing the player here
        // For example, you might want to disable player controls, play an animation, etc.
        Debug.Log("Player grabbed by Wall Master!");

    }
}
