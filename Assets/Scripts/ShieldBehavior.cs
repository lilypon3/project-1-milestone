using UnityEngine;

public class ShieldBehavior : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        Destroy(gameObject);
    }
}
