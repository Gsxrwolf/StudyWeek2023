using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private int speed = 2;

    public int health = 100;
    private int melee = 15;

    public static bool isStanding = false;

    public static bool isAlive = true;

   

    void Update()
    {

       
        if (health == 0 || health <= 0) // die 
        {
            Destroy(gameObject);
            isAlive = false;
        }

        if (isStanding) // is True if Enemy is on Bottom 
        {
         //   transform.position = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime); // Follow the player
                                                                                                                   //  isStanding = false;
        }
       
    }

public void GetDamage(int _damage) // get Damage from Player && Event !?
    {
        if(isAlive)
        {
            health = health - _damage;
        }     
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bottom")) // To avoid that Enemys will fly they are "sticked" to Bottom  ( In case we add a Dash or smth )
        {
            isStanding = true;
           
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            //isStanding = false ( bleibt stehen ) und animation starten

          //  pC = collision.GetComponent<PlayerController>();


            //PlayerController.DealDamage(melee);
        }
    }    
}
