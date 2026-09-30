using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BombAttack : MonoBehaviour, IWeapon
{

    public GameObject bombPrefab;
    public Transform launchOffset;
    private bool isAttacking = false;
    private float timer = 0f;
    private float timeToAttack = 0.25f;
    private GridMovement gridMovement;
    private float startingMoveSpeed;

    public bool IsUnlocked { get; set; } = true; // must be picked up in-game, or cheated in

    void Start()
    {
        gridMovement = GetComponent<GridMovement>();
        startingMoveSpeed = gridMovement.moveSpeed;
    }


    void Update()
    {

        if (isAttacking)
        {
            timer += Time.deltaTime;
            if (timer >= timeToAttack)
            {
                isAttacking = false;
                gridMovement.moveSpeed = startingMoveSpeed;
                timer = 0f;
            }
    
        }

    }

    public void Attack() {
        if (isAttacking) return;
       
        Instantiate(bombPrefab, launchOffset.position, transform.rotation);
        isAttacking = true;
    }

    
}
