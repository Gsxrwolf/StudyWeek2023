using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField]
    public int speed = 2;
    public int health = 100;
    public int damage = 15;

    public static bool isStanding = false;

    public static bool isAlive = true;

    [SerializeField] public GameObject gameobject;

    public SpriteRenderer m_spriteRenderer;

    private PlayerController m_playerController;
    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {

       
        if (health == 0 || health <= 0) // die 
        {
            Destroy(gameObject);
            isAlive = false;
        }


        if(gameObject.transform.position.x <= transform.position.x)
        {
            m_spriteRenderer.flipX = true;
        }
        else 
        {
            m_spriteRenderer.flipX = false;
        }
        // is True if Enemy is on Bottom 
        
        transform.position = Vector3.MoveTowards(transform.position, gameobject.transform.position, speed * Time.deltaTime); // Follow the player
                                                                                                                   //  isStanding = false;
        
       
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
       

        if(collision.gameObject.CompareTag("Player"))
        {
            //isStanding = false ( bleibt stehen ) und animation starten

          //  pC = collision.GetComponent<PlayerController>();


            //PlayerController.DealDamage(melee);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().DealDamage(damage);
        }
    }
}
