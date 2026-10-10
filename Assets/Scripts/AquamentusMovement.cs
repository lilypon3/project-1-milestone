using UnityEngine;
using System.Collections;

public class AquamentusMovement : MonoBehaviour
{
    public GameObject aquamentus;
    private Transform aquamentusTransform;
    private float speed = 1f;

    void Start()
    {
        aquamentusTransform = aquamentus.transform;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        Vector3[] waypoints =
        {
            new Vector3(Random.Range(73.24f, 74.63f), 49.42f, 0f),
            new Vector3(Random.Range(73.24f, 74.63f), 49.42f, 0f)
        };

        while (true)
        {
            
            yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));
            yield return MoveTo(new Vector3(Random.Range(73.24f, 74.63f), 49.42f, 0f));
            
        }
    }

    IEnumerator MoveTo(Vector3 target)
    {
        while (aquamentusTransform.position != target)
        {
            aquamentusTransform.position = Vector3.MoveTowards(
                aquamentusTransform.position,
                target,
                speed * Time.deltaTime);
            yield return null;
        }
    }
}
