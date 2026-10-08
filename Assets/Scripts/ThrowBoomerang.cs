using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ThrowBoomerang : MonoBehaviour
{

    
    public MonoBehaviour boomerang;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Throw());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Throw() {
        IWeapon weapon = boomerang as IWeapon;
        yield return new WaitForSeconds(3f);
        weapon.Attack();
        StartCoroutine(Throw());
    }
}
