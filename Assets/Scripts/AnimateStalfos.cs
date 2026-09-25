using UnityEngine;
using System.Collections;

public class AnimateStalfos : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

        
        transform.localScale = new Vector3(-.08f, .08f, 1f);
        yield return new WaitForSeconds(0.2f);
        transform.localScale = new Vector3(.08f, .08f, 1f);
        yield return new WaitForSeconds(0.2f);

        }

    }
}
