using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BombBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("Explode");
    }

    // Update is called once per frame
    public IEnumerator Explode() {
        yield return new WaitForSeconds(1.5f);
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Enemy").transform.position) < 1.5f)
        {
            Health health = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(3, Vector2.zero);
            }
        }
        Destroy(gameObject);
    }
}
