using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int speed = 2;

    [SerializeField] public int XP;

    [SerializeField] public GameObject gameobject;

    [SerializeField] public int health = 50;

    [SerializeField] private int damage = 10;

    private bool isStanding = false;

    public static bool isAlive = false;

    public PlayerController pC;

    
    private void Start()
    {
        
    }
    void Update()
    {
        if (health == 0 || health <= 0) // die 
        {
            Destroy(gameObject);
            GameManager.Instance.score += XP;
            isAlive = false;
        }



        if (isStanding) // is True if Enemy is on Bottom 
        {
            transform.position = Vector3.MoveTowards(transform.position, gameObject.transform.position, speed * Time.deltaTime);  // Follow the player    
        }
    }

    public void DealDamage(int _damage) // get Damage from Player && Event !?
    {
        if(isAlive)
        {
            health = health - _damage;
        }
        
    }


    // Enemy soll stehenbleiben wenn er den Player berührt dann animation ausführen und schaden machen soferb player in range ist ( Trigger Enter 2D ? )
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bottom")) // To avoid that Enemys will fly they are "sticked" to Bottom  ( In case we add a Dash or smth )
        {
            isStanding = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().DealDamage(damage);
        }
    }
}
