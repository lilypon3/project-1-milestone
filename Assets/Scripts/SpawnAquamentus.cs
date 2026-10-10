using UnityEngine;

public class SpawnAquamentus : MonoBehaviour
{

    public GameObject aquamentus;
    private bool isAquamentusSpawned = false;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if 
        (!isAquamentusSpawned && player.transform.position.x >= 70f && player.transform.position.x <= 75f && player.transform.position.y >= 48f && player.transform.position.y <= 50f)
        {
            Spawn();
            isAquamentusSpawned = true;
        }
    }

    void Spawn()
    {
        aquamentus.SetActive(true);
    }
}
