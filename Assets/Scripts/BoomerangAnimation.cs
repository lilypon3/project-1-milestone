using UnityEngine;

public class BoomerangAnimation : MonoBehaviour
{
    // Adjust the speed in the Unity Inspector
    [SerializeField] private float spinSpeed = 100f; 

    void Update()
    {
        // Vector3.forward represents the Z-axis (0, 0, 1)
        // Multiplying by Time.deltaTime ensures a smooth spin regardless of frame rate
        transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
    }
}
