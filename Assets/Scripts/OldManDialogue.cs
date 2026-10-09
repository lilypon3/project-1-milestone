using UnityEngine;
using System.Collections;
using TMPro;

public class OldManDialogue : MonoBehaviour
{
    public TextMeshProUGUI text_box;
    [TextArea] public string message = "HIIII >0<";
    public float char_delay = 0.05f;
    public AudioClip blip;

    Coroutine typing;

    void Awake()
{
    if (text_box != null) text_box.gameObject.SetActive(false);
}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || typing != null) return;
        typing = StartCoroutine(Type());
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) Hide();
    }

    void OnDisable() { Hide(); }   // room freeze/leave also hides it

    IEnumerator Type()
    {
        text_box.gameObject.SetActive(true);
        text_box.text = "";
        foreach (char c in message)
        {
            text_box.text += c;
            if (blip != null && c != ' ') AudioSource.PlayClipAtPoint(blip, Camera.main.transform.position, 0.4f);
            yield return new WaitForSeconds(char_delay);
        }
    }

    void Hide()
    {
        if (typing != null) StopCoroutine(typing);
        typing = null;
        if (text_box != null) text_box.gameObject.SetActive(false);
    }
}