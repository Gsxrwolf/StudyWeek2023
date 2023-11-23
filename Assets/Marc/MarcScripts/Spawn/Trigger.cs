using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] public bool spawnEnemy = true;
    [SerializeField] public bool spawnBoss = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(Spawn.i == 0 && spawnEnemy == true) // spawn Enemy
            {
                Spawn.i = 1; 
            }
        }
    }
}
